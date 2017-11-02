using System;

using UnityEngine;

public class MenuTabSwitcher : MonoBehaviour
{
    public GameObject inventoryTab, mapTab, journalTab, enemyTab, charecterTab, menu;
    public int state = 0; //0=closed; 1=inventory; 2=map; 3=Journal; 4=Enemies; 5=Charecters

    public bool ToggleTab(string tab)
    {
        bool closed = OpenTab(tab, true);
        Time.timeScale = closed ? 0 : 1;
        return closed;
    }
    public void ToggleTabV(string tab)
    {
        ToggleTab(tab);
    }
    public void OpenTab(string tab)
    {
        OpenTab(tab, false);
    }
    public void CloseMenu()
    {
        if (state != 0)
            OpenTab(state.ToString(), true);
        Time.timeScale = 1;
    }
    private bool OpenTab(string tab, bool closeOnRepeat)
    {
        menu.SetActive(true);
        inventoryTab.SetActive(false);
        mapTab.SetActive(false);
        journalTab.SetActive(false);
        enemyTab.SetActive(false);
        charecterTab.SetActive(false);
        switch (tab)
        {
            case ("0"):
                menu.SetActive(false);
                return true;
            case ("1"):
                if (state == 1 && closeOnRepeat)
                {
                    state = 0;
                    menu.SetActive(false);
                    return false;
                }
                else
                {
                    inventoryTab.SetActive(true);
                    state = 1;
                    return true;
                }
            case ("2"):
                if (state == 2 && closeOnRepeat)
                {
                    state = 0;
                    menu.SetActive(false);
                    return false;
                }
                else
                {
                    mapTab.SetActive(true);
                    state = 2;
                    return true;
                }
            case ("3"):
                if (state == 3 && closeOnRepeat)
                {
                    state = 0;
                    menu.SetActive(false);
                    return false;
                }
                else
                {
                    journalTab.SetActive(true);
                    state = 3;
                    return true;
                }
            case ("4"):
                if (state == 4 && closeOnRepeat)
                {
                    state = 0;
                    menu.SetActive(false);
                    return false;
                }
                else
                {
                    enemyTab.SetActive(true);
                    state = 4;
                    return true;
                }
            case ("5"):
                if (state == 5 && closeOnRepeat)
                {
                    state = 0;
                    menu.SetActive(false);
                    return false;
                }
                else
                {
                    charecterTab.SetActive(true);
                    state = 5;
                    return true;
                }
            default:
                throw new ArgumentException("" + tab + " is not a valit tab input: valid inputs are '1' '2' '3' '4' and '5'");
        }
    }
}
