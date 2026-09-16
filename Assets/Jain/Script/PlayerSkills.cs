using UnityEngine;

namespace Jain
{
    public class PlayerSkills : MonoBehaviour
    {
        bool Q;
        int Qconut = 0;
        public GameObject PlayerSkill_Q;
        public GameObject QskillPoint;

        private Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            GetInput();
            if (Q == true)
            {
                Skill_Q();
                ++Qconut;
                if (Qconut > 2)
                {
                    Qconut = 0;
                    Invoke("Skill_Q", 0.5f);
                    Debug.Log("추가타 발생!");
                }
            }
        }

        void GetInput()
        {
            Q = Input.GetButtonDown("PlayerSkill_Q");
        }

        void Skill_Q()
        {
<<<<<<< HEAD
            Instantiate(PlayerSkill_Q, QskillPoint.transform.position, transform.rotation);
            Invoke("Skill_Q2", 0.2f);
        }
=======
            Vector3 convert;

            if (transform.position.x > 0f)
            {
                convert = new Vector3(1f, 0.5f, 0f);
            }
            else
            {
                convert = new Vector3(-1f, 0.5f, 0f);
            }
                Vector3 spawnPosition = transform.position + convert;
>>>>>>> 0973462fe7b83bb51a5dce7019ede7f439a8d62f

        void Skill_Q2()
        {
            animator.SetTrigger("IsQskill");
        }
    }
}