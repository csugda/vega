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
            ReadMaxHealth();
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
        private void ReadMaxHealth()
        {
            if (GameObject.Find("SHOP_ITEM_CARRYOVER"))
            {
                ItemCarryover carry = GameObject.Find("SHOP_ITEM_CARRYOVER").GetComponent<ItemCarryover>();
                int bonushealth = 0;
                for (int i = carry.upgradeLevels[0]; i > 0; --i)
                    bonushealth += 10 * i;
                maxHealth = bonushealth + 100;
                carry.Finished();
            }
            else
            {
                Debug.LogWarning("SHOP_ITEM_CARRYOVER not present in scene, most likely game was not launched from shop scene. " +
                    "\nUsing default health ammount");
                maxHealth = 100;
            }
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
