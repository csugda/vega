using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public string s;
    public void Change()
    {
        SceneManager.LoadScene(s);
    }
}
