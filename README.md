# ScreenCapture_eDoc
 
「ScreenCapture_eDoc」は、Eclipse External Beam Planning 画面をキャプチャーして ARIA eDoc プリンターに出力する ESAPI スクリプトです。
  
# Features

Eclipse External Beam Planning 画面をキャプチャし、ARIA eDoc プリンターを介して画像を ARIA ドキュメントに直接保存します。

# Demo

![Screen capture of planCompare UI](https://github.com/tkmd94/ScreenCapture_eDoc/blob/master/demo.gif)

# Requirement

* ARIA eDoc
* Eclipse 15.6 以上 (古いバージョンではチェックされていません。)

# Installation

1. このプロジェクトをビルドして、DLL ファイル [ScreenCapture_eDoc.esapi.dll] を生成します。
2. 生成された DLL ファイルを、Eclipse ツールバーの [ツール] -> [スクリプト] で指定したフォルダーにコピーします。
3. このスクリプトをお気に入りとしてマークし、キーボード ショートカットを設定します。
4. ARIA Data AdministrationでDocumentTypeを登録する。
    1. ARIA -> Data Administration -> Clinical Assessment -> DocumentTypeにスクリーンキャプチャで取り込む文書の名称を登録する。
5. ARIA eDocのプロファイルを登録する。
    1. サンプル```ARIAeDocProfile_ENU_ESAPI_ScreenCapture.xml```の```ScreenCapture```の部分を前項で登録した名称に変更してプロファイルを保存する。   
        ```<x:result tag="DocumentType">ScreenCapture</x:result>```   
        ※プロファイル名は適宜修正する。
    2. ARIA serverの次のフォルダにプロファイルをコピーする。   
        ```D:\Varian\Data\ARIA IC\ARIAeDoc\Profiles```   
    3. ARIA Serverのサービスから```Varian ARIA IC ARIA eDoc```を再起動する。

# Usage

**※本ソースコードは自己責任で使用してください。**

1. キャプチャしたい画面を表示します。
2. 登録したキーボード ショートカットからスクリプトを実行します。
3. スクリプトの実行が完了すると、[OK] ダイアログが表示されます。
 
# Author
 
* Takashi Kodama
 
# License
 
"ScreenCapture_eDoc" は [MIT ライセンス](https://en.wikipedia.org/wiki/MIT_License) の下にあります。
