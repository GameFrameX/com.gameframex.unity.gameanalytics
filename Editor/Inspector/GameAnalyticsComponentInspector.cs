//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX.Editor;
using GameFrameX.GameAnalytics.Runtime;
using UnityEditor;

namespace GameFrameX.GameAnalytics.Editor
{
    [CustomEditor(typeof(GameAnalyticsComponent))]
    internal sealed class GameAnalyticsComponentInspector : GameFrameworkInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Repaint();
        }
    }
}