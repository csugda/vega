using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour {

    private int damageLevel, healthLevel, speedLevel, fireReateLevel;
    public int HealthCost { get { return Cost(healthLevel); } }
    public int DamageCost { get { return Cost(damageLevel); } }
    public int FireRateCost { get { return Cost(fireReateLevel); } }
    public int SpeedCost { get { return Cost(speedLevel); } }
    // Use this for initialization
    void Start () {
        ReadLevels();
        UpdateShopUI();
	}
    private void OnDestroy()
    {
        SaveLevels();
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
        //save the levels to override the previous document
    }
    private void UpdateShopUI()
    {

    }
    private int Cost(int l)
    {
        return (int)(l * Mathf.Pow(1.5f, l)) + 4;
    }

}
