using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTabSwitcher : MonoBehaviour
{
    public GameObject inventoryTab, mapTab, menuTab, menu;
    public int state = 0; //0=closed; 1=inventory; 2=map; 3=menu


    public void OpenInventoryCalled()
    {
        if (state == 1)
            CloseMenus();
        else
        {
            menu.SetActive(true);
            inventoryTab.SetActive(true);
            mapTab.SetActive(false);
            menuTab.SetActive(false);
            state = 1;
        }
    }

    public void OpenMapCalled()
    {
        if (state == 2)
            CloseMenus();
        else
        {
            menu.SetActive(true);
            inventoryTab.SetActive(false);
            mapTab.SetActive(true);
            menuTab.SetActive(false);
            state = 2;
        }
    }

    public void OpenMenuCalled()
    {
        if (state == 3)
            CloseMenus();
        else
        {
            menu.SetActive(true);
            inventoryTab.SetActive(false);
            mapTab.SetActive(false);
            menuTab.SetActive(true);
            state = 3;
        }
    }

    public void CloseMenus()
    {
        inventoryTab.SetActive(false);
        mapTab.SetActive(false);
        menuTab.SetActive(false);
        menu.SetActive(false);
        state = 0;
    }
}
