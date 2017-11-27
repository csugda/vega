using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.TreeModel
{
	[Serializable]
	public class TreeElement
	{
		[SerializeField] int _ID;
		[SerializeField] string _Name;
		[SerializeField] int _Depth;
		[NonSerialized] TreeElement _Parent;
		[NonSerialized] List<TreeElement> _Children;

		public int depth
		{
			get { return _Depth; }
			set { _Depth = value; }
		}

		public TreeElement parent
		{
			get { return _Parent; }
			set { _Parent = value; }
		}

		public List<TreeElement> children
		{
			get { return _Children; }
			set { _Children = value; }
		}

		public bool hasChildren
		{
			get { return children != null && children.Count > 0; }
		}

		public string name
		{
			get { return _Name; } set { _Name = value; }
		}

		public int id
		{
			get { return _ID; } set { _ID = value; }
		}

		public TreeElement ()
		{
		}

		public TreeElement (string name, int depth, int id)
		{
			_Name = name;
			_ID = id;
			_Depth = depth;
		}
	}

}
