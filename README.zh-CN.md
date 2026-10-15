<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X GameAnalytics 游戏数据分析组件

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 项目简介

**GameAnalytics 游戏数据分析组件 (GameAnalytics Component)** - 提供游戏开发者集成和使用游戏数据分析的功能的接口。它包含了不同类型的事件上报和计时器功能。

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.gameanalytics": "https://github.com/gameframex/com.gameframex.unity.gameanalytics.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.gameanalytics.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。
## 使用示例

### 初始化

`GameAnalyticsComponent` 在 Unity 的 `Awake` 方法中被初始化，创建一个 `GameAnalyticsManager` 实例。

```csharp
public void Init()
{
    _gameAnalyticsManager.Init();
    _isInit = true;
}
```

### 计时功能

#### 开始计时

```csharp
public void StartTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StartTimer(eventName);
}
```

#### 结束计时

```csharp
public void StopTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StopTimer(eventName);
}
```

### 事件上报

#### 简单事件上报

```csharp
public void Event(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName);
}
```

#### 带数值的事件上报

```csharp
public void Event(string eventName, float eventValue)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName, eventValue);
}
```

#### 带自定义字段的事件上报

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

#### 带数值和自定义字段的事件上报

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

> **注意：** 请确保在使用组件的任何方法之前，组件已被正确初始化。上报的事件名称应该具有代表性和唯一性，以确保数据分析的准确性。使用命名空间 `GameFrameX.GameAnalytics.Runtime`，确保 `GameAnalyticsManager` 被正确实例化并已通过框架注册。

## 文档与资源

- [文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ群: 467608841 / 233840761

## 渠道注册表

自本版本起，`GameAnalyticsComponent` 的渠道发现不再使用 Inspector 序列化的 provider 类型名列表。渠道包通过自身的 Bootstrap（`RuntimeInitializeOnLoadMethod`，`BeforeSceneLoad`）在启动期向 `GameAnalyticsRegistry` 自注册（引用即注册：未安装的渠道包代码被裁剪，注册不会发生）。注册即生效，为广播语义：每个统计事件都会广播到全部注册渠道（无路由，与支付域的按渠道路由语义不同）。注册表懒实例化渠道管理器，且不参与 GameFrameworkEntry 模块 Update/Shutdown 生命周期。

**破坏性变更 / 迁移说明：** `GameAnalyticsComponent` 上序列化的 provider 列表（`GameAnalyticsComponentProvider`：`ComponentType` 类型名字符串 + 键值对配置）已移除，不再从 Inspector 配置渠道。请改为在游戏启动代码中对渠道包 Bootstrap 的强类型配置赋值（见各渠道包 README）。

## 更新日志

查看 [Releases](https://github.com/gameframex/com.gameframex.unity.gameanalytics/releases) 了解更新日志。


## 依赖

| 包 | 说明 |
|----|------|
| (无) | - |

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
