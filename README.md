# ScreenCapture_eDoc
 
「ScreenCapture_eDoc」は、Eclipse External Beam Planning 画面をキャプチャーして ARIA eDoc プリンターに出力する ESAPI スクリプトです。
  
# Features

Eclipse External Beam Planning 画面をキャプチャし、ARIA eDoc プリンターを介して画像を ARIA ドキュメントに直接保存します。

# Requirement

* ARIA eDoc
* Eclipse 15.6 以上 (古いバージョンではチェックされていません。)

# Installation

1. このプロジェクトをビルドして、DLL ファイル [ScreenCapture_eDoc.esapi.dll] を生成します。
2. 生成された DLL ファイルを、Eclipse ツールバーの [ツール] -> [スクリプト] で指定したフォルダーにコピーします。
3. このスクリプトをお気に入りとしてマークし、キーボード ショートカットを設定します。
4. ARIAeDocProfile_ENU_ESAPI_ScreenCapture.xml を ARIA eDoc の Profiles フォルダーにコピーし、設定を変更します。

# Usage

1. キャプチャしたい画面を表示します。
2. 登録したキーボード ショートカットからスクリプトを実行します。
3. スクリプトの実行が完了すると、[OK] ダイアログが表示されます。
 
# Author
 
* Takashi Kodama
 
# License
 
"ScreenCapture_eDoc" は [MIT ライセンス](https://en.wikipedia.org/wiki/MIT_License) の下にあります。
