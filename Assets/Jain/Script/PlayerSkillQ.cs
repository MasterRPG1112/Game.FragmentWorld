using UnityEngine;

namespace Jain
{
    public class PlayerSkillQ : MonoBehaviour
    {
        public float speed = 10f;
        public float destroyTime = 3f;

        bool LaunchCheck = true;

        private Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            animator.SetBool("Qskill", true);
            Invoke("Launch", 1.0f);
            Destroy(gameObject, destroyTime);
        }

        void Update()
        {
            if (LaunchCheck == false)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }

        void Launch()
        {
            LaunchCheck = false;
            animator.SetBool("Qskill", false);
        }
    }
}