using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyDamageEvent : UnityEvent<int, Transform>  {   }

public class EnemyHealth : MonoBehaviour {
    public int health = 100;
    public EnemyDamageEvent onDammaged;
	// Use this for initialization
	void Start ()
    {
        onDammaged.AddListener(RecieveDamage);
	}
	
    private void RecieveDamage(int ammount, Transform source)
    {
        if (ammount < 0)
            throw new ArgumentException("Cannot recieve negative dammage. Source: " + source.name);
        health -= ammount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
