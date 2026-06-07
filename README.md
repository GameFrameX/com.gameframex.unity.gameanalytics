<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X GameAnalytics Component

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Project Overview

The **GameAnalytics Component** provides interfaces for game developers to integrate and use game data analytics functionality. It includes different types of event reporting and timer features.

## Quick Start

### Installation

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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
     ],
     "dependencies": {
       "com.gameframex.unity.gameanalytics": "1.2.0"
     }
   }
   ```

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.gameanalytics": "https://github.com/gameframex/com.gameframex.unity.gameanalytics.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.gameanalytics.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.
## Usage Examples

### Initialization

`GameAnalyticsComponent` is initialized in Unity's `Awake` method, creating a `GameAnalyticsManager` instance.

```csharp
public void Init()
{
    _gameAnalyticsManager.Init();
    _isInit = true;
}
```

### Timer Functions

#### Start Timer

```csharp
public void StartTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StartTimer(eventName);
}
```

#### Stop Timer

```csharp
public void StopTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StopTimer(eventName);
}
```

### Event Reporting

#### Simple Event

```csharp
public void Event(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName);
}
```

#### Event with Value

```csharp
public void Event(string eventName, float eventValue)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName, eventValue);
}
```

#### Event with Custom Fields

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

#### Event with Value and Custom Fields

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

> **Note:** Ensure the component is properly initialized before using any methods. Event names should be representative and unique for accurate data analysis. Use the namespace `GameFrameX.GameAnalytics.Runtime` and ensure `GameAnalyticsManager` is correctly instantiated and registered with the framework.

## Documentation & Resources

- [Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: 467608841 / 233840761

## Changelog

See [Releases](https://github.com/gameframex/com.gameframex.unity.gameanalytics/releases) for changelog.


## Dependencies

| Package | Description |
|---------|-------------|
| (无) | - |

## License

See [LICENSE.md](LICENSE.md) for license information.
