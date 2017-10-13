using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowItemInfo : MonoBehaviour {

    public Transform infoBox;
    private Transform parent;
    public GameObject parentGO;
	void Start () {
        parent = parentGO != null ? parentGO.transform : this.gameObject.transform.parent.parent;
        //parent.parent because the first parent has a gridLayoutGroup that throws everything off when you give it a new child
        infoBox.gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        if (infoBox == null)
            infoBox = this.transform.Find("InfoPanel");
        infoBox.gameObject.SetActive(false);
    }
    public void ShowInfoPannel()
    {
        if (infoBox.GetComponentInChildren<Text>().text != "") //dont show text if there is no text
        {
            infoBox.gameObject.SetActive(true);
            infoBox.SetParent(parent, true);
            infoBox.SetAsLastSibling();
        }
    }
    public void HideInfoPannel()
    {
        infoBox.gameObject.SetActive(false);
        infoBox.SetParent(this.gameObject.transform);
    }

    public void OnDestroy()
    {
        if (infoBox != null && infoBox.gameObject != null)
        Destroy(infoBox.gameObject);
    }

    
}
