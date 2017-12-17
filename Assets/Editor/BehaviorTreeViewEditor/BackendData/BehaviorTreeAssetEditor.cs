using Assets.Scripts.AI;
using Assets.Scripts.AI.TreeModel;
using Assets.Scripts.AI.Components;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace BehaviorTreeViewEditor.BackEndData
{
    [CustomEditor (typeof(BehaviorTreeAsset))]
	public class BehaviorTreeAssetEditor : UnityEditor.Editor
    {
        BehaviorTreeView _TreeView;
		SearchField _SearchField;
		const string kSessionStateKeyPrefix = "TVS";

		BehaviorTreeAsset asset
		{
			get { return (BehaviorTreeAsset) target; }
		}

		void OnEnable ()
		{
			Undo.undoRedoPerformed += OnUndoRedoPerformed;

			var treeViewState = new TreeViewState();
			var jsonState = SessionState.GetString (kSessionStateKeyPrefix + asset.GetInstanceID(), "");
			if (!string.IsNullOrEmpty (jsonState))
				JsonUtility.FromJsonOverwrite (jsonState, treeViewState);

            if(asset.treeElements.Count == 0)
            {
                asset.treeElements.Add(new BehaviorTreeElement("root", -1, 0));
            }
			var treeModel = new TreeModel<BehaviorTreeElement>(asset.treeElements);
			_TreeView = new BehaviorTreeView(treeViewState, treeModel);
			_TreeView.beforeDroppingDraggedItems += OnBeforeDroppingDraggedItems;
			_TreeView.Reload ();

			_SearchField = new SearchField ();
			
			_SearchField.downOrUpArrowKeyPressed += _TreeView.SetFocusAndEnsureSelectedItem;
		}


		void OnDisable ()
		{
			Undo.undoRedoPerformed -= OnUndoRedoPerformed;

			SessionState.SetString (kSessionStateKeyPrefix + asset.GetInstanceID (), JsonUtility.ToJson (_TreeView.state));
		}

		void OnUndoRedoPerformed ()
		{
			if (_TreeView != null)
			{
				_TreeView.treeModel.SetData (asset.treeElements);
				_TreeView.Reload ();
			}
		}

		void OnBeforeDroppingDraggedItems (IList<TreeViewItem> draggedRows)
		{
			Undo.RecordObject (asset, string.Format ("Moving {0} Item{1}", draggedRows.Count, draggedRows.Count > 1 ? "s" : ""));
		}

		public override void OnInspectorGUI ()
		{
            GUILayout.Space(5f);
            ToolBar();
            GUILayout.Space(3f);

            const float topToolbarHeight = 20f;
            const float spacing = 2f;
            float totalHeight = _TreeView.totalHeight + topToolbarHeight + 2 * spacing;
            Rect rect = GUILayoutUtility.GetRect(0, 10000, 0, totalHeight);
            Rect toolbarRect = new Rect(rect.x, rect.y, rect.width, topToolbarHeight);
            Rect multiColumnTreeViewRect = new Rect(rect.x, rect.y + topToolbarHeight + spacing, rect.width, rect.height - topToolbarHeight - 2 * spacing);
            SearchBar(toolbarRect);
            DoTreeView(multiColumnTreeViewRect);
        }

		void SearchBar (Rect rect)
		{
			_TreeView.searchString = _SearchField.OnGUI(rect, _TreeView.searchString);
		}

		void DoTreeView (Rect rect)
		{
			_TreeView.OnGUI (rect);
		}

		void ToolBar ()
		{
            var style = "miniButton";
            using (new EditorGUILayout.HorizontalScope ())
			{
	            if (GUILayout.Button ("Expand All", style))
				{
					_TreeView.ExpandAll ();
				}

				if (GUILayout.Button ("Collapse All", style))
				{
					_TreeView.CollapseAll ();
				}

				GUILayout.FlexibleSpace ();

                if (GUILayout.Button("Save Behavior Tree", style))
                {
                    SessionState.SetString(kSessionStateKeyPrefix + asset.GetInstanceID(), JsonUtility.ToJson(_TreeView.state));
                    Debug.Log("Saved Behavior Tree");
                }

				if (GUILayout.Button ("Add Item", style))
				{
					Undo.RecordObject (asset, "Add Item To Asset");

					// Add item as child of selection
					var selection = _TreeView.GetSelection ();
					TreeElement parent = (selection.Count == 1 ? _TreeView.treeModel.Find (selection[0]) : null) ?? _TreeView.treeModel.root;
					int depth = parent != null ? parent.depth + 1 : 0;
					int id = _TreeView.treeModel.GenerateUniqueID ();
					var element = new BehaviorTreeElement ("Behavior " + id, depth, id);
					_TreeView.treeModel.AddElement(element, parent, 0);

					// Select newly created element
					_TreeView.SetSelection (new[] {id}, TreeViewSelectionOptions.RevealAndFrame);
				}

				if (GUILayout.Button ("Remove Item", style))
				{
					Undo.RecordObject (asset, "Remove Item From Asset");
					var selection = _TreeView.GetSelection ();
					_TreeView.treeModel.RemoveElements (selection);
				}
			}

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Add Selector", style))
            {
                //TODO

                Undo.RecordObject(asset, "Add Selector To Asset");

                // Add 'Selector' as child of selection
                var selection = _TreeView.GetSelection();
                BehaviorTreeElement parent = (selection.Count == 1 ? _TreeView.treeModel.Find(selection[0]) : null) ?? _TreeView.treeModel.root;
                int depth = parent != null ? parent.depth + 1 : 0;
                int id = _TreeView.treeModel.GenerateUniqueID();
                var element = new Selector("Selector Component", depth, id);
                _TreeView.treeModel.AddElement(element, parent, 0);

                // Select newly created element
                _TreeView.SetSelection(new[] { id }, TreeViewSelectionOptions.RevealAndFrame);

                Debug.Log("Selector: Do Me!");
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Add Sequencer", style))
            {
                //TODO
                Debug.Log("Sequencer: Do Me!");
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Add Inverter", style))
            {
                //TODO
                Debug.Log("Inverter: Do Me!");
            }
            GUILayout.Space(5);

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Behavior", style))
            {
                //AddBehavior();
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Delete Behavior", style))
            {
                Undo.RecordObject(asset, "Remove Component From Asset");
                var selection = _TreeView.GetSelection();
                _TreeView.treeModel.RemoveElements(selection);
            }

            GUILayout.EndHorizontal();

        }


		class BehaviorTreeView : TreeViewWithTreeModel<BehaviorTreeElement>
		{
			public BehaviorTreeView(TreeViewState state, TreeModel<BehaviorTreeElement> model)
				: base(state, model)
			{
				showBorder = true;
				showAlternatingRowBackgrounds = true;
			}
		}
	}
}
