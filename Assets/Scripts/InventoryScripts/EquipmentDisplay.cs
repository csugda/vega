using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.InventoryScripts
{
    internal class EquipmentDisplay : MonoBehaviour
    {
        public GameObject managerGO;
        private EquippedGearManager manager;
        private GameObject La, Ra, H, T, L;
        public void Start()
        {
            manager = managerGO.GetComponent<EquippedGearManager>();
            if (manager == null)
                Debug.LogError("manager is null!?");
            La = this.transform.Find("LArm").gameObject;
            Ra = this.transform.Find("RArm").gameObject;
            H = this.transform.Find("Head").gameObject;
            T = this.transform.Find("Torso").gameObject;
            L = this.transform.Find("Legs").gameObject;
            this.Redraw();
        }


        public void Redraw()
        {
            Debug.Log(manager.head);

           // La.GetComponent<Text>().text = manager.lArm.name;
           // Ra.GetComponent<Text>().text = manager.rArm.name;
            H.transform.Find("Text").GetComponent<Text>().text = manager.head.name;
            // T.GetComponent<Text>().text = manager.torso.name;
            // L.GetComponent<Text>().text = manager.legs.name;
            //TODO add popup info and sprites to this.
            H.transform.Find("InfoPanel").GetComponentInChildren<Text>().text =
                manager.head.ItemInfo;
        }
    }
}