using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuButtonActions : MonoBehaviour {
    private bool muted = false;
    void Start()
    {
        AudioListener.pause = false;
    }
	public void ToggleMute()
    {
        AudioListener.pause = !muted;
        muted = !muted;
        this.transform.Find("Text").GetComponent<Text>().text = muted ? "Un-Mute" : "Mute";
    }
    public void Quit()
    {
        Application.Quit();
    }
    public GameObject Boomrang;
   public void ReloadLevel()
    {
        Instantiate(Boomrang);
        SceneManager.LoadScene("ShopScene");
    }
    

}
