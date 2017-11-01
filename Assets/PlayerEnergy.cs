using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnergyEvent : UnityEvent<int> { }
public class PlayerEnergy : MonoBehaviour {
    public int Energy { get; private set; }
    public static EnergyEvent onEnergyChanged = new EnergyEvent();
    public static EnergyEvent ChangeEnergy = new EnergyEvent();
    // Use this for initialization
    void Start () {
        Energy = 0;
        ChangeEnergy.AddListener(OnEnergyChanged);
        ChangeEnergy.Invoke(0);
        onEnergyChanged.Invoke(Energy);

    }

    private void OnEnergyChanged(int ammount)
    {
        Energy = Energy + ammount < 0 ? 0 : Energy + ammount > 100 ? 100 : Energy + ammount;
        onEnergyChanged.Invoke(Energy);

    }

}
