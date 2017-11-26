using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateBehaviorTree
{
    [MenuItem("Assets/Create/Behavior Tree")]
    public static BehaviorTree Create()
    {
        BehaviorTree asset = ScriptableObject.CreateInstance<BehaviorTree>();

        AssetDatabase.CreateAsset(asset, "Assets/AI/BehaviorTree.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}