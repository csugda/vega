using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts.Items
{
    class DamagePlayerItem : MonoBehaviour
    {
        public int damageAmmount;

        public void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.tag == "Player")
                HealthManager.onHealthChanged.Invoke(-damageAmmount);
            Destroy(this.gameObject);
        }
    }
}

