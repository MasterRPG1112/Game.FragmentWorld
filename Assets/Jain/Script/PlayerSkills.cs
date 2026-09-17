using UnityEngine;

namespace Jain
{
    public class PlayerSkills : MonoBehaviour
    {
        bool Q;
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
            }
        }

        void GetInput()
        {
            Q = Input.GetButtonDown("PlayerSkill_Q");
        }

        void Skill_Q()
        {
            Instantiate(PlayerSkill_Q, QskillPoint.transform.position, transform.rotation);
            Invoke("Skill_Q2", 0.2f);
        }

        void Skill_Q2()
        {
            animator.SetTrigger("IsQskill");
        }
    }
}