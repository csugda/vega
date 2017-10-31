using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyDisplay : MonoBehaviour {
    private Text text;
    public string words;
	// Use this for initialization
	void Start () {
        text = this.gameObject.GetComponent<Text>();
        PlayerEnergy.onEnergyChanged.AddListener(UpdateText);
	}

    public void UpdateText(int number)
    {
        text.text = words + " " + number + "%";
    }
	

}
