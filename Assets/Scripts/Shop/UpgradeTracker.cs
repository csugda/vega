using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Assets.Scripts.Shop
{
    public class UpgradeTracker : MonoBehaviour
    {

        public int damageLevel, healthLevel, speedLevel, fireReateLevel;
        public int HealthCost { get { return Cost(healthLevel); } }
        public int DamageCost { get { return Cost(damageLevel); } }
        public int FireRateCost { get { return Cost(fireReateLevel); } }
        public int SpeedCost { get { return Cost(speedLevel); } }
        // Use this for initialization
        void Awake()
        {
            ReadLevels();
        }
        private void OnDestroy()
        {
            SaveUpgradesToCarryover();
            SaveLevels();
        }

        private void SaveUpgradesToCarryover()
        {
            ItemCarryover carry = GameObject.Find("SHOP_ITEM_CARRYOVER").GetComponent<ItemCarryover>();
            carry.upgradeLevels[0] = healthLevel;
            carry.upgradeLevels[1] = speedLevel;
            carry.upgradeLevels[2] = damageLevel;
            carry.upgradeLevels[3] = fireReateLevel;
        }

        private void ReadLevels()
        {
            try
            {
                string line;
                StreamReader reader = new StreamReader("Assets/Resources/Upgrades.txt");
                using (reader)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        Debug.LogError("No upgrade info saved at 'Assets/Resources/Upgrades.txt'");
                    string[] temp = line.Split(';');
                    healthLevel = int.Parse(temp[0]);
                    speedLevel = int.Parse(temp[1]);
                    damageLevel = int.Parse(temp[2]);
                    fireReateLevel = int.Parse(temp[3]);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }
        }
        private void SaveLevels()
        {
            string path = "Assets/Resources/Upgrades.txt";

            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine(healthLevel + ";" + speedLevel + ";" + damageLevel + ";" + fireReateLevel);
            writer.Close();
        }

        private int Cost(int l)
        {
            return (int)(l * Mathf.Pow(1.5f, l)) + 4;
        }
        public int GetLevel(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.Health:
                    return healthLevel;
                case UpgradeType.Speed:
                    return speedLevel;
                case UpgradeType.FireRate:
                    return fireReateLevel;
                case UpgradeType.Damage:
                    return damageLevel;
                default:
                    throw new ArgumentException(type.ToString() + " is not a valid UpgrdeType. How did it even get in here?");
            }
        }
        public void UpgradeHealth()
        {
            if (healthLevel >= 10)
            {
                healthLevel = 10;
                Debug.Log("upgrade level max is 10. need to make the button change to show that.");
            }
            else
            healthLevel++;
        }
        public void UpgradeFireRate()
        {
            if (fireReateLevel >= 10)
            {
                fireReateLevel = 10;
                Debug.Log("upgrade level max is 10. need to make the button change to show that.");
            }
            else
            fireReateLevel++;
        }
        public void UpgradeDamage()
        {
            if (damageLevel>= 10)
            {
                damageLevel = 10;
                Debug.Log("upgrade level max is 10. need to make the button change to show that.");
            }
            else
                damageLevel++;
        }
        public void UpgradeSpeed()
        {
            if (speedLevel >= 10)
            {
                speedLevel = 10;
                Debug.Log("upgrade level max is 10. need to make the button change to show that.");
            }
            else
                speedLevel++;
        }
        public void ResetLevels()
        {
            healthLevel = 0;
            fireReateLevel = 0;
            damageLevel = 0;
            speedLevel = 0;
        }

        
    }
}