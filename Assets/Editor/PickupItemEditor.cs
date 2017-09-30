using UnityEditor;
using UnityEngine;
using Assets.Scripts.InventoryScripts;
using System.Collections;
using System;

[CustomEditor(typeof(PickupItem)), CanEditMultipleObjects]
public class PickupItemEditor : Editor
{

    public SerializedProperty
        type_prop,
        go_prop,
        heal_prop,
        weapon_prop;
       //image_prop,
       //  heal_prop;

    void OnEnable()
    {
        type_prop = serializedObject.FindProperty("itemTypeChoice");
        go_prop = serializedObject.FindProperty("inventoryGO"); 
        // Setup the SerializedProperties
        heal_prop = serializedObject.FindProperty("healItem");
        weapon_prop = serializedObject.FindProperty("weaponItem");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(go_prop);
        EditorGUILayout.PropertyField(type_prop);

        PickupItem.ItemType ty = (PickupItem.ItemType)type_prop.enumValueIndex;

        switch (ty)
        {
            case PickupItem.ItemType.HealItem:
                EditorGUILayout.PropertyField(heal_prop, new GUIContent("Heal Item"));

                break;
            case PickupItem.ItemType.Weapon:
                EditorGUILayout.PropertyField(heal_prop, new GUIContent("Weapon"));
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}