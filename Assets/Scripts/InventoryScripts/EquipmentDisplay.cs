using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.InventoryScripts
{
    internal class EquipmentDisplay : MonoBehaviour
    {
        public GameObject managerGO;
        private EquippedGearManager manager;
        private GameObject H;//, T, L; //La, Ra
        public void Start()
        {
            manager = managerGO.GetComponent<EquippedGearManager>();
            if (manager == null)
                Debug.LogError("manager is null!?");
            //Arms are not being implemented for now
            //La = this.transform.Find("LArm").gameObject;
            //Ra = this.transform.Find("RArm").gameObject;
            H = this.transform.Find("Head").gameObject;
            //T = this.transform.Find("Torso").gameObject;
            //L = this.transform.Find("Legs").gameObject;
            this.Redraw();
            Inventory.onInventoryChanged.AddListener(Redraw);
        }


        public void Redraw()
        {

           // La.GetComponent<Text>().text = manager.lArm.name;
           // Ra.GetComponent<Text>().text = manager.rArm.name;
            H.transform.Find("Text").GetComponent<Text>().text = manager.head.name;
            H.GetComponent<Image>().sprite = manager.head.Image;
            // T.GetComponent<Text>().text = manager.torso.name;
            // L.GetComponent<Text>().text = manager.legs.name;
            H.transform.Find("InfoPanel").GetComponentInChildren<Text>().text =
                manager.head.ItemInfo;
        }
    }
}