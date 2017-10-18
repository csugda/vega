using UnityEngine;

namespace Assets.Scripts
{
    class MeleeWeapon : MonoBehaviour
    {
        public int damage = 25;
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyHealth>().onDammaged.Invoke(damage, this.transform); 
            }
        }
    }
}
