using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    public class PickupItem : MonoBehaviour, IInventoryItem
    {
        private GameObject _ManagerGO;
        protected GameObject ManagerGO {
            get
            {
                if (_ManagerGO == null)
                    _ManagerGO = GameObject.Find("ManagerGO");
                return _ManagerGO;
            }
            private set
            {
                _ManagerGO = value;
            }
        }
        public void Start()
        {
            ManagerGO = GameObject.Find("ManagerGO");
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
        private int _ItemPrice;
        public int ItemPrice
        {
            get
            {
                return _ItemPrice;
            }
            set
            {
                _ItemPrice = value;
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


        public virtual void OnTriggerEnter(Collider other)
        {
            if (ManagerGO == null)
                ManagerGO = GameObject.Find("ManagerGO");
            if (other.gameObject.tag == "Player")
            {
                if (ManagerGO.GetComponent<Inventory>().AddItem(this))
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
