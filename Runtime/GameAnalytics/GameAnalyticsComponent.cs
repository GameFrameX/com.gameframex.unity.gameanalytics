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
    [AddComponentMenu("GameFrameX/GameAnalytics")]
    [GameFrameXAutoComponent(400)]
    public sealed class GameAnalyticsComponent : GameFrameworkComponent
    {
        private bool m_IsInit = false;
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
        /// 设置玩家ID
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        public void SetPlayerId(string playerId)
        {
            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.SetPlayerId(playerId);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <remarks>
        /// 渠道发现来源为 <see cref="GameAnalyticsRegistry"/>：渠道包通过自身 Bootstrap 在启动期自注册，注册即生效（广播语义，无路由，全部注册渠道都会收到事件）。
        /// 渠道实例化与配置注入发生在注册表工厂闭包内（编译期类型安全），本组件不再通过类型名字符串反射发现渠道。
        /// </remarks>
        public void Init()
        {
            if (m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in GameAnalyticsRegistry.GetAllManagers())
            {
                m_GameAnalyticsManager.Add(gameAnalyticsManager);
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
        /// 设置公共属性
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        public void SetPublicProperties(string key, object value)
        {
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.SetPublicProperties(key, value);
            }
        }

        /// <summary>
        /// 清除公共属性
        /// </summary>
        public void ClearPublicProperties()
        {
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.ClearPublicProperties();
            }
        }

        /// <summary>
        /// 获取公共属性
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetPublicProperties()
        {
            if (!m_IsInit)
            {
                return new Dictionary<string, object>();
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                return gameAnalyticsManager.GetPublicProperties();
            }

            return new Dictionary<string, object>();
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
        /// 暂停计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public void PauseTimer(string eventName)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.PauseTimer(eventName);
            }
        }

        /// <summary>
        /// 恢复计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public void ResumeTimer(string eventName)
        {
            GameFrameworkGuard.NotNullOrEmpty(eventName, nameof(eventName));
            if (!m_IsInit)
            {
                return;
            }

            foreach (var gameAnalyticsManager in m_GameAnalyticsManager)
            {
                gameAnalyticsManager.ResumeTimer(eventName);
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
}