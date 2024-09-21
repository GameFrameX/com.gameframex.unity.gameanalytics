using System.Collections.Generic;
using GameFrameX.Runtime;

namespace GameFrameX.GameAnalytics.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseGameAnalyticsManager : GameFrameworkModule, IGameAnalyticsManager
    {
        protected bool m_IsInit = false;

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
        public abstract void SetPublicProperties(string key, object value);

        /// <summary>
        /// 清除公共属性
        /// </summary>
        public abstract void ClearPublicProperties();

        /// <summary>
        /// 获取公共属性
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, object> GetPublicProperties();

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
    }
}