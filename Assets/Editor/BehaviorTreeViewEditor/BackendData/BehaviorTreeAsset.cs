using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeViewEditor.BackEndData
{
	[CreateAssetMenu (fileName = "BehaviorTreeAsset", menuName = "AI/Behavior Tree Asset", order = 1)]
	public class BehaviorTreeAsset : ScriptableObject
	{
		[SerializeField] List<BehaviorTreeElement> _TreeElements = 
            new List<BehaviorTreeElement>();

        public static BehaviorTreeAsset Create(string treeName)
        {
            BehaviorTree asset = ScriptableObject.CreateInstance<BehaviorTree>();
            string filePath = "Assets/AI/" + treeName + ".asset";

            AssetDatabase.CreateAsset(asset, filePath);
            AssetDatabase.SaveAssets();
            return asset;
        }

        internal List<BehaviorTreeElement> treeElements
		{
			get { return _TreeElements; }
			set { _TreeElements = value; }
		}
	}
}
