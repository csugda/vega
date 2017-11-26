using UnityEngine;
using System.Collections;
using Assets.Scripts.AI;

[CreateAssetMenu(fileName = "Behavior Tree", menuName = "AI/Behavior Tree", order = 1)]
public class BehaviorTreeObjectClass : ScriptableObject
{
    public string objectName = "New Behavior Tree";
    public Behavior[] myBehaviors; 
    public bool colorIsRandom = false;
    public Color thisColor = Color.white;
    public Vector3[] spawnPoints;
}