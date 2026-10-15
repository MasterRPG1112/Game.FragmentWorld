using UnityEngine;

namespace Jain
{
    public class BossPattern1 : MonoBehaviour
    {
        public float speed = 3.5f;

        void Start()
        {
            Destroy(gameObject, 1.0f);
        }

        void Update()
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }
    }
}
