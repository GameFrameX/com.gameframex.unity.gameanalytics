using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.GameAnalytics.Runtime
{
    [Preserve]
    internal class GameFrameXGameAnalyticsCroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
            _ = typeof(GameAnalyticsManager);
            _ = typeof(IGameAnalyticsManager);
            _ = typeof(GameAnalyticsComponent);
        }
    }
}