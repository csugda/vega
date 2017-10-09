using UnityEngine;

namespace Assets.Scripts
{
    class RangedWeapon : MonoBehaviour
    {
        int damage;
        float range;
        public void Fire()
        {
            //probablly want some kind of targeting somewhere other then "this.transform.forward"
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit, range) && hit.collider.gameObject.tag.Equals("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().onDammaged.Invoke(damage, this.transform);
            }
        }
    }
}
