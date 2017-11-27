using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

public class BehaviorTreeView : TreeView
{
    public BehaviorTreeView(TreeViewState treeViewState)
        : base(treeViewState)
    {
        Reload();
    }

    protected override TreeViewItem BuildRoot()
    {
        var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
        return root;
    }
}
