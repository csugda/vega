using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class UpgradeTextUpdate : MonoBehaviour
    {
        public string baseText;
        public UpgradeType type;
        private UpgradeTracker tracker;
        private Text text;
        private void Start()
        {
            //this is on a text under a button, so its parent is the button that needs to update it
            this.gameObject.transform.parent.gameObject.GetComponent<Button>().onClick.AddListener(this.RefreshText);

            tracker = GameObject.Find("Upgrades").GetComponent<UpgradeTracker>();
            text = this.gameObject.GetComponent<Text>();
            RefreshText();
        }
        public void RefreshText()
        {
            switch (type)
            {
                case UpgradeType.Damage:
                    //TODO this is the spot to grey out the buttons when fully upgraded. 
                    text.text = baseText + "\n" + tracker.DamageCost;
                    break;
                case UpgradeType.FireRate:
                    text.text = baseText + "\n" + tracker.FireRateCost;
                    break;
                case UpgradeType.Health:
                    text.text = baseText + "\n" + tracker.HealthCost;
                    break;
                case UpgradeType.Speed:
                    text.text = baseText + "\n" + tracker.SpeedCost;
                    break;
            }

        }
    }
}
