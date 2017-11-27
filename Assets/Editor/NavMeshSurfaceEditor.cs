using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine.AI;
using UnityEngine;

namespace UnityEditor.AI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavMeshSurface))]
    class NavMeshSurfaceEditor : Editor
    {
        SerializedProperty _AgentTypeID;
        SerializedProperty _BuildHeightMesh;
        SerializedProperty _Center;
        SerializedProperty _CollectObjects;
        SerializedProperty _DefaultArea;
        SerializedProperty _LayerMask;
        SerializedProperty _OverrideTileSize;
        SerializedProperty _OverrideVoxelSize;
        SerializedProperty _Size;
        SerializedProperty _TileSize;
        SerializedProperty _UseGeometry;
        SerializedProperty _VoxelSize;

        class Styles
        {
            public readonly GUIContent _LayerMask = new GUIContent("Include Layers");

            public readonly GUIContent _ShowInputGeom = new GUIContent("Show Input Geom");
            public readonly GUIContent _ShowVoxels = new GUIContent("Show Voxels");
            public readonly GUIContent _ShowRegions = new GUIContent("Show Regions");
            public readonly GUIContent _ShowRawContours = new GUIContent("Show Raw Contours");
            public readonly GUIContent _ShowContours = new GUIContent("Show Contours");
            public readonly GUIContent _ShowPolyMesh = new GUIContent("Show Poly Mesh");
            public readonly GUIContent _ShowPolyMeshDetail = new GUIContent("Show Poly Mesh Detail");
        }

        struct AsyncBakeOperation
        {
            public NavMeshSurface surface;
            public NavMeshData bakeData;
            public AsyncOperation bakeOperation;
        }

        static List<AsyncBakeOperation> s_BakeOperations = new List<AsyncBakeOperation>();

        static Styles s_Styles;

        static bool s_ShowDebugOptions;

        static Color s_HandleColor = new Color(127f, 214f, 244f, 100f) / 255;
        static Color s_HandleColorSelected = new Color(127f, 214f, 244f, 210f) / 255;
        static Color s_HandleColorDisabled = new Color(127f * 0.75f, 214f * 0.75f, 244f * 0.75f, 100f) / 255;

        static int s_HandleControlIDHint = typeof(NavMeshSurfaceEditor).Name.GetHashCode();
        BoxBoundsHandle _BoundsHandle = new BoxBoundsHandle(s_HandleControlIDHint);

        bool editingCollider
        {
            get { return EditMode.editMode == EditMode.SceneViewEditMode.Collider && EditMode.IsOwner(this); }
        }

        void OnEnable()
        {
            _AgentTypeID = serializedObject.FindProperty("_AgentTypeID");
            _BuildHeightMesh = serializedObject.FindProperty("_BuildHeightMesh");
            _Center = serializedObject.FindProperty("_Center");
            _CollectObjects = serializedObject.FindProperty("_CollectObjects");
            _DefaultArea = serializedObject.FindProperty("_DefaultArea");
            _LayerMask = serializedObject.FindProperty("_LayerMask");
            _OverrideTileSize = serializedObject.FindProperty("_OverrideTileSize");
            _OverrideVoxelSize = serializedObject.FindProperty("_OverrideVoxelSize");
            _Size = serializedObject.FindProperty("_Size");
            _TileSize = serializedObject.FindProperty("_TileSize");
            _UseGeometry = serializedObject.FindProperty("_UseGeometry");
            _VoxelSize = serializedObject.FindProperty("_VoxelSize");

            NavMeshVisualizationSettings.showNavigation++;
        }

        void OnDisable()
        {
            NavMeshVisualizationSettings.showNavigation--;
        }

        static string GetAndEnsureTargetPath(NavMeshSurface surface)
        {
            // Create directory for the asset if it does not exist yet.
            var activeScenePath = surface.gameObject.scene.path;

            var targetPath = "Assets";
            if (!string.IsNullOrEmpty(activeScenePath))
                targetPath = Path.Combine(Path.GetDirectoryName(activeScenePath), Path.GetFileNameWithoutExtension(activeScenePath));
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);
            return targetPath;
        }

        static void CreateNavMeshAsset(NavMeshSurface surface)
        {
            var targetPath = GetAndEnsureTargetPath(surface);

            var combinedAssetPath = Path.Combine(targetPath, "NavMesh-" + surface.name + ".asset");
            combinedAssetPath = AssetDatabase.GenerateUniqueAssetPath(combinedAssetPath);
            AssetDatabase.CreateAsset(surface.navMeshData, combinedAssetPath);
        }

        static NavMeshData GetNavMeshAssetToDelete(NavMeshSurface navSurface)
        {
            var prefabType = PrefabUtility.GetPrefabType(navSurface);
            if (prefabType == PrefabType.PrefabInstance || prefabType == PrefabType.DisconnectedPrefabInstance)
            {
                // Don't allow deleting the asset belonging to the prefab parent
                var parentSurface = PrefabUtility.GetPrefabParent(navSurface) as NavMeshSurface;
                if (parentSurface && navSurface.navMeshData == parentSurface.navMeshData)
                    return null;
            }
            return navSurface.navMeshData;
        }

        void ClearSurface(NavMeshSurface navSurface)
        {
            var assetToDelete = GetNavMeshAssetToDelete(navSurface);
            navSurface.RemoveData();
            navSurface.navMeshData = null;
            EditorUtility.SetDirty(navSurface);

            if (assetToDelete)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(assetToDelete));
                EditorSceneManager.MarkSceneDirty(navSurface.gameObject.scene);
            }
        }

        public override void OnInspectorGUI()
        {
            if (s_Styles == null)
                s_Styles = new Styles();

            serializedObject.Update();

            var bs = NavMesh.GetSettingsByID(_AgentTypeID.intValue);

            if (bs.agentTypeID != -1)
            {
                // Draw image
                const float diagramHeight = 80.0f;
                Rect agentDiagramRect = EditorGUILayout.GetControlRect(false, diagramHeight);
                NavMeshEditorHelpers.DrawAgentDiagram(agentDiagramRect, bs.agentRadius, bs.agentHeight, bs.agentClimb, bs.agentSlope);
            }
            NavMeshComponentsGUIUtility.AgentTypePopup("Agent Type", _AgentTypeID);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_CollectObjects);
            if ((CollectObjects)_CollectObjects.enumValueIndex == CollectObjects.Volume)
            {
                EditorGUI.indentLevel++;
                InspectorEditButtonGUI();
                EditorGUILayout.PropertyField(_Size);
                EditorGUILayout.PropertyField(_Center);
            }
            else
            {
                if (editingCollider)
                    EditMode.QuitEditMode();
            }

            EditorGUILayout.PropertyField(_LayerMask, s_Styles._LayerMask);
            EditorGUILayout.PropertyField(_UseGeometry);

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            _OverrideVoxelSize.isExpanded = EditorGUILayout.Foldout(_OverrideVoxelSize.isExpanded, "Advanced");
            if (_OverrideVoxelSize.isExpanded)
            {
                EditorGUI.indentLevel++;

                NavMeshComponentsGUIUtility.AreaPopup("Default Area", _DefaultArea);

                // Override voxel size.
                EditorGUILayout.PropertyField(_OverrideVoxelSize);

                using (new EditorGUI.DisabledScope(!_OverrideVoxelSize.boolValue || _OverrideVoxelSize.hasMultipleDifferentValues))
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PropertyField(_VoxelSize);

                    if (!_OverrideVoxelSize.hasMultipleDifferentValues)
                    {
                        if (!_AgentTypeID.hasMultipleDifferentValues)
                        {
                            float voxelsPerRadius = _VoxelSize.floatValue > 0.0f ? (bs.agentRadius / _VoxelSize.floatValue) : 0.0f;
                            EditorGUILayout.LabelField(" ", voxelsPerRadius.ToString("0.00") + " voxels per agent radius", EditorStyles.miniLabel);
                        }
                        if (_OverrideVoxelSize.boolValue)
                            EditorGUILayout.HelpBox("Voxel size controls how accurately the navigation mesh is generated from the level geometry. A good voxel size is 2-4 voxels per agent radius. Making voxel size smaller will increase build time.", MessageType.None);
                    }
                    EditorGUI.indentLevel--;
                }

                // Override tile size
                EditorGUILayout.PropertyField(_OverrideTileSize);

                using (new EditorGUI.DisabledScope(!_OverrideTileSize.boolValue || _OverrideTileSize.hasMultipleDifferentValues))
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PropertyField(_TileSize);

                    if (!_TileSize.hasMultipleDifferentValues && !_VoxelSize.hasMultipleDifferentValues)
                    {
                        float tileWorldSize = _TileSize.intValue * _VoxelSize.floatValue;
                        EditorGUILayout.LabelField(" ", tileWorldSize.ToString("0.00") + " world units", EditorStyles.miniLabel);
                    }

                    if (!_OverrideTileSize.hasMultipleDifferentValues)
                    {
                        if (_OverrideTileSize.boolValue)
                            EditorGUILayout.HelpBox("Tile size controls the how local the changes to the world are (rebuild or carve). Small tile size allows more local changes, while potentially generating more data in overal.", MessageType.None);
                    }
                    EditorGUI.indentLevel--;
                }


                // Height mesh
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(_BuildHeightMesh);
                }

                EditorGUILayout.Space();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            var hadError = false;
            var multipleTargets = targets.Length > 1;
            foreach (NavMeshSurface navSurface in targets)
            {
                var settings = navSurface.GetBuildSettings();
                // Calculating bounds is potentially expensive when unbounded - so here we just use the center/size.
                // It means the validation is not checking vertical voxel limit correctly when the surface is set to something else than "in volume".
                var bounds = new Bounds(Vector3.zero, Vector3.zero);
                if (navSurface.collectObjects == CollectObjects.Volume)
                {
                    bounds = new Bounds(navSurface.center, navSurface.size);
                }

                var errors = settings.ValidationReport(bounds);
                if (errors.Length > 0)
                {
                    if (multipleTargets)
                        EditorGUILayout.LabelField(navSurface.name);
                    foreach (var err in errors)
                    {
                        EditorGUILayout.HelpBox(err, MessageType.Warning);
                    }
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(EditorGUIUtility.labelWidth);
                    if (GUILayout.Button("Open Agent Settings...", EditorStyles.miniButton))
                        NavMeshEditorHelpers.OpenAgentSettings(navSurface.agentTypeID);
                    GUILayout.EndHorizontal();
                    hadError = true;
                }
            }

            if (hadError)
                EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(Application.isPlaying || _AgentTypeID.intValue == -1))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUIUtility.labelWidth);
                if (GUILayout.Button("Clear"))
                {
                    foreach (NavMeshSurface s in targets)
                        ClearSurface(s);
                    SceneView.RepaintAll();
                }

                if (GUILayout.Button("Bake"))
                {
                    // Remove first to avoid double registration of the callback
                    EditorApplication.update -= UpdateAsyncBuildOperations;
                    EditorApplication.update += UpdateAsyncBuildOperations;

                    foreach (NavMeshSurface surf in targets)
                    {
                        var oper = new AsyncBakeOperation();

                        oper.bakeData = InitializeBakeData(surf);
                        oper.bakeOperation = surf.UpdateNavMesh(oper.bakeData);
                        oper.surface = surf;

                        s_BakeOperations.Add(oper);
                    }
                }

                GUILayout.EndHorizontal();
            }

            // Show progress for the selected targets
            for (int i = s_BakeOperations.Count - 1; i >= 0; --i)
            {
                if (!targets.Contains(s_BakeOperations[i].surface))
                    continue;

                var oper = s_BakeOperations[i].bakeOperation;
                if (oper == null)
                    continue;

                var p = oper.progress;
                if (oper.isDone)
                {
                    SceneView.RepaintAll();
                    continue;
                }

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Cancel", EditorStyles.miniButton))
                {
                    var bakeData = s_BakeOperations[i].bakeData;
                    UnityEngine.AI.NavMeshBuilder.Cancel(bakeData);
                    s_BakeOperations.RemoveAt(i);
                }

                EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), p, "Baking: " + (int)(100 * p) + "%");
                if (p <= 1)
                    Repaint();

                GUILayout.EndHorizontal();
            }
        }

        static NavMeshData InitializeBakeData(NavMeshSurface surface)
        {
            var emptySources = new List<NavMeshBuildSource>();
            var emptyBounds = new Bounds();
            return UnityEngine.AI.NavMeshBuilder.BuildNavMeshData(surface.GetBuildSettings(), emptySources, emptyBounds
                , surface.transform.position, surface.transform.rotation);
        }

        static void UpdateAsyncBuildOperations()
        {
            foreach (var oper in s_BakeOperations)
            {
                if (oper.surface == null || oper.bakeOperation == null)
                    continue;

                if (oper.bakeOperation.isDone)
                {
                    var surface = oper.surface;
                    var delete = GetNavMeshAssetToDelete(surface);
                    if (delete != null)
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(delete));

                    surface.RemoveData();
                    surface.navMeshData = oper.bakeData;
                    if (surface.isActiveAndEnabled)
                        surface.AddData();
                    CreateNavMeshAsset(surface);
                    EditorSceneManager.MarkSceneDirty(surface.gameObject.scene);
                }
            }
            s_BakeOperations.RemoveAll(o => o.bakeOperation == null || o.bakeOperation.isDone);
            if (s_BakeOperations.Count == 0)
                EditorApplication.update -= UpdateAsyncBuildOperations;
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.Pickable)]
        static void RenderBoxGizmoSelected(NavMeshSurface navSurface, GizmoType gizmoType)
        {
            RenderBoxGizmo(navSurface, gizmoType, true);
        }

        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
        static void RenderBoxGizmoNotSelected(NavMeshSurface navSurface, GizmoType gizmoType)
        {
            if (NavMeshVisualizationSettings.showNavigation > 0)
                RenderBoxGizmo(navSurface, gizmoType, false);
            else
                Gizmos.DrawIcon(navSurface.transform.position, "NavMeshSurface Icon", true);
        }

        static void RenderBoxGizmo(NavMeshSurface navSurface, GizmoType gizmoType, bool selected)
        {
            var color = selected ? s_HandleColorSelected : s_HandleColor;
            if (!navSurface.enabled)
                color = s_HandleColorDisabled;

            var oldColor = Gizmos.color;
            var oldMatrix = Gizmos.matrix;

            // Use the unscaled matrix for the NavMeshSurface
            var localToWorld = Matrix4x4.TRS(navSurface.transform.position, navSurface.transform.rotation, Vector3.one);
            Gizmos.matrix = localToWorld;

            if (navSurface.collectObjects == CollectObjects.Volume)
            {
                Gizmos.color = color;
                Gizmos.DrawWireCube(navSurface.center, navSurface.size);

                if (selected && navSurface.enabled)
                {
                    var colorTrans = new Color(color.r * 0.75f, color.g * 0.75f, color.b * 0.75f, color.a * 0.15f);
                    Gizmos.color = colorTrans;
                    Gizmos.DrawCube(navSurface.center, navSurface.size);
                }
            }
            else
            {
                if (navSurface.navMeshData != null)
                {
                    var bounds = navSurface.navMeshData.sourceBounds;
                    Gizmos.color = Color.grey;
                    Gizmos.DrawWireCube(bounds.center, bounds.size);
                }
            }

            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;

            Gizmos.DrawIcon(navSurface.transform.position, "NavMeshSurface Icon", true);
        }

        void InspectorEditButtonGUI()
        {
            var navSurface = (NavMeshSurface)target;
            var bounds = new Bounds(navSurface.transform.position, navSurface.size);

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

            var navSurface = (NavMeshSurface)target;
            var color = navSurface.enabled ? s_HandleColor : s_HandleColorDisabled;
            var localToWorld = Matrix4x4.TRS(navSurface.transform.position, navSurface.transform.rotation, Vector3.one);
            using (new Handles.DrawingScope(color, localToWorld))
            {
                _BoundsHandle.center = navSurface.center;
                _BoundsHandle.size = navSurface.size;

                EditorGUI.BeginChangeCheck();
                _BoundsHandle.DrawHandle();
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(navSurface, "Modified NavMesh Surface");
                    Vector3 center = _BoundsHandle.center;
                    Vector3 size = _BoundsHandle.size;
                    navSurface.center = center;
                    navSurface.size = size;
                    EditorUtility.SetDirty(target);
                }
            }
        }

        [MenuItem("GameObject/AI/NavMesh Surface", false, 2000)]
        public static void CreateNavMeshSurface(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var go = NavMeshComponentsGUIUtility.CreateAndSelectGameObject("NavMesh Surface", parent);
            go.AddComponent<NavMeshSurface>();
            var view = SceneView.lastActiveSceneView;
            if (view != null)
                view.MoveToView(go.transform);
        }
    }
}
