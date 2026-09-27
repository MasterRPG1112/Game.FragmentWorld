using UnityEngine;

namespace Jain
{
    public class Wskill1 : MonoBehaviour
    {
        public float speed = 3.5f;

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
