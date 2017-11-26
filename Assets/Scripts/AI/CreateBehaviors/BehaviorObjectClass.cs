using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Behavior", menuName = "AI/Behavior", order = 2)]
public class BehaviorObjectClass : ScriptableObject
{
    public string objectName = "New Behavior";
    public bool colorIsRandom = false;
    public Color thisColor = Color.white;
    public Vector3[] spawnPoints;
}