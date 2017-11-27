using UnityEngine.AI;

namespace UnityEditor.AI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavMeshModifier))]
    class NavMeshModifierEditor : Editor
    {
        SerializedProperty _AffectedAgents;
        SerializedProperty _Area;
        SerializedProperty _IgnoreFromBuild;
        SerializedProperty _OverrideArea;

        void OnEnable()
        {
            _AffectedAgents = serializedObject.FindProperty("_AffectedAgents");
            _Area = serializedObject.FindProperty("_Area");
            _IgnoreFromBuild = serializedObject.FindProperty("_IgnoreFromBuild");
            _OverrideArea = serializedObject.FindProperty("_OverrideArea");

            NavMeshVisualizationSettings.showNavigation++;
        }

        void OnDisable()
        {
            NavMeshVisualizationSettings.showNavigation--;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_IgnoreFromBuild);

            EditorGUILayout.PropertyField(_OverrideArea);
            if (_OverrideArea.boolValue)
            {
                EditorGUI.indentLevel++;
                NavMeshComponentsGUIUtility.AreaPopup("Area Type", _Area);
                EditorGUI.indentLevel--;
            }

            NavMeshComponentsGUIUtility.AgentMaskPopup("Affected Agents", _AffectedAgents);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
