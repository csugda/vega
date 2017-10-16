using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyDamageEvent : UnityEvent<int, Transform>  {   }

public class EnemyHealth : MonoBehaviour {
    public float maxHealth = 100f;
    public float currHealth = 0f;
    public GameObject healthBar;
    public EnemyDamageEvent onDammaged;

	// Use this for initialization
	void Start ()
    {
        onDammaged.AddListener(RecieveDamage);
        currHealth = maxHealth;
	}
	
    private void RecieveDamage(int ammount, Transform source)
    {
        if (ammount < 0)
            throw new ArgumentException("Cannot recieve negative dammage. Source: " + source.name);
        currHealth -= ammount;
        if (currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
