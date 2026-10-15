using UnityEngine;

namespace Jain
{
    public class MiniBoss2_Pattern1 : MonoBehaviour
    {
        public float damage;
        void Start()
        {
            Destroy(gameObject, 0.5f);
        }

        void Update()
        {
        
        }
    }
}
