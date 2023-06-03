﻿
# Entry画面とロジックの作成
初めて起動する場合に表示するユーザ情報を入力する画面（Xamarin FormのXAML）とロジック（C#）を作ってください。
入力するユーザ情報は下記のとおりです。
・入力項目は、
　・ユーザ名を入力する。
　・対象の画像（Image）を選択する。
　　イメージは、emoji1.png～emoji10.pngまでの10個があります。画像は、幅が222px、高さが324pxです。
　・イメージ画像の背景色を選択する。
・入力したユーザ情報は、Xamarin.Essentials.Preferencesに保存してください。キーは、「UserInfo」です。
・Viewのクラス名は、EntryViewで作ります。
・ユーザ登録が終わったら、HomeViewという画面へ遷移してください。

