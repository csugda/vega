using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    public class PickupItem : MonoBehaviour, IInventoryItem
    {
        GameObject InventoryGO;
        void Start()
        {
            InventoryGO = GameObject.Find("InventoryGO");
        }
        [SerializeField]
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        [SerializeField]
        private int _StackSize;
        public int StackSize
        {
            get
            {
                return _StackSize;
            }
            set
            {
                _StackSize = value;
            }
        }

        [SerializeField]
        private Sprite _Image;
        public Sprite Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }

        [SerializeField]
        [TextArea(3, 10)]
        private String _ItemInfo;
        public String ItemInfo
        {
            get
            {
                return _ItemInfo;
            }
            set
            {
                _ItemInfo = value;
            }
        }


        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                InventoryGO.GetComponent<Inventory>().AddItem(this);
                Destroy(this.gameObject);
            }
        }

        public virtual void OnItemUsed()
        {
            throw new NotImplementedException();
        }

        public virtual bool Equals(IInventoryItem other)
        {
            return this.Name == other.Name;
        }
    }
}
