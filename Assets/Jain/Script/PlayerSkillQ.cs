using UnityEngine;

namespace Jain
{
    public class PlayerSkillQ : MonoBehaviour
    {
        public float speed = 10f;
        public float destroyTime = 3f;

        private Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            Destroy(gameObject, destroyTime);
        }

        void Update()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}