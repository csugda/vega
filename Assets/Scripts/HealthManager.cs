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

        public void Start()
        {
            onHealthChanged.AddListener(ChangeHealth);
            UpdateHelthDisplay();
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
            UpdateHelthDisplay();
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
            UpdateHelthDisplay();
        }

        private void UpdateHelthDisplay()
        {
            healthUI.GetComponent<Text>().text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }
}
