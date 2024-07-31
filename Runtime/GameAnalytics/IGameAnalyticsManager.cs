using System.Collections.Generic;

namespace GameFrameX.GameAnalytics.Runtime
{
    /// <summary>
    /// 游戏数据分析组件。
    /// </summary>
    public interface IGameAnalyticsManager
    {
        /// <summary>
        /// 自动初始化
        /// </summary>
        void Init(Dictionary<string, string> args);

        /// <summary>
        /// 手动初始化
        /// </summary>
        void ManualInit(Dictionary<string, string> args);

        /// <summary>
        /// 是否初始化
        /// </summary>
        bool IsInit();

        /// <summary>
        /// 是否手动初始化
        /// </summary>
        bool IsManualInit();

        /// <summary>
        /// 设置公共属性
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        void SetPublicProperties(string key, object value);

        /// <summary>
        /// 清除公共属性
        /// </summary>
        void ClearPublicProperties();

        /// <summary>
        /// 获取公共属性
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetPublicProperties();

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        void StartTimer(string eventName);

        /// <summary>
        /// 暂停计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        void PauseTimer(string eventName);

        /// <summary>
        /// 恢复计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        void ResumeTimer(string eventName);

        /// <summary>
        /// 结束计时
        /// </summary>
        /// <param name="eventName">事件名称</param>
        void StopTimer(string eventName);

        /// <summary>
        /// 上报事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        void Event(string eventName);

        /// <summary>
        /// 上报带有数值的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        void Event(string eventName, float eventValue);

        /// <summary>
        /// 上报自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="customF">自定义字段</param>
        void Event(string eventName, Dictionary<string, object> customF);

        /// <summary>
        /// 上报带有数值和自定义字段的事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventValue">事件数值</param>
        /// <param name="customF">自定义字段</param>
        void Event(string eventName, float eventValue, Dictionary<string, object> customF);
    }
}