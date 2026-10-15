using UnityEngine;

namespace Jain
{
    public class MiniBoss1_Pattern2 : MonoBehaviour
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
