using System;
using UnityEngine;

namespace Assets.Scripts
{
    class DamagePlayer : MonoBehaviour
    {
        public int damageAmmount;
        
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
                HealthManager.onHealthChanged.Invoke(-damageAmmount);
            Destroy(this.gameObject);
        }
        
    }
}

