<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X GameAnalytics 게임 분석 컴포넌트

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>
## 프로젝트 개요

**GameAnalytics 게임 분석 컴포넌트 (GameAnalytics Component)** - 게임 개발자가 게임 데이터 분석 기능을 통합하고 사용할 수 있는 인터페이스를 제공합니다. 다양한 유형의 이벤트 보고 및 타이머 기능이 포함되어 있습니다.

## 빠른 시작

### 설치 방법 (선택)

1. `manifest.json`의 `dependencies`에 다음 내용을 추가:
   ```json
   {
      "com.gameframex.unity.gameanalytics": "https://github.com/AlianBlank/com.gameframex.unity.gameanalytics.git"
   }
   ```
2. Unity의 `Packages Manager`에서 `Git URL`을 사용하여 추가: `https://github.com/AlianBlank/com.gameframex.unity.gameanalytics.git`
3. 저장소를 직접 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치하면 자동으로 로드됩니다.

## 사용 예시

### 초기화

`GameAnalyticsComponent`는 Unity의 `Awake` 메서드에서 초기화되어 `GameAnalyticsManager` 인스턴스를 생성합니다.

```csharp
public void Init()
{
    _gameAnalyticsManager.Init();
    _isInit = true;
}
```

### 타이머 기능

#### 타이머 시작

```csharp
public void StartTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StartTimer(eventName);
}
```

#### 타이머 정지

```csharp
public void StopTimer(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.StopTimer(eventName);
}
```

### 이벤트 보고

#### 단순 이벤트

```csharp
public void Event(string eventName)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName);
}
```

#### 값이 포함된 이벤트

```csharp
public void Event(string eventName, float eventValue)
{
    if (!_isInit) return;
    _gameAnalyticsManager.Event(eventName, eventValue);
}
```

#### 사용자 정의 필드가 포함된 이벤트

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

#### 값과 사용자 정의 필드가 포함된 이벤트

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

> **참고:** 컴포넌트의 메서드를 사용하기 전에 컴포넌트가 올바르게 초기화되었는지 확인하세요. 이벤트 이름은 대표성과 고유성을 가져야 데이터 분석의 정확성이 보장됩니다. 네임스페이스 `GameFrameX.GameAnalytics.Runtime`을 사용하고 `GameAnalyticsManager`가 올바르게 인스턴스화되어 프레임워크에 등록되었는지 확인하세요.

## 문서 및 자료

- [문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: 467608841 / 233840761

## 변경 로그

변경 로그는 [Releases](https://github.com/gameframex/com.gameframex.unity.gameanalytics/releases)에서 확인하세요.

## 라이선스

이 프로젝트는 [MIT 라이선스](https://github.com/gameframex/com.gameframex.unity.gameanalytics/blob/main/LICENSE)에 따라 배포됩니다.
