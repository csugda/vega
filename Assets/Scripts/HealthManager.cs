using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class HealthManager : MonoBehaviour
    {
        public int currentHealth, maxHealth;
        public GameObject healthUI;


        public void Start()
        {
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
                Debug.LogError("Player Died");
                currentHealth = 0;
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
