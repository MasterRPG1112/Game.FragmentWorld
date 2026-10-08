using UnityEngine;

namespace Jain
{
    public class Mob2_skill : MonoBehaviour
    {
        public float speed = 3.5f;
        public float damage;

        void Start()
        {
            Destroy(gameObject, 1.5f);
        }

        void Update()
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }
}

