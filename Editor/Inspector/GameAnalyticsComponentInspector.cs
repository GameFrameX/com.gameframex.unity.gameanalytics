//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using GameFrameX.Editor;
using GameFrameX.GameAnalytics.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GameFrameX.GameAnalytics.Editor
{
    [CustomEditor(typeof(GameAnalyticsComponent))]
    internal sealed class GameAnalyticsComponentInspector : ComponentTypeComponentInspector
    {
        private SerializedProperty m_GameAnalyticsComponentProviders;

        private readonly GUIContent m_GameAnalyticsComponentProvidersGUIContent = new GUIContent("游戏数据分析组件列表,按序上报");

        // private readonly GUIContent m_AppIdGUIContent = new GUIContent("AppId");
        // private readonly GUIContent m_ChannelIdGUIContent = new GUIContent("ChannelId");
        // private readonly GUIContent m_ChannelGUIContent = new GUIContent("Channel");
        // private readonly GUIContent m_AppKeyGUIContent = new GUIContent("AppKey");
        // private readonly GUIContent m_SecretKeyGUIContent = new GUIContent("SecretKey");
        private readonly GUIContent m_ComponentTypeGUIContent = new GUIContent("ComponentType");
        private readonly GUIContent m_ParamsGUIContent        = new GUIContent("Params");

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update();
            m_ReorderAbleList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        protected override void RefreshTypeNames()
        {
            List<string> managerTypeNameList = new List<string>
            {
                NoneOptionName
            };
            managerTypeNameList.AddRange(Type.GetRuntimeTypeNames(typeof(IGameAnalyticsManager)));
            m_ManagerTypeNames = managerTypeNameList.ToArray();
        }

        private ReorderableList m_ReorderAbleList;

        string[] m_ManagerTypeNames = new string[]
        {
            NoneOptionName
        };

        protected override void Enable()
        {
            m_GameAnalyticsComponentProviders = serializedObject.FindProperty("m_gameAnalyticsComponentProviders");
            m_ReorderAbleList = new ReorderableList(serializedObject, m_GameAnalyticsComponentProviders, true, true, true, true)
            {
                drawElementCallback   = DrawElementCallback,
                elementHeightCallback = ElementHeightCallback,
                drawHeaderCallback    = DrawHeaderCallback,
            };
        }

        private void DrawHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, m_GameAnalyticsComponentProvidersGUIContent);
        }

        private float ElementHeightCallback(int index)
        {
            SerializedProperty element       = m_ReorderAbleList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty paramProperty = element.FindPropertyRelative("Params");
            if (paramProperty.isExpanded)
            {
                int count = 2;
                for (int i = 0; i < paramProperty.arraySize; i++)
                {
                    var propertyElement = paramProperty.GetArrayElementAtIndex(i);
                    if (propertyElement.isExpanded)
                    {
                        count += 2;
                    }
                }

                return (EditorGUIUtility.singleLineHeight + 6) * (paramProperty.arraySize + count) + EditorGUIUtility.standardVerticalSpacing * 2;
            }
            else
            {
                return (EditorGUIUtility.singleLineHeight + 6) * 2 + EditorGUIUtility.standardVerticalSpacing * 2;
            }
        }

        void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.indentLevel++;
            SerializedProperty element                                  = m_ReorderAbleList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty componentTypeSerializedProperty          = element.FindPropertyRelative("ComponentType");
            SerializedProperty componentTypeNameIndexSerializedProperty = element.FindPropertyRelative("ComponentTypeNameIndex");
            SerializedProperty paramProperty                            = element.FindPropertyRelative("Params");

            // SerializedProperty appIdSerializedProperty = element.FindPropertyRelative("AppId");
            // SerializedProperty channelIdSerializedProperty = element.FindPropertyRelative("ChannelId");
            // SerializedProperty channelSerializedProperty = element.FindPropertyRelative("Channel");
            // SerializedProperty appKeySerializedProperty = element.FindPropertyRelative("AppKey");
            // SerializedProperty secretKeySerializedProperty = element.FindPropertyRelative("SecretKey");
            // SerializedProperty componentTypeSerializedProperty = element.FindPropertyRelative("ComponentType");
            // SerializedProperty componentTypeNameIndexSerializedProperty = element.FindPropertyRelative("ComponentTypeNameIndex");
            //
            // EditorGUI.PropertyField(rect, appIdSerializedProperty, m_AppIdGUIContent, true);
            // rect.y += EditorGUIUtility.singleLineHeight + 6;
            // EditorGUI.PropertyField(rect, channelIdSerializedProperty, m_ChannelIdGUIContent, true);
            // rect.y += EditorGUIUtility.singleLineHeight + 6;
            // EditorGUI.PropertyField(rect, channelSerializedProperty, m_ChannelGUIContent, true);
            // rect.y += EditorGUIUtility.singleLineHeight + 6;
            // EditorGUI.PropertyField(rect, appKeySerializedProperty, m_AppKeyGUIContent, true);
            // rect.y += EditorGUIUtility.singleLineHeight + 6;
            // EditorGUI.PropertyField(rect, secretKeySerializedProperty, m_SecretKeyGUIContent, true);
            // rect.y += EditorGUIUtility.singleLineHeight + 6;

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.PrefixLabel(rect, m_ComponentTypeGUIContent);
                rect.x                                            += EditorGUIUtility.labelWidth - 14;
                componentTypeNameIndexSerializedProperty.intValue =  EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), componentTypeNameIndexSerializedProperty.intValue, m_ManagerTypeNames);
                componentTypeSerializedProperty.stringValue       =  m_ManagerTypeNames[componentTypeNameIndexSerializedProperty.intValue];
                rect.y                                            += EditorGUIUtility.singleLineHeight + 6;

                rect.x -= EditorGUIUtility.labelWidth - 14;
                EditorGUI.PropertyField(rect, paramProperty, true);
                rect.y += EditorGUIUtility.singleLineHeight * paramProperty.arraySize + 6;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }
    }
}