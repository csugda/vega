using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuGO;
    private Transform menuCanvas;
    public int menuButton; //which mouse button will activate the menu
    public bool menuOpen;



    /*
     * I am defining a string array for each menu type in order to make the menu dyanmic, 
     * setting the strings will determine which menu buttons appear, as long as they are cereated and named in menuCanvas
    */
    private string[] walkMenu = { "WalkHere", "CloseMenu" };
    private String[] playerMenu = { "CloseMenu" };


    void Start()
    {
        menuCanvas = menuGO.transform.Find("MenuCanvas");
        CloseMenu();
        menuOpen = menuGO.activeSelf;
        
    }

    public void OpenMenuButtonPressed(int button, Vector3 pos, Transform target)
    {
        
        if (button != menuButton)
            return;
        if (menuOpen)
            CloseMenu();
        else
        {
            menuGO.SetActive(true);
            
            //set menu position 
            menuGO.transform.position = pos;

            //set menu buttons
            //TODO make this something that can be set in the editor
            if (target.gameObject.name == "PlayerModel")
            {
                OpenMenu(playerMenu);
            }
            else //at the moment the only options are walk or player.
            {
                OpenMenu(walkMenu); //TODO: walkmenu is the default while walk is the only interaction
            }
            
            menuOpen = true;
        }
    }

    //dyanmically set which menu buttons are enabled based on the array passed in
    private void OpenMenu(string[] menuType)
    {
        foreach (string s in menuType)
        {
            Transform child = menuCanvas.Find(s);
            if (child == null)
            {
                Debug.LogError( s + " is not a valid menu button. Did you create it in the scene?");
                continue;
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }


    //close menu, setting all buttons to inactive.
    public void CloseMenu()
    {
        
        foreach (Transform child in menuCanvas)
        {
            child.gameObject.SetActive(false);
        }
        menuGO.SetActive(false);
        menuOpen = false;
    }
}
