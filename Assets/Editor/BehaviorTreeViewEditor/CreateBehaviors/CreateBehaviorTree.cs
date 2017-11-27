//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//namespace BehaviorTreeViewEditor.CreateBehaviors
//{
//    public class CreateBehaviorTree
//    {

//        public static BehaviorTree Create()
//        {
//            BehaviorTree asset = ScriptableObject.CreateInstance<BehaviorTree>();

//            AssetDatabase.CreateAsset(asset, "Assets/AI/BehaviorTree.asset");
//            AssetDatabase.SaveAssets();
//            return asset;
//        }

//        public static BehaviorTree Create(string treeName)
//        {
//            BehaviorTree asset = ScriptableObject.CreateInstance<BehaviorTree>();
//            string filePath = "Assets/AI/" + treeName + ".asset";

//            AssetDatabase.CreateAsset(asset, filePath);
//            AssetDatabase.SaveAssets();
//            return asset;
//        }
//    } 
//}