using System;
using System.Collections.Generic;
using System.Linq;
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
                        Dictionary<string, string> args = new Dictionary<string, string>();
                        foreach (var param in gameAnalyticsComponentProvider.Setting)
                        {
                            args[param.Key] = param.Value;
                        }

                        gameAnalyticsManager.Init(args);
                        m_GameAnalyticsManager.Add(gameAnalyticsManager);
                    }
                }
            }

            m_IsInit = true;
        }

        /// <summary>
        /// 手动初始化
        /// </summary>
        /// <param name="extraArgs"></param>
        public void ManualInit(Dictionary<string, string> extraArgs)
        {
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                if (gameAnalyticsManager.IsManualInit())
                {
                    gameAnalyticsManager.ManualInit(extraArgs);
                }
            }
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
                if (gameAnalyticsManager.IsInit())
                {
                    gameAnalyticsManager.StartTimer(eventName);
                }
                else
                {
                    Log.Warning("GameAnalyticsManager is not init.");
                }
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
                if (gameAnalyticsManager.IsInit())
                {
                    gameAnalyticsManager.StopTimer(eventName);
                }
                else
                {
                    Log.Warning("GameAnalyticsManager is not init.");
                }
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
                if (gameAnalyticsManager.IsInit())
                {
                    gameAnalyticsManager.Event(eventName);
                }
                else
                {
                    Log.Warning("GameAnalyticsManager is not init.");
                }
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
                if (gameAnalyticsManager.IsInit())
                {
                    gameAnalyticsManager.Event(eventName, eventValue);
                }
                else
                {
                    Log.Warning("GameAnalyticsManager is not init.");
                }
            }
        }

        /// <summary>
        /// 上报自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="customF">自定义字段</param>
        public void Event(string eventName, Dictionary<string, object> customF)
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
                if (gameAnalyticsManager.IsInit())
                {
                    gameAnalyticsManager.Event(eventName, value);
                }
                else
                {
                    Log.Warning("GameAnalyticsManager is not init.");
                }
            }
        }

        /// <summary>
        /// 上报带有数值和自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        /// <param name="customF">自定义字段</param>
        public void Event(string eventName, float eventValue, Dictionary<string, object> customF)
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
                if (gameAnalyticsManager.IsInit())
                {
                    gameAnalyticsManager.Event(eventName, eventValue, value);
                }
                else
                {
                    Log.Warning("GameAnalyticsManager is not init.");
                }
            }
        }
    }

    [Serializable]
    public class GameAnalyticsComponentProvider
    {
        /// <summary>
        /// 实现组件类型
        /// </summary>
        public string ComponentType;

        /// <summary>
        /// 这个是给编辑器用的
        /// </summary>
        public int ComponentTypeNameIndex = 0;

        /// <summary>
        /// 参数
        /// </summary>
        public List<GameAnalyticsComponentParamKV> Setting = new List<GameAnalyticsComponentParamKV>();
    }

    [Serializable]
    public class GameAnalyticsComponentParamKV
    {
        public string Key;
        public string Value;
    }
}