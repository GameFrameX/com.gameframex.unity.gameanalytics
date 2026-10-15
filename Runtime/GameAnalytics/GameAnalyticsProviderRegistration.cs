// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任。

using System;

namespace GameFrameX.GameAnalytics.Runtime
{
    /// <summary>
    /// 统计渠道注册描述
    /// </summary>
    /// <remarks>
    /// 由渠道包在启动时通过 <see cref="GameAnalyticsRegistry.Register(GameAnalyticsProviderRegistration)"/> 显式注册，
    /// 工厂委托在首次获取时才实例化渠道管理器，并在闭包内注入强类型渠道配置（app key 等）。
    /// 统计域为广播语义：注册即生效，一个事件会 fan-out 到全部注册渠道，与支付域的按 ChannelId 路由语义不同。
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class GameAnalyticsProviderRegistration
    {
        /// <summary>
        /// 初始化统计渠道注册描述
        /// </summary>
        /// <param name="channelId">渠道唯一标识</param>
        /// <param name="displayName">渠道展示名称（用于 UI 展示）</param>
        /// <param name="factory">渠道管理器工厂，编译期类型安全，首次使用时调用并注入渠道配置</param>
        public GameAnalyticsProviderRegistration(string channelId, string displayName, Func<IGameAnalyticsManager> factory)
        {
            if (string.IsNullOrEmpty(channelId))
            {
                throw new ArgumentNullException(nameof(channelId));
            }

            ChannelId = channelId;
            DisplayName = displayName;
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// 获取渠道唯一标识
        /// </summary>
        public string ChannelId { get; }

        /// <summary>
        /// 获取渠道展示名称
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// 获取渠道管理器工厂
        /// </summary>
        public Func<IGameAnalyticsManager> Factory { get; }
    }
}
