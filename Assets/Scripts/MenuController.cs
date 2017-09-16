using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuGO;
    private Transform menuCanvas;
    public int menuButton; //which mouse button will activate the menu
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
        menuCanvas = menuGO.transform.Find("MenuCanvas");
        CloseMenu();
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

            //set menu buttons based on tag of clicked object
            string menuType = target.tag;
            if (menuOptions.ContainsKey(menuType))
            {
                OpenMenu(menuOptions[menuType]);
            }
            else
            {
                Debug.LogError("MenuController line 52: " + menuType + " is not a valid menu option. make sure that the tag is added to the text doccument");
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
                Debug.LogError("MenuController line 66: " + s + " is not a valid menu button. Did you create it in the scene?");
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
            Console.WriteLine("{0}\n", e.Message);
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