using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;

public class BehaviorTreeAssetEditor : EditorWindow
{
    public BehaviorTreeAsset behaviorTree;
    private int viewIndex = 1;

    [MenuItem("Window/Behavior Tree Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(BehaviorTreeAssetEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            behaviorTree = AssetDatabase.LoadAssetAtPath(objectPath, typeof(BehaviorTreeAsset)) as BehaviorTreeAsset;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Behavior Tree Editor", EditorStyles.boldLabel);
        if (behaviorTree != null)
        {
            if (GUILayout.Button("Show Behavior Tree"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = behaviorTree;
            }
        }
        //if (GUILayout.Button("Open Behavior Tree"))
        //{
        //    OpenBehaviorTreeAsset();
        //}
        //if (GUILayout.Button("New Behavior Tree"))
        //{
        //    EditorUtility.FocusProjectWindow();
        //    Selection.activeObject = behaviorTree;
        //}
        GUILayout.EndHorizontal();

        if (behaviorTree == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Behavior Tree", GUILayout.ExpandWidth(false)))
            {
                CreateNewBehaviorTreeAsset();
            }
            if (GUILayout.Button("Open Existing Behavior Tree", GUILayout.ExpandWidth(false)))
            {
                OpenBehaviorTreeAsset();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (behaviorTree != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < behaviorTree.behaviorTree.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Behavior", GUILayout.ExpandWidth(false)))
            {
                AddBehavior();
            }
            if (GUILayout.Button("Delete Behavior", GUILayout.ExpandWidth(false)))
            {
                DeleteBehavior(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (behaviorTree.behaviorTree == null)
                Debug.Log("wtf");
            if (behaviorTree.behaviorTree.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Behavior", viewIndex, GUILayout.ExpandWidth(false)), 1, behaviorTree.behaviorTree.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField("of   " + behaviorTree.behaviorTree.Count.ToString() + "  behaviors", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                behaviorTree.behaviorTree[viewIndex - 1].BehaviorName = EditorGUILayout.TextField("Behavior Name", behaviorTree.behaviorTree[viewIndex - 1].BehaviorName as string);
                //inventoryItemList.itemList[viewIndex - 1].itemIcon = EditorGUILayout.ObjectField("Item Icon", inventoryItemList.itemList[viewIndex - 1].itemIcon, typeof(Texture2D), false) as Texture2D;
                //inventoryItemList.itemList[viewIndex - 1].itemObject = EditorGUILayout.ObjectField("Item Object", inventoryItemList.itemList[viewIndex - 1].itemObject, typeof(Rigidbody), false) as Rigidbody;

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                //inventoryItemList.itemList[viewIndex - 1].isUnique = (bool)EditorGUILayout.Toggle("Unique", inventoryItemList.itemList[viewIndex - 1].isUnique, GUILayout.ExpandWidth(false));
                //inventoryItemList.itemList[viewIndex - 1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", inventoryItemList.itemList[viewIndex - 1].isIndestructible, GUILayout.ExpandWidth(false));
                //inventoryItemList.itemList[viewIndex - 1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", inventoryItemList.itemList[viewIndex - 1].isQuestItem, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                //inventoryItemList.itemList[viewIndex - 1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", inventoryItemList.itemList[viewIndex - 1].isStackable, GUILayout.ExpandWidth(false));
                //inventoryItemList.itemList[viewIndex - 1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", inventoryItemList.itemList[viewIndex - 1].destroyOnUse, GUILayout.ExpandWidth(false));
                //inventoryItemList.itemList[viewIndex - 1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", inventoryItemList.itemList[viewIndex - 1].encumbranceValue, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This Behavior Tree is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(behaviorTree);
        }
    }

    void CreateNewBehaviorTreeAsset()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        behaviorTree = CreateBehaviorTreeAsset.Create();
        if (behaviorTree)
        {
            behaviorTree.behaviorTree = new List<BehaviorTreeElement>();
            string relPath = AssetDatabase.GetAssetPath(behaviorTree);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenBehaviorTreeAsset()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Behavior Tree", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            behaviorTree = AssetDatabase.LoadAssetAtPath(relPath, typeof(BehaviorTreeAsset)) as BehaviorTreeAsset;
            if (behaviorTree.behaviorTree == null)
                behaviorTree.behaviorTree = new List<BehaviorTreeElement>();
            if (behaviorTree)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddBehavior()
    {
    }

    void DeleteBehavior(int index)
    {
        behaviorTree.behaviorTree.RemoveAt(index);
    }
}