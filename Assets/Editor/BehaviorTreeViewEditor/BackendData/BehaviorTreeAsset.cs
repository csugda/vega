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

		internal List<BehaviorTreeElement> treeElements
		{
			get { return _TreeElements; }
			set { _TreeElements = value; }
		}
	}
}
