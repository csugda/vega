using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boomarang : MonoBehaviour {
    
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += Reload;
    }
    private void Reload(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= Reload;
        StartCoroutine(Boomarang());
    }
    private WaitForSeconds waitforload = new WaitForSeconds(0.01f);
    private IEnumerator Boomarang()
    {
        yield return waitforload;
        SceneManager.LoadScene("LevelGenerationScene");
        Destroy(this.gameObject);
    }

}
