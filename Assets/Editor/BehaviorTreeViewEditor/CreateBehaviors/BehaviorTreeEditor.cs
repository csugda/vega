//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Collections.Generic;
//using Assets.Scripts.AI;
//using UnityEditor.IMGUI.Controls;

//namespace BehaviorTreeViewEditor.CreateBehaviors
//{
//    public class BehaviorTreeAssetEditor : EditorWindow
//    {
//        //public BehaviorTreeAsset behaviorTree;
//        private int viewIndex = 1;
//        public BehaviorTreeAsset behaviorTree;

//        [SerializeField] TreeViewState m_TreeViewState;
//        BehaviorTreeView m_BehaviorTreeView;
//        //TreeViewItem root;

//        private string treeName = "New";

//        [MenuItem("Window/Behavior Tree Editor %#e")]
//        static void Init()
//        {
//            EditorWindow.GetWindow(typeof(BehaviorTreeAssetEditor));
//        }

//        void OnEnable()
//        {
//            if (EditorPrefs.HasKey("ObjectPath"))
//            {
//                string objectPath = EditorPrefs.GetString("ObjectPath");
//                //     behaviorTree = AssetDatabase.LoadAssetAtPath(objectPath, typeof(BehaviorTreeAsset)) as BehaviorTreeAsset;
//            }

//            //if (m_TreeViewState == null)
//            //    m_TreeViewState = new TreeViewState();

//            //m_BehaviorTreeView = new BehaviorTreeView(m_TreeViewState);
//        }

//        void OnGUI()
//        {
//            GUILayout.BeginHorizontal();
//            GUILayout.Label("Behavior Tree Editor", EditorStyles.boldLabel);
//            if (behaviorTree != null)
//            {
//                if (GUILayout.Button("Show Behavior Tree"))
//                {
//                    EditorUtility.FocusProjectWindow();
//                    Selection.activeObject = behaviorTree;
//                }
//            }
//            GUILayout.EndHorizontal();

//            if (behaviorTree == null)
//            {
//                GUILayout.BeginHorizontal();
//                GUILayout.Space(10);
//                //treeName = EditorGUILayout.TextField("Behavior Tree Name: ", treeName, GUILayout.MaxWidth(400));
//                if (GUILayout.Button("Create New Behavior Tree", GUILayout.ExpandWidth(false)))
//                {
//                    CreateNewBehaviorTreeAsset();
//                }
//                GUILayout.EndHorizontal();

//                GUILayout.BeginHorizontal();
//                GUILayout.Space(10);
//                if (GUILayout.Button("Open Existing Behavior Tree", GUILayout.ExpandWidth(false)))
//                {
//                    OpenBehaviorTreeAsset();
//                }
//                GUILayout.EndHorizontal();
//            }

//            GUILayout.Space(20);

//            if (behaviorTree != null)
//            {
//                GUILayout.BeginHorizontal();
//                GUILayout.Space(10);
//                if (GUILayout.Button("Add Selector", GUILayout.ExpandWidth(false)))
//                {
//                    //TODO
//                    Debug.Log("Selector: Do Me!");
//                }
//                GUILayout.Space(5);
//                if (GUILayout.Button("Add Sequencer", GUILayout.ExpandWidth(false)))
//                {
//                    //TODO
//                    Debug.Log("Sequencer: Do Me!");
//                }
//                GUILayout.Space(5);
//                if (GUILayout.Button("Add Inverter", GUILayout.ExpandWidth(false)))
//                {
//                    //TODO
//                    Debug.Log("Inverter: Do Me!");
//                }
//                GUILayout.Space(5);

//                if (GUILayout.Button("Add Behavior", GUILayout.ExpandWidth(false)))
//                {
//                    AddBehavior();
//                }
//                GUILayout.Space(5);
//                if (GUILayout.Button("Delete Behavior", GUILayout.ExpandWidth(false)))
//                {
//                    //DeleteBehavior(viewIndex - 1);
//                }

//                GUILayout.EndHorizontal();

//                if (behaviorTree.behaviorTree == null)
//                    Debug.Log("wtf");
//                if (behaviorTree.behaviorTree.Count > 0)
//                {
//                    //GUILayout.BeginHorizontal();
//                    //EditorGUILayout.LabelField("of   " + behaviorTree.behaviorTree.Count.ToString() + "  behaviors", "", GUILayout.ExpandWidth(false));
//                    //GUILayout.EndHorizontal();

//                    //behaviorTree.behaviorTree[viewIndex - 1].BehaviorName = EditorGUILayout.TextField("Behavior Name", behaviorTree.behaviorTree[viewIndex - 1].BehaviorName as string);

//                    GUILayout.Space(10);

//                    // Tree view stuff
//                    //m_BehaviorTreeView.OnGUI(new Rect(0, 0, position.width, position.height));

//                }
//                else
//                {
//                    GUILayout.Label("This Behavior Tree is Empty.");
//                }

//                if (GUI.changed)
//                {
//                    EditorUtility.SetDirty(behaviorTree);
//                }
//            }
//        }

//        void CreateNewBehaviorTreeAsset()
//        {
//            // There is no overwrite protection here!
//            // There is No "Are you sure you want to overwrite your existing object?" if it exists.
//            // This should probably get a string from the user to create a new name and pass it ...
//            viewIndex = 1;
//            //behaviorTree = CreateBehaviorTreeAsset.Create();
//            // This should probably get a string from the user to create a new name and pass it ...      

//            //behaviorTree = CreateBehaviorTree.Create(treeName);
//            if (behaviorTree)
//            {
//                behaviorTree.behaviorTree = new List<BehaviorTreeElement>();
//                string relPath = AssetDatabase.GetAssetPath(behaviorTree);
//                EditorPrefs.SetString("ObjectPath", relPath);
//            }
//            //m_BehaviorTreeView = new BehaviorTreeView(m_TreeViewState);
//            //root = m_BehaviorTreeView.BuildRoot();
//        }

//        void OpenBehaviorTreeAsset()
//        {
//            string absPath = EditorUtility.OpenFilePanel("Select Behavior Tree", "", "");
//            if (absPath.StartsWith(Application.dataPath))
//            {
//                string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
//                //behaviorTree = AssetDatabase.LoadAssetAtPath(relPath, typeof(BehaviorTreeAsset)) as BehaviorTreeAsset;
//                if (behaviorTree.behaviorTree == null)
//                    behaviorTree.behaviorTree = new List<BehaviorTreeElement>();
//                if (behaviorTree)
//                {
//                    EditorPrefs.SetString("ObjectPath", relPath);
//                }
//            }
//        }

//        void AddBehavior()
//        {
//            //Behavior newBehavior = ScriptableObject.CreateInstance<Behavior>();
//            //newBehavior.BehaviorName = "New Behavior";
//            //behaviorTree.behaviorTree.Add(newBehavior);
//            //root.AddChild();
//        }

//        void DeleteBehavior(int index)
//        {
//            behaviorTree.behaviorTree.RemoveAt(index);
//        }

//    }
//}