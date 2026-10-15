using UnityEngine;

namespace GameFrameX.GameAnalytics.Runtime
{
    [UnityEngine.Scripting.Preserve]
    internal class GameFrameXGameAnalyticsCroppingHelper : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            _ = typeof(BaseGameAnalyticsManager);
            _ = typeof(IGameAnalyticsManager);
            _ = typeof(GameAnalyticsComponent);
            _ = typeof(GameAnalyticsProviderRegistration);
            _ = typeof(GameAnalyticsRegistry);
        }
    }
}