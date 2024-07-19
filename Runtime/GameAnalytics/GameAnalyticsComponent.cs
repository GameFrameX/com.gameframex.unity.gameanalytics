using System;
using System.Collections.Generic;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.GameAnalytics.Runtime
{
    /// <summary>
    /// 游戏数据分析组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/GameAnalytics")]
    public sealed class GameAnalyticsComponent : GameFrameworkComponent
    {
        private bool m_IsInit = false;
        [SerializeField] private List<GameAnalyticsComponentProvider> m_gameAnalyticsComponentProviders = new List<GameAnalyticsComponentProvider>();
        private List<IGameAnalyticsManager> m_GameAnalyticsManager;

        protected override void Awake()
        {
            m_GameAnalyticsManager = new List<IGameAnalyticsManager>();
            IsAutoRegister = false;
            base.Awake();
        }

        private void OnEnable()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsComponentProvider in m_gameAnalyticsComponentProviders)
            {
                if (gameAnalyticsComponentProvider.ComponentType.IsNotNullOrWhiteSpace())
                {
                    var gameAnalyticsComponentType = Utility.Assembly.GetType(gameAnalyticsComponentProvider.ComponentType);
                    if (gameAnalyticsComponentType == null)
                    {
                        Log.Error($"Can not find component type '{gameAnalyticsComponentProvider.ComponentType}'.");
                        continue;
                    }

                    if (Activator.CreateInstance(gameAnalyticsComponentType) is IGameAnalyticsManager gameAnalyticsManager)
                    {
                        gameAnalyticsManager.Init(gameAnalyticsComponentProvider.AppId, gameAnalyticsComponentProvider.ChannelId, gameAnalyticsComponentProvider.Channel, gameAnalyticsComponentProvider.AppKey, gameAnalyticsComponentProvider.SecretKey);
                        m_GameAnalyticsManager.Add(gameAnalyticsManager);
                    }
                }
            }

            m_IsInit = true;
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public void StartTimer(string eventName)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.StartTimer(eventName);
            }
        }

        /// <summary>
        /// 结束计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public void StopTimer(string eventName)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.StopTimer(eventName);
            }
        }

        /// <summary>
        /// 上报事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public void Event(string eventName)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.Event(eventName);
            }
        }

        /// <summary>
        /// 上报带有数值的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        public void Event(string eventName, float eventValue)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.Event(eventName, eventValue);
            }
        }

        /// <summary>
        /// 上报自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="customF">自定义字段</param>
        public void Event(string eventName, Dictionary<string, string> customF)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            var value = new Dictionary<string, object>();

            foreach (var kv in customF)
            {
                value[kv.Key] = kv.Value;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.Event(eventName, value);
            }
        }

        /// <summary>
        /// 上报带有数值和自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        /// <param name="customF">自定义字段</param>
        public void Event(string eventName, float eventValue, Dictionary<string, string> customF)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            var value = new Dictionary<string, object>();

            foreach (var kv in customF)
            {
                value[kv.Key] = kv.Value;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.Event(eventName, eventValue, value);
            }
        }
    }

    [Serializable]
    public class GameAnalyticsComponentProvider
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId;

        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel;

        /// <summary>
        /// 渠道ID
        /// </summary>
        public string ChannelId;

        /// <summary>
        /// Key
        /// </summary>
        public string AppKey;

        /// <summary>
        /// 安全校验密码
        /// </summary>
        public string SecretKey;

        /// <summary>
        /// 实现组件类型
        /// </summary>
        public string ComponentType;

        /// <summary>
        /// 这个是给编辑器用的
        /// </summary>
        public int ComponentTypeNameIndex = 0;
    }
}