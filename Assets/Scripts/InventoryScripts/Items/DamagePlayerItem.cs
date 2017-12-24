using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts.Items
{
    class DamagePlayerItem : MonoBehaviour
    {
        public int damageAmmount;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                HealthManager.onHealthChanged.Invoke(-damageAmmount);
            Destroy(this.gameObject);
        }
    }
}

