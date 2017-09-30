using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //variables for popup menu
    public GameObject popupMenuGO;
    private Transform popupMenuCanvas;
    public int popupMenuButton; //which mouse button will activate the menu
    public bool popupMenuOpen;


    //variables for main menu
    public GameObject menuGO;
    public string inventoryButton, mapButton, menuButton;
    public bool menuOpen;
   




    /*
     * menuOptions maps a string to a string[]. the string must be an object tag and the strings in the array must be buttons in the editor.
     * these options are read in from the file "Assets/Resources/MenuOptions.txt" so any new options should be added as tags, and in the .txt
    */
    private Dictionary<string, string[]> menuOptions;

    void Start()
    {
        menuOptions = new Dictionary<string, string[]>();
        LoadMenuOptions();
        popupMenuCanvas = popupMenuGO.transform.Find("MenuCanvas");
        ClosePopupMenu();
        this.gameObject.GetComponent<MenuTabSwitcher>().CloseMenus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryButton))
        {
            menuOpen = this.gameObject.GetComponent<MenuTabSwitcher>().OpenInventoryCalled();
        }
        if (Input.GetKeyDown(mapButton))
        {
            menuOpen = this.gameObject.GetComponent<MenuTabSwitcher>().OpenMapCalled();
        }
        if (Input.GetKeyDown(menuButton))
        {
            menuOpen = this.gameObject.GetComponent<MenuTabSwitcher>().OpenMenuCalled();
        }
    }

    public void OpenMenuButtonPressed(int button, Vector3 pos, Transform target)
    {
        Debug.Log(target.name);
        if (button != popupMenuButton)
            return;
        if (popupMenuOpen)
            ClosePopupMenu();
        else
        {
            popupMenuGO.SetActive(true);

            //set menu position 
            popupMenuGO.transform.position = pos;

            //set menu buttons based on tag of clicked object
            string menuType = target.tag;
            if (menuOptions.ContainsKey(menuType))
            {
                OpenPopupMenu(menuOptions[menuType], pos, target);
            }
            else
            {
                Debug.LogError("MenuController line 52: " + menuType + " is not a valid menu option. make sure that the tag is added to the text doccument");
            }
            popupMenuOpen = true;
        }
    }

    //dynamically set which menu buttons are enabled based on the array passed in
    private void OpenPopupMenu(string[] menuType, Vector3 pos, Transform target)
    {
        foreach (string s in menuType)
        {
            Transform child = popupMenuCanvas.Find(s);
            if (child == null)
            {
                Debug.LogError("MenuController line 66: " + s + " is not a valid menu button. Did you create it in the scene?");
                continue;
            }
            else
            {
                child.gameObject.GetComponent<MenuButton>().Activate(pos, target);
            }
        }
    }


    //close menu, setting all buttons to inactive.
    public void ClosePopupMenu()
    {

        foreach (Transform child in popupMenuCanvas)
        {
            child.gameObject.SetActive(false);
        }
        popupMenuGO.SetActive(false);
        popupMenuOpen = false;
    }


    //read menu tag data from the file
    private bool LoadMenuOptions()
    {
        try
        {
            string line;
            StreamReader reader = new StreamReader("Assets/Resources/MenuOptions.txt");
            using (reader)
            {
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                        {
                            string[] options = Split(entries, 1, entries.Length);
                            menuOptions.Add(entries[0], options);
                        }
                    }
                }
                while (line != null);
                reader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }
    private string[] Split(string[] sa, int start, int end)
    {
        string[] rtn = new string[end - start];
        int j = 0;
        for (int i = start; i < end; i++)
        {
            rtn[j] = sa[i];
            ++j;
        }
        return rtn;
    }
}