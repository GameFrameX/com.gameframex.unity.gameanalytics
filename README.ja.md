<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X GameAnalytics ゲーム分析コンポーネント

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## プロジェクト概要

**GameAnalytics ゲーム分析コンポーネント (GameAnalytics Component)** - ゲーム開発者がゲームデータ分析機能を統合して使用するためのインターフェースを提供します。異なるタイプのイベントレポートとタイマー機能が含まれています。

## クイックスタート

### インストール

Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

`scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.gameanalytics": "1.2.0"
  }
}
```

## 使用例

### 初期化

`GameAnalyticsComponent` は Unity の `Awake` メソッドで初期化され、`GameAnalyticsManager` インスタンスを作成します。

```csharp
public void Init()
{
    _gameAnalyticsManager.Init();
    _isInit = true;
}
```

### タイマー機能

#### タイマー開始

```csharp
public void StartTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StartTimer(eventName);
}
```

#### タイマー停止

```csharp
public void StopTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StopTimer(eventName);
}
```

### イベントレポート

#### シンプルなイベント

```csharp
public void Event(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName);
}
```

#### 値付きイベント

```csharp
public void Event(string eventName, float eventValue)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName, eventValue);
}
```

#### カスタムフィールド付きイベント

```csharp
public void Event(string eventName, Dictionary<string, string> customF)
{
    if (!_isInit) return;
    var value = new Dictionary<string, object>();
    foreach (var kv in customF)
    {
        value[kv.Key] = kv.Value;
    }
    _gameAnalyticsManager.Event(eventName, value);
}
```

#### 値とカスタムフィールド付きイベント

```csharp
public void Event(string eventName, float eventValue, Dictionary<string, string> customF)
{
    if (!_isInit) return;
    var value = new Dictionary<string, object>();
    foreach (var kv in customF)
    {
        value[kv.Key] = kv.Value;
    }
    _gameAnalyticsManager.Event(eventName, eventValue, value);
}
```

> **注意:** コンポーネントのメソッドを使用する前に、コンポーネントが正しく初期化されていることを確認してください。イベント名は代表的で一意である必要があります。ネームスペース `GameFrameX.GameAnalytics.Runtime` を使用し、`GameAnalyticsManager` が正しくインスタンス化されフレームワークに登録されていることを確認してください。

## ドキュメントとリソース

- [ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQグループ: 467608841 / 233840761

## 変更履歴

変更履歴は [Releases](https://github.com/gameframex/com.gameframex.unity.gameanalytics/releases) をご覧ください。

## ライセンス

詳しくは [LICENSE.md](LICENSE.md) をご参照ください。
