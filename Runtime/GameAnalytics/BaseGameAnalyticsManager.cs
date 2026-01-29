using System.Collections.Generic;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.GameAnalytics.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseGameAnalyticsManager : GameFrameworkModule, IGameAnalyticsManager
    {
        protected bool m_IsInit = false;

        /// <summary>
        /// 公共属性
        /// </summary>
        protected readonly Dictionary<string, object> m_PublicProperties = new Dictionary<string, object>();

        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        protected override void Shutdown()
        {
        }


        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init(Dictionary<string, string> args);

        /// <summary>
        /// 手动初始化
        /// </summary>
        public abstract void ManualInit(Dictionary<string, string> args);

        /// <summary>
        /// 是否初始化
        /// </summary>
        /// <returns></returns>
        public bool IsInit()
        {
            return m_IsInit;
        }

        /// <summary>
        /// 是否手动初始化
        /// </summary>
        public abstract bool IsManualInit();

        /// <summary>
        /// 设置公共属性
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        public virtual void SetPublicProperties(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            m_PublicProperties[key] = value;
        }

        /// <summary>
        /// 清除公共属性
        /// </summary>
        public virtual void ClearPublicProperties()
        {
            m_PublicProperties.Clear();
            AddDeviceInfoToPublicProperties();
        }

        /// <summary>
        /// 获取公共属性
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, object> GetPublicProperties()
        {
            return new Dictionary<string, object>(m_PublicProperties);
        }

        /// <summary>
        /// 添加设备信息到公共属性
        /// </summary>
        protected virtual void AddDeviceInfoToPublicProperties()
        {
            // 设备基本信息
            m_PublicProperties["device_id"] = SystemInfo.deviceUniqueIdentifier;
            m_PublicProperties["device_model"] = SystemInfo.deviceModel;
            m_PublicProperties["device_type"] = SystemInfo.deviceType.ToString();

            // 操作系统信息
            m_PublicProperties["os"] = SystemInfo.operatingSystem;

            // 应用程序信息
            m_PublicProperties["app_version"] = Application.version;
            m_PublicProperties["unity_version"] = Application.unityVersion;
            m_PublicProperties["platform"] = Application.platform.ToString();

            // 系统硬件信息
            m_PublicProperties["processor_type"] = SystemInfo.processorType;
            m_PublicProperties["processor_count"] = SystemInfo.processorCount;
            m_PublicProperties["processor_frequency"] = SystemInfo.processorFrequency;
            m_PublicProperties["system_memory_size"] = SystemInfo.systemMemorySize;

            // 图形相关信息
            m_PublicProperties["graphics_device_name"] = SystemInfo.graphicsDeviceName;
            m_PublicProperties["graphics_device_type"] = SystemInfo.graphicsDeviceType.ToString();
            m_PublicProperties["graphics_memory_size"] = SystemInfo.graphicsMemorySize;
            m_PublicProperties["graphics_device_version"] = SystemInfo.graphicsDeviceVersion;
            m_PublicProperties["graphics_shader_level"] = SystemInfo.graphicsShaderLevel;

            // 屏幕信息
            m_PublicProperties["screen_width"] = Screen.width;
            m_PublicProperties["screen_height"] = Screen.height;
            m_PublicProperties["screen_dpi"] = Screen.dpi;
            m_PublicProperties["screen_refresh_rate"] = Screen.currentResolution.refreshRate;
            // 添加本地化语言信息
            m_PublicProperties["system_language"] = Application.systemLanguage.ToString();
            m_PublicProperties["current_culture"] = System.Globalization.CultureInfo.CurrentCulture.Name;

            // 网络类型
            string networkType;
            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    networkType = "No Network";
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    networkType = "Mobile Data";
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    networkType = "WiFi";
                    break;
                default:
                    networkType = "Unknown";
                    break;
            }

            m_PublicProperties["network_type"] = networkType;
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public abstract void StartTimer(string eventName);

        /// <summary>
        /// 暂停计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public abstract void PauseTimer(string eventName);

        /// <summary>
        /// 恢复计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public abstract void ResumeTimer(string eventName);

        /// <summary>
        /// 结束计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public abstract void StopTimer(string eventName);

        /// <summary>
        /// 上报事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        public abstract void Event(string eventName);

        /// <summary>
        /// 上报带有数值的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        public abstract void Event(string eventName, float eventValue);

        /// <summary>
        /// 上报自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="customF">自定义字段</param>
        public abstract void Event(string eventName, Dictionary<string, object> customF);

        /// <summary>
        /// 上报带有数值和自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        /// <param name="customF">自定义字段</param>
        public abstract void Event(string eventName, float eventValue, Dictionary<string, object> customF);

        /// <summary>
        /// 设置玩家ID
        /// </summary>
        /// <param name="playerId">玩家ID</param>
        public abstract void SetPlayerId(string playerId);
    }
}