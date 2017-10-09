using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class DropItems : MonoBehaviour
    {
        public GameObject[] itemsToDrop;
        public void OnDestroy()
        {
            foreach (GameObject o in itemsToDrop)
                Instantiate(o, this.gameObject.transform.parent);
        }
    }
}
