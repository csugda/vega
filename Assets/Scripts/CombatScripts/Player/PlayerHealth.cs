using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class PlayerDamageEvent : UnityEvent<int, Transform> { }

public class PlayerHealth : MonoBehaviour {
    public float maxHealth = 100f;
    public float currHealth = 0f;
    public GameObject healthBar;
    Vector3 initalScale = Vector3.one; //NOTE make sure that the local scale is actully one to begin with..
    public PlayerDamageEvent onDamaged;

    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    bool damaged;

    // Use this for initialization
    void Awake()
    {
        currHealth = maxHealth;
        onDamaged.AddListener(ReceiveDamage);
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void ReceiveDamage(int amount, Transform source)
    {
        if (amount < 0)
            throw new ArgumentException("Cannot recieve negative damage. Source: " + source.name);

        damaged = true;
        currHealth -= amount;
        healthBar.transform.localScale = new Vector3(
            Mathf.Clamp((currHealth / maxHealth), 0f, 1f) * initalScale.x, initalScale.y, initalScale.z);

        if (currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
