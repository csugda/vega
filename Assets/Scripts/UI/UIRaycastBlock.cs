using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycastBlock : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click detected on " + this.gameObject.name);
    }
}
