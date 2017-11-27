using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine.AI;
using UnityEngine;

namespace UnityEditor.AI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavMeshModifierVolume))]
    class NavMeshModifierVolumeEditor : Editor
    {
        SerializedProperty _AffectedAgents;
        SerializedProperty _Area;
        SerializedProperty _Center;
        SerializedProperty _Size;

        static Color s_HandleColor = new Color(187f, 138f, 240f, 210f) / 255;
        static Color s_HandleColorDisabled = new Color(187f * 0.75f, 138f * 0.75f, 240f * 0.75f, 100f) / 255;

        static int s_HandleControlIDHint = typeof(NavMeshModifierVolumeEditor).Name.GetHashCode();
        BoxBoundsHandle _BoundsHandle = new BoxBoundsHandle(s_HandleControlIDHint);

        bool editingCollider
        {
            get { return EditMode.editMode == EditMode.SceneViewEditMode.Collider && EditMode.IsOwner(this); }
        }

        void OnEnable()
        {
            _AffectedAgents = serializedObject.FindProperty("_AffectedAgents");
            _Area = serializedObject.FindProperty("_Area");
            _Center = serializedObject.FindProperty("_Center");
            _Size = serializedObject.FindProperty("_Size");

            NavMeshVisualizationSettings.showNavigation++;
        }

        void OnDisable()
        {
            NavMeshVisualizationSettings.showNavigation--;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            InspectorEditButtonGUI();

            EditorGUILayout.PropertyField(_Size);
            EditorGUILayout.PropertyField(_Center);

            NavMeshComponentsGUIUtility.AreaPopup("Area Type", _Area);
            NavMeshComponentsGUIUtility.AgentMaskPopup("Affected Agents", _AffectedAgents);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        static void RenderBoxGizmo(NavMeshModifierVolume navModifier, GizmoType gizmoType)
        {
            var color = navModifier.enabled ? s_HandleColor : s_HandleColorDisabled;
            var colorTrans = new Color(color.r * 0.75f, color.g * 0.75f, color.b * 0.75f, color.a * 0.15f);

            var oldColor = Gizmos.color;
            var oldMatrix = Gizmos.matrix;

            Gizmos.matrix = navModifier.transform.localToWorldMatrix;

            Gizmos.color = colorTrans;
            Gizmos.DrawCube(navModifier.center, navModifier.size);

            Gizmos.color = color;
            Gizmos.DrawWireCube(navModifier.center, navModifier.size);

            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;

            Gizmos.DrawIcon(navModifier.transform.position, "NavMeshModifierVolume Icon", true);
        }

        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
        static void RenderBoxGizmoNotSelected(NavMeshModifierVolume navModifier, GizmoType gizmoType)
        {
            if (NavMeshVisualizationSettings.showNavigation > 0)
            {
                var color = navModifier.enabled ? s_HandleColor : s_HandleColorDisabled;
                var oldColor = Gizmos.color;
                var oldMatrix = Gizmos.matrix;

                Gizmos.matrix = navModifier.transform.localToWorldMatrix;

                Gizmos.color = color;
                Gizmos.DrawWireCube(navModifier.center, navModifier.size);

                Gizmos.matrix = oldMatrix;
                Gizmos.color = oldColor;
            }

            Gizmos.DrawIcon(navModifier.transform.position, "NavMeshModifierVolume Icon", true);
        }

        void InspectorEditButtonGUI()
        {
            var navModifier = (NavMeshModifierVolume)target;
            var bounds = new Bounds(navModifier.transform.position, navModifier.size);

            EditMode.DoEditModeInspectorModeButton(
                EditMode.SceneViewEditMode.Collider,
                "Edit Volume",
                EditorGUIUtility.IconContent("EditCollider"),
                bounds,
                this
                );
        }

        void OnSceneGUI()
        {
            if (!editingCollider)
                return;

            var vol = (NavMeshModifierVolume)target;
            var color = vol.enabled ? s_HandleColor : s_HandleColorDisabled;
            using (new Handles.DrawingScope(color, vol.transform.localToWorldMatrix))
            {
                _BoundsHandle.center = vol.center;
                _BoundsHandle.size = vol.size;

                EditorGUI.BeginChangeCheck();
                _BoundsHandle.DrawHandle();
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(vol, "Modified NavMesh Modifier Volume");
                    Vector3 center = _BoundsHandle.center;
                    Vector3 size = _BoundsHandle.size;
                    vol.center = center;
                    vol.size = size;
                    EditorUtility.SetDirty(target);
                }
            }
        }

        [MenuItem("GameObject/AI/NavMesh Modifier Volume", false, 2001)]
        static public void CreateNavMeshModifierVolume(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var go = NavMeshComponentsGUIUtility.CreateAndSelectGameObject("NavMesh Modifier Volume", parent);
            go.AddComponent<NavMeshModifierVolume>();
            var view = SceneView.lastActiveSceneView;
            if (view != null)
                view.MoveToView(go.transform);
        }
    }
}
