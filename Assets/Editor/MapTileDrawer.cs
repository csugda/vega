using Assets.Scripts.Map.Map_Tiles;
using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(MapTile))]
public class IngredientDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var tileRect = new Rect(position.x+15, position.y, 185, position.height);
        var weightRect = new Rect(position.x + 205, position.y, 60, position.height);
        var typeRect = new Rect(position.x + 270, position.y, 80, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(tileRect, property.FindPropertyRelative("Tile"), GUIContent.none);
        EditorGUI.PropertyField(weightRect, property.FindPropertyRelative("Weight"), GUIContent.none);
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("Type"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}