using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using VMS.TPS.Common.Model.API;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        //Define variables
        string printerName = "";
        string patientName = "NA";
        string patientId = "NA";
        string courseId = "NA";
        string planId = "NA";
        string user = "NA";
        string printDate = "NA";
        Bitmap captImage;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            //Find eDoc Printer
            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (s.Contains("eDoc"))
                    printerName = s;
            }

            var plan = context.PlanSetup;
            var planSum = context.PlanSumsInScope.FirstOrDefault();
            var patient = context.Patient;
            var course = context.Course;

            if (patient == null)
            {
                MessageBox.Show("No patient loaded!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (context.PlanSumsInScope.Count() > 1)
            {
                MessageBox.Show("Two or more PlanSums are loaded in Scope.\nPlease close the unused PlanSum.!", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            patientName = patient.LastName + " " + patient.FirstName;
            patientId = patient.Id;
            user = context.CurrentUser.Name;
            printDate = DateTime.Now.ToString();

            var selectedPlanningItem = plan != null ? (PlanningItem)plan : (PlanningItem)planSum;

            if (course != null)
            {
                courseId = course.Id;
            }

            if (selectedPlanningItem != null)
            {
                planId = selectedPlanningItem.Id;
            }

            Thread trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();

        }

        /// <summary>
        /// Define RECT class
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /// <summary>
        /// Gets the size of the bounding rectangle of the specified window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        [DllImport("User32.Dll")]
        static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

        /// <summary>
        /// Take a screen capture and print.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        extern static IntPtr GetForegroundWindow();
        private void ThreadTask()
        {

            Rectangle rectangle = new Rectangle();

            // Wait 500 miliseconds for script progress bar to disappear
            Thread.Sleep(500);

            RECT rect;
            IntPtr active = GetForegroundWindow();
            GetWindowRect(active, out rect);
            rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

            captImage = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);

            // Get screen capture using CopyFromScreen method.
            using (Graphics g = Graphics.FromImage(captImage))
            {
                g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.DefaultPageSettings.Landscape = true;
            if (printerName != "")
                pd.PrinterSettings.PrinterName = printerName;

            for (var index = 0; index < pd.PrinterSettings.PaperSizes.Count; index++)
            {
                if (pd.PrinterSettings.PaperSizes[index].PaperName.Contains("A3") == true)
                {
                    pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes[index];
                    break;
                }
            }

            pd.Print();

            MessageBox.Show("Done.");
        }

        /// <summary>
        /// Draw header information on the captured image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            float zoom = 1;
            float padding = 50;

            if (captImage.Width > e.Graphics.VisibleClipBounds.Width)
            {
                zoom = e.Graphics.VisibleClipBounds.Width /
                    captImage.Width;
            }

            if ((captImage.Height + padding) * zoom >
                    e.Graphics.VisibleClipBounds.Height)
            {
                zoom = e.Graphics.VisibleClipBounds.Height /
                    (captImage.Height + padding);
            }

            e.Graphics.DrawImage(captImage, 0, padding,
                                               captImage.Width * zoom,
                                               captImage.Height * zoom);

            Font font = new Font("Arial Unicode MS", 10.5f);
            Brush brush = new SolidBrush(Color.Black);

            e.Graphics.DrawString(string.Format("Patient ID: {0}, Patient Name: {1}", patientId, patientName), font, brush, new PointF(10, 10));
            e.Graphics.DrawString(string.Format("Course ID: {0}, Plan ID: {1},", courseId, planId), font, brush, new PointF(10, 30));
            string outText = string.Format("(ESAPI ScreenCapture) Printed on {0} by {1}", printDate, user);
            SizeF stringSize = e.Graphics.MeasureString(outText, font, 1000);
            e.Graphics.DrawString(outText, font, brush, new PointF(e.Graphics.VisibleClipBounds.Width - stringSize.Width - 10, 10));
        }
    }
}
