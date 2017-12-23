using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyDamageEvent : UnityEvent<int, Transform>  {   }

public class EnemyHealth : MonoBehaviour {
    public float maxHealth = 100f;
    public float currHealth = 0f;
    public GameObject healthBar;
    Vector3 initalScale = Vector3.one; //NOTE make sure that the local scale is actully one to begin with..
    public EnemyDamageEvent onDamaged;

	// Use this for initialization
	void Awake ()
    {
        currHealth = maxHealth;
        onDamaged.AddListener(ReceiveDamage);
	}
	
    public void ReceiveDamage(int amount, Transform source)
    {
        if (amount < 0)
            throw new ArgumentException("Cannot recieve negative damage. Source: " + source.name);
        currHealth -= amount;
        healthBar.transform.localScale =
            new Vector3(Mathf.Clamp(
                (currHealth / maxHealth), 0f, 1f) * initalScale.x, initalScale.y, initalScale.z);
        if (currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
