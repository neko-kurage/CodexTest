# GUISystem

このモジュールでは `TransformChangeNotifier` と `UiHierarchyCache` を提供します。

## TransformChangeNotifier
`Transform` のローカル値や階層が変化した際に `UnityEvent` を発行する `MonoBehaviour` です。ハンドラーを `RegisterHandler` で登録することでコールバックを受け取れます。

## UiHierarchyCache
子階層を一度だけ走査して内部にキャッシュし、階層が変更された際にのみ再構築を行います。`Find` メソッドで `Transform` を高速に取得できます。
