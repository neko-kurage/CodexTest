# GUISystem

このモジュールでは、UnityのGUIシステムを拡張する3つのコンポーネントを提供します。

## TransformChangeNotifier

`Transform` のローカル値（位置、回転、スケール）や階層が変化した際に `UnityEvent` を発行する `MonoBehaviour` です。

### 主な機能

- Transform の位置、回転、スケールの変化を自動検知
- 階層構造の変化も検知可能
- インスペクターでのイベント設定
- コードからのリスナー登録・解除

### 使い方

#### 1. コンポーネントの追加

```csharp
// GameObjectにコンポーネントを追加
TransformChangeNotifier notifier = gameObject.AddComponent<TransformChangeNotifier>();
```

#### 2. インスペクターでのイベント設定

- `On Transform Changed` イベントにメソッドを追加
- 変化時に呼び出されるメソッドを設定

#### 3. コードからのリスナー登録

```csharp
// ITransformChangeHandlerを実装したクラス
public class MyTransformHandler : MonoBehaviour, ITransformChangeHandler
{
    public void OnTransformChanged(Transform changed)
    {
        Debug.Log($"Transform changed: {changed.name}");
    }
}

// リスナーの登録
MyTransformHandler handler = GetComponent<MyTransformHandler>();
notifier.RegisterHandler(handler);

// リスナーの解除
notifier.UnregisterHandler(handler);
```

#### 4. 強制通知

```csharp
// 手動で変化通知を送信
notifier.ForceNotify();
```

## UiHierarchyCache

子階層を一度だけ走査して内部にキャッシュし、階層が変更された際にのみ再構築を行います。`Find` メソッドで `Transform` を高速に取得できます。

### 主な機能

- 子階層の自動キャッシュ
- 階層変更時の自動再構築
- Instance IDによる高速検索

### 使い方

#### 1. コンポーネントの追加

```csharp
// 親オブジェクトにコンポーネントを追加
UiHierarchyCache cache = parentObject.AddComponent<UiHierarchyCache>();
```

#### 2. 子オブジェクトの検索

```csharp
// 子オブジェクトのInstance IDを取得
int childId = childTransform.GetInstanceID();

// キャッシュから高速検索
Transform found = cache.Find(childId);

if (found != null)
{
    Debug.Log($"Found: {found.name}");
}
else
{
    Debug.Log("Not found");
}
```

#### 3. 実用例

```csharp
public class UIManager : MonoBehaviour
{
    private UiHierarchyCache _cache;
    private Dictionary<string, int> _childIds = new();

    void Start()
    {
        _cache = GetComponent<UiHierarchyCache>();
        
        // 子オブジェクトのIDを記録
        foreach (Transform child in transform)
        {
            _childIds[child.name] = child.GetInstanceID();
        }
    }

    public Transform FindChild(string name)
    {
        if (_childIds.TryGetValue(name, out int id))
        {
            return _cache.Find(id);
        }
        return null;
    }
}
```

## RectTransformViewportSize

`RectTransform` の幅と高さを pixel, %, `vw`, `vh`, `vmax`, `vmin` で指定するコンポーネントです。付与すると `RectTransform` のサイズは自動的に計算され、インスペクタ上では編集できなくなります。

### 対応単位

- **Pixels**: 固定ピクセル値
- **Percent**: 親要素に対する相対値（%）
- **Vw**: ビューポート幅に対する相対値（1vw = スクリーン幅の1%）
- **Vh**: ビューポート高さに対する相対値（1vh = スクリーン高さの1%）
- **Vmax**: ビューポートの大きい方の辺に対する相対値
- **Vmin**: ビューポートの小さい方の辺に対する相対値

### 使い方

#### 1. コンポーネントの追加

```csharp
// UI要素にコンポーネントを追加
RectTransformViewportSize viewSize = uiElement.AddComponent<RectTransformViewportSize>();
```

#### 2. インスペクターでの設定

- `Width`: 幅の値と単位を設定
- `Height`: 高さの値と単位を設定

#### 3. コードからの設定

```csharp
// 幅を50%に設定
viewSize.Width = new ViewportValue { Value = 50f, Unit = ViewportUnit.Percent };

// 高さを100vhに設定
viewSize.Height = new ViewportValue { Value = 100f, Unit = ViewportUnit.Vh };
```

#### 4. 使用例

```csharp
public class ResponsiveUI : MonoBehaviour
{
    void Start()
    {
        RectTransformViewportSize viewportSize = GetComponent<RectTransformViewportSize>();
        
        // スマートフォン用の設定
        if (Screen.width < 800)
        {
            // 幅は親の80%、高さは200px固定
            viewportSize.Width = new ViewportValue { Value = 80f, Unit = ViewportUnit.Percent };
            viewportSize.Height = new ViewportValue { Value = 200f, Unit = ViewportUnit.Pixels };
        }
        else
        {
            // デスクトップ用の設定
            // 幅はビューポート幅の30%、高さはビューポート高さの50%
            viewportSize.Width = new ViewportValue { Value = 30f, Unit = ViewportUnit.Vw };
            viewportSize.Height = new ViewportValue { Value = 50f, Unit = ViewportUnit.Vh };
        }
    }
}
```

## 注意事項

- `RectTransformViewportSize` が付与されたオブジェクトは、インスペクターで `RectTransform` のサイズを直接編集できません
- `TransformChangeNotifier` は `ExecuteAlways` 属性により、エディタ上でも動作します
- `UiHierarchyCache` は階層変更を自動検知しますが、大量の子オブジェクトがある場合はパフォーマンスに注意してください

## 関連ファイル

- `TransformState.cs`: Transform の状態保持クラス
- `ITransformChangeHandler.cs`: Transform 変化通知用インターフェース
- `ViewportUnits.cs`: ビューポート単位の定義
- `Editor/RectTransformLockEditor.cs`: RectTransform のサイズ編集制御用エディタ拡張
