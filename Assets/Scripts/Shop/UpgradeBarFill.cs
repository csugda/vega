using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Shop
{
    public class UpgradeBarFill : MonoBehaviour
    {
        public GameObject emptyBar, fullBar;
        
        public UpgradeType myType;
        private void Start()
        {
            UpdateUI();
        }
        public void UpdateUI()
        {
            foreach (Transform t in this.gameObject.transform)
            {
                Destroy(t.gameObject);
            }
            int level = this.gameObject.transform.parent.parent.gameObject.GetComponent<UpgradeTracker>().GetLevel(myType);
            for (int i = 0; i < level; ++i)
            {
                Instantiate(fullBar, this.gameObject.transform);
            }
            for (int i = level; i < 10; ++i)
            {
                Instantiate(emptyBar, this.gameObject.transform);
            }
        }
    }
}
