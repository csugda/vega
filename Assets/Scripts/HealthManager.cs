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
        public static HealthEvent onHealthChanged = new HealthEvent();
        public static HealthEvent onMaxHealthChanged = new HealthEvent();

        public void Start()
        {
            onHealthChanged.AddListener(ChangeHealth);
            onMaxHealthChanged.AddListener(ChangeMaxHealth);
            onHealthChanged.AddListener(healthUI.GetComponent<HealthBarTestScript>().DecreaseHealth);
            onMaxHealthChanged.AddListener(healthUI.GetComponent<HealthBarTestScript>().ChangeMaxHealth);
            healthUI.GetComponent<HealthBarTestScript>().curHealth = currentHealth;
            healthUI.GetComponent<HealthBarTestScript>().maxHealth = maxHealth;
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
                UpdateHelthDisplay();
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
