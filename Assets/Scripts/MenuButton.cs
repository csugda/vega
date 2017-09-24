using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class MenuButtonEvent : UnityEvent<Vector3, Transform>
{
}
public class MenuButton : MonoBehaviour {

    void Start()
    {
        Button b = this.gameObject.GetComponent<Button>();
        b.onClick.AddListener(ButtonClicked);
    }

    public MenuButtonEvent Clicked;
    private Transform target;
    private Vector3 location;

    public bool Activate(Vector3 loc, Transform tar)
    {
        this.gameObject.SetActive(true);
        this.target = tar;
        this.location = loc;
        return true;
    }
	
    public void ButtonClicked()
    {
        Clicked.Invoke(location, target);
    }
    
}
