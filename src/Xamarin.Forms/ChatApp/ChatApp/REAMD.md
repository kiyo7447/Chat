
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


# Android
```C#
NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://notificationnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=xxxxxxx", "NotificationHub Name");
string message = "{\"data\":{\"message\":\"Hello from Azure\"}}";
await hub.SendFcmNativeNotificationAsync(message);

```
この例では、Firebase Cloud Messaging（FCM）に対応する形式でJSONメッセージを作成し、SendFcmNativeNotificationAsyncメソッドを使用してそのメッセージを送信しています。

注意点としては、FCMを使用する場合、メッセージの形式は"data"オブジェクトを使用することと、非同期メソッドであるためawaitキーワードを使用しています。


# iOS
```C#
NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://notificationnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=xxxxxxx", "NotificationHub Name");
string message = "{\"title\":\"((Notification title))\",\"description\":\"Hello from Azure\"}";
hub.SendAppleNativeNotificationAsync(message);

```
送信するメッセージをJSON形式で作成します。この例では、タイトルと説明を含むメッセージを作成しています。

最後に、SendAppleNativeNotificationAsyncメソッドを使用して、作成したメッセージを通知ハブに送信します。

なお、このコード例はiOS向けのものですが、他のプラットフォーム向けにも同様にメッセージを送信することが可能です。詳細な情報については、Microsoftの公式チュートリアルを参照してください​1​。


# UWP
```C#
NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://notificationnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=xxxxxxx", "NotificationHub Name");
string message = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from Azure</text></binding></visual></toast>";
await hub.SendWindowsNativeNotificationAsync(message);

```
UWPに対応する形式でXMLメッセージを作成し、SendWindowsNativeNotificationAsyncメソッドを使用してそのメッセージを送信しています。

注意点としては、UWPを使用する場合、メッセージの形式は特定のXML形式（トースト通知）であることと、非同期メソッドであるためawaitキーワードを使用しています。


