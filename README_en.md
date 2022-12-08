# ScreenCapture_eDoc
 
"ScreenCapture_eDoc" is an ESAPI script that captures the Eclipse External Beam Planning screen and prints it to the ARIA eDoc printer.
  
# Features

Capture the Eclipse External Beam Planning screen and save image directly to ARIA Documents via the ARIA eDoc printer.

# Demo

![Screen capture of planCompare UI](https://github.com/tkmd94/ScreenCapture_eDoc/blob/master/demo.gif)

# Requirement

* ARIA eDoc
* Eclipse 15.6 or higher (Not checked in older versions.)

# Installation

1. Build this project to generate the DLL file [ScreenCapture_eDoc.esapi.dll]. 
2. Copy the generated DLL file to the folder specified in Tools -> Scripts in the Eclipse toolbar.
3. Mark this script as a favorite and set a keyboard shortcut.
4. Copy ARIAeDocProfile_ENU_ESAPI_ScreenCapture.xml to ARIA eDoc's Profiles folder and modify the settings.

# Usage
**Please use this source code at your own risk.**
1. Display the screen you want to capture.
2. Run the script from the registered keyboard shortcut.
3. When script execution is complete, an OK dialog will appear.
 
# Author
 
* Takashi Kodama
 
# License
 
"ScreenCapture_eDoc" is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).
