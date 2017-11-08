using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Assets.Scripts
{
    [System.Serializable]
    public class HealthEvent : UnityEvent<int> { }
    class HealthManager : MonoBehaviour
    {
        public int currentHealth, maxHealth;
        public GameObject healthUI;
        private HealthBarTestScript healthBar;
        public static HealthEvent onHealthChanged = new HealthEvent();
        public static HealthEvent onMaxHealthChanged = new HealthEvent();

        public void Start()
        {
            healthBar = healthUI.GetComponent<HealthBarTestScript>();
            onHealthChanged.AddListener(ChangeHealth);
            onMaxHealthChanged.AddListener(ChangeMaxHealth);
            onHealthChanged.AddListener(healthBar.DecreaseHealth);
            onMaxHealthChanged.AddListener(healthBar.ChangeMaxHealth);
            healthBar.curHealth = currentHealth;
            healthBar.maxHealth = maxHealth;
            onHealthChanged.Invoke(0);
            onMaxHealthChanged.Invoke(0);
            
        }
        public void ChangeHealth(int ammount)
        {
            if (ammount > 0 && currentHealth + ammount > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (ammount < 0 && currentHealth + ammount <= 0)
            {
                //DEATH
                currentHealth = 0;
                Debug.LogError("Player Died");
                return;
            }
            currentHealth += ammount;
           // UpdateHelthDisplay();
        }

        public void ChangeMaxHealth(int ammount)
        {
            if (ammount < 0 && maxHealth + ammount < 1)
            {
                maxHealth = 1;
                currentHealth = 1;
            }
            else if (ammount < 0)
            {
                maxHealth += ammount;
                currentHealth = Math.Min(currentHealth, maxHealth);
            }
            else
            {
                maxHealth += ammount;
            }
            //UpdateHelthDisplay();
        }

        private void UpdateHelthDisplay()
        {
            healthUI.GetComponent<Text>().text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }
}
