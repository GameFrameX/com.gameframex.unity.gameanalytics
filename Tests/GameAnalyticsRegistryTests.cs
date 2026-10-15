// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任。

using System.Collections.Generic;
using GameFrameX.GameAnalytics.Runtime;
using GameFrameX.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GameFrameX.GameAnalytics.Tests
{
    public class GameAnalyticsRegistryTests
    {
        private class StubAnalyticsManager : BaseGameAnalyticsManager
        {
            public int SetPlayerIdCallCount;
            public string LastPlayerId;

            public override void Init(Dictionary<string, string> args) { }
            public override void ManualInit(Dictionary<string, string> args) { }
            public override bool IsManualInit() { return false; }
            public override void StartTimer(string eventName) { }
            public override void PauseTimer(string eventName) { }
            public override void ResumeTimer(string eventName) { }
            public override void StopTimer(string eventName) { }
            public override void Event(string eventName) { }
            public override void Event(string eventName, float eventValue) { }
            public override void Event(string eventName, Dictionary<string, object> customF) { }
            public override void Event(string eventName, float eventValue, Dictionary<string, object> customF) { }

            public override void SetPlayerId(string playerId)
            {
                SetPlayerIdCallCount++;
                LastPlayerId = playerId;
            }
        }

        private sealed class OtherAnalyticsManager : StubAnalyticsManager
        {
        }

        [SetUp]
        public void SetUp()
        {
            GameAnalyticsRegistry.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            GameAnalyticsRegistry.Reset();
        }

        [Test]
        public void Register_ThenGetManager_CreatesInstanceLazilyAndCaches()
        {
            var stub = new StubAnalyticsManager();
            var factoryCallCount = 0;
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("stub", "存根渠道", () =>
            {
                factoryCallCount++;
                return stub;
            }));

            Assert.IsTrue(GameAnalyticsRegistry.Contains("stub"));
            Assert.AreEqual(0, factoryCallCount, "工厂在首次获取前不应被调用");

            var first = GameAnalyticsRegistry.GetManager("stub");
            var second = GameAnalyticsRegistry.GetManager("stub");

            Assert.IsNotNull(first);
            Assert.IsInstanceOf<StubAnalyticsManager>(first);
            Assert.AreSame(first, second, "同一渠道应复用实例");
            Assert.AreEqual(1, factoryCallCount);
        }

        [Test]
        public void Register_DuplicateChannelId_KeepsFirstRegistration()
        {
            var first = new StubAnalyticsManager();
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("stub", "第一个", () => first));
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("stub", "第二个", () => new OtherAnalyticsManager()));

            Assert.AreSame(first, GameAnalyticsRegistry.GetManager("stub"), "重复注册应保留首个");
        }

        [Test]
        public void GetManager_UnknownOrNullChannel_ReturnsNull()
        {
            Assert.IsNull(GameAnalyticsRegistry.GetManager("not-exists"));
            Assert.IsNull(GameAnalyticsRegistry.GetManager(null));
            Assert.IsNull(GameAnalyticsRegistry.GetManager(string.Empty));
            Assert.IsFalse(GameAnalyticsRegistry.Contains("not-exists"));
            Assert.IsFalse(GameAnalyticsRegistry.Contains(null));
        }

        [Test]
        public void Register_NullArguments_AreIgnoredOrThrown()
        {
            GameAnalyticsRegistry.Register(null);
            Assert.IsFalse(GameAnalyticsRegistry.Contains("stub"));

            Assert.Throws<System.ArgumentNullException>(() => new GameAnalyticsProviderRegistration(null, "无效", () => new StubAnalyticsManager()));
            Assert.Throws<System.ArgumentNullException>(() => new GameAnalyticsProviderRegistration("stub", "无效", null));
        }

        [Test]
        public void Unregister_RemovesChannelAndCachedInstance()
        {
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("stub", "存根渠道", () => new StubAnalyticsManager()));
            var manager = GameAnalyticsRegistry.GetManager("stub");
            Assert.IsNotNull(manager);

            GameAnalyticsRegistry.Unregister("stub");
            Assert.IsFalse(GameAnalyticsRegistry.Contains("stub"));
            Assert.IsNull(GameAnalyticsRegistry.GetManager("stub"));

            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("stub", "存根渠道", () => new StubAnalyticsManager()));
            Assert.IsInstanceOf<StubAnalyticsManager>(GameAnalyticsRegistry.GetManager("stub"), "重新注册后可再次获取");
        }

        [Test]
        public void Reset_ClearsAllRegistrations()
        {
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("stub", "存根渠道", () => new StubAnalyticsManager()));
            GameAnalyticsRegistry.Reset();

            Assert.IsFalse(GameAnalyticsRegistry.Contains("stub"));
            Assert.IsNull(GameAnalyticsRegistry.GetManager("stub"));
        }

        [Test]
        public void GetAllManagers_ReturnsManagersInRegistrationOrder()
        {
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("channel-a", "渠道A", () => new StubAnalyticsManager()));
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("channel-b", "渠道B", () => new OtherAnalyticsManager()));

            var managers = GameAnalyticsRegistry.GetAllManagers();

            Assert.AreEqual(2, managers.Count);
            Assert.IsInstanceOf<StubAnalyticsManager>(managers[0]);
            Assert.IsInstanceOf<OtherAnalyticsManager>(managers[1], "返回顺序应与注册顺序一致");
        }

        [Test]
        public void TalkingDataBootstrap_Register_RegistersTalkingDataChannel()
        {
            // 反射调用渠道包 Bootstrap 的注册方法，验证自注册产物（渠道标识与实例类型全名）
            var bootstrapType = System.Type.GetType("GameFrameX.GameAnalytics.TalkingData.Runtime.TalkingDataAnalyticsBootstrap, GameFrameX.GameAnalytics.TalkingData.Runtime");
            Assume.That(bootstrapType, Is.Not.Null, "未安装 talkingdata 渠道包时跳过");
            Utility.Json.SetJsonHelper(new LitJsonHelper());
            try
            {
                var registerMethod = bootstrapType.GetMethod("Register", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                registerMethod.Invoke(null, null);

                Assert.IsTrue(GameAnalyticsRegistry.Contains("talkingdata"));
                Assert.AreEqual("GameFrameX.GameAnalytics.TalkingData.Runtime.GameAnalyticsByTalkingDataManager", GameAnalyticsRegistry.GetManager("talkingdata").GetType().FullName);
            }
            finally
            {
                Utility.Json.SetJsonHelper(null);
            }
        }

        [Test]
        public void GravityEngineBootstrap_Register_RegistersGravityEngineChannel()
        {
            var bootstrapType = System.Type.GetType("GameFrameX.GameAnalytics.GravityEngine.Runtime.GravityEngineAnalyticsBootstrap, GameFrameX.GameAnalytics.GravityEngine.Runtime");
            Assume.That(bootstrapType, Is.Not.Null, "未安装 gravity-engine 渠道包时跳过");
            Utility.Json.SetJsonHelper(new LitJsonHelper());
            try
            {
                var registerMethod = bootstrapType.GetMethod("Register", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                // EditMode 场景无 GravityEngineAPI 组件，manager.Init 的场景探测按预期走错误日志分支
                LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex("GravityEngineAPI"));
                registerMethod.Invoke(null, null);
                Assert.IsTrue(GameAnalyticsRegistry.Contains("gravity-engine"));
                Assert.AreEqual("GameFrameX.GameAnalytics.GravityEngine.Runtime.GameAnalyticsByGravityEngineManager", GameAnalyticsRegistry.GetManager("gravity-engine").GetType().FullName);
            }
            finally
            {
                Utility.Json.SetJsonHelper(null);
            }
        }

        [Test]
        public void Component_InitAndSetPlayerId_FanOutToAllRegisteredChannels()
        {
            var first = new StubAnalyticsManager();
            var second = new OtherAnalyticsManager();
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("channel-a", "渠道A", () => first));
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration("channel-b", "渠道B", () => second));

            var gameObject = new GameObject("GameAnalyticsFanOutTest");
            try
            {
                var component = gameObject.AddComponent<GameAnalyticsComponent>();
                // EditMode 下 AddComponent 不会触发 Awake，手动反射调用以还原运行时初始化顺序
                typeof(GameAnalyticsComponent).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(component, null);
                component.Init();
                component.SetPlayerId("player-1");

                Assert.AreEqual(1, first.SetPlayerIdCallCount, "注册两个渠道时，SetPlayerId 每个渠道各收到一次");
                Assert.AreEqual(1, second.SetPlayerIdCallCount, "注册两个渠道时，SetPlayerId 每个渠道各收到一次");
                Assert.AreEqual("player-1", first.LastPlayerId);
                Assert.AreEqual("player-1", second.LastPlayerId);
            }
            finally
            {
                Object.DestroyImmediate(gameObject);
            }
        }
    }
}
