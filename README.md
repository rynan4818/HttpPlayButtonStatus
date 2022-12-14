# HttpPlayButtonStatus

このBeatSaberプラグインは、OBS StudioやStreamlabs Desktopのシーンコントロール用の追加スクリプトである、[obs-control](https://github.com/rynan4818/obs-control)や[Streamlabs-obs-control](https://github.com/rynan4818/Streamlabs-obs-control)のゲームスタートシーンに切り替わるタイミングを、PLAYボタンを押した瞬間に早めことができます。また、自動シーン切り替えのON/OFFやオプション用シーン1~3にBeatSaber内から手動切替ができます。

なお、デンパ時計さんがメンテされている[HttpSiraStatus](https://github.com/denpadokei/HttpSiraStatus)の使用が必須です。

https://user-images.githubusercontent.com/14249877/187076201-8d6d8cb5-152b-48ef-adfd-d4d32567cf08.mp4

# インストール方法

1. [HttpSiraStatus](https://github.com/denpadokei/HttpSiraStatus/releases)と[Beat Saber Overlay 改良版](https://github.com/rynan4818/beat-saber-overlay)および、[obs-control](https://github.com/rynan4818/obs-control)または、[Streamlabs-obs-control](https://github.com/rynan4818/Streamlabs-obs-control)をインストールして動作するか確認します。

    `Beat Saber Overlay 改良版`は**Release v2022/04/25**以降、 `obs-control`と`Streamlabs-obs-control`は**Release v2022/08/28**以降が対応しています。

2. [リリースページ](https://github.com/rynan4818/HttpPlayButtonStatus/releases)から最新のHttpPlayButtonStatusのリリースをダウンロードします。

3. ダウンロードしたzipファイルをBeat Saberフォルダに解凍して、`Plugin`フォルダに`HttpPlayButtonStatus.dll`ファイルをコピーします。

※このmodは以下のプラグインに依存しています。
  - BSIPA
  - BeatSaberMarkupLanguage
  - SiraUtil
  - [HttpSiraStatus](https://github.com/denpadokei/HttpSiraStatus/releases)

それぞれの依存modの対応バージョンは[manifest.json](https://github.com/rynan4818/HttpPlayButtonStatus/blob/main/HttpPlayButtonStatus/manifest.json)の`dependsOn`項目を参照下さい。

# 使用方法

HttpPlayButtonStatusをインストールすると、下記画面の様なゲームプレイのMOD設定にPLAY BUTTON STATUSが追加されます。

![image](https://user-images.githubusercontent.com/14249877/187055755-e2df84c3-c2aa-46cc-a560-aac88ac2f754.png)

- Scene Change : 自動シーン切り替え機能のON/OFFを設定します。
- Play Button Start Change : PLAYボタンを押した瞬間にゲームシーンに切り替えます。OFFにすると従来どおりの動作になります。
- Play Button Change Delay : PLAYボタンを押した瞬間からゲームシーンに切り替えるまでの時間を遅らせたい場合に設定します。
- MENU SCENE : メニュー用のシーンに手動で切り替えます。
- OPTION SCENE 1~3 : オプション用のシーンに手動で切り替えます。(項目名は設定ファイルで変更可能)

MODの設定画面が不要な場合は、目玉アイコンからPlay Button StatusをOFFにすると設定画面が消えます。

![image](https://user-images.githubusercontent.com/14249877/187055778-722ecdf7-56df-4c21-9dba-2b37af46866d.png)

# 設定ファイル

`Beat Saber\UserData`フォルダの`HttpPlayButtonStatus.json`でオプション用シーンのボタン表示名を変更できます。

    {
      "PlayButtonEnable": true,
      "SceneChangeEnable": true,
      "PlayButtonDelay": 0.0,
      "OptionSceneName1": "Break Time",       ※オプション1用ボタン名
      "OptionSceneName2": "Camera Script",    ※オプション2用ボタン名
      "OptionSceneName3": "Ending Scene"      ※オプション3用ボタン名
    }

`PlayButtonEnable`, `SceneChangeEnable`, `PlayButtonDelay`はMODの設定画面から変更できます。

![image](https://user-images.githubusercontent.com/14249877/187057715-28665ead-d923-4501-9050-fb506cba21b4.png)
