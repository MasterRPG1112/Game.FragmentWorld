using UnityEngine;

namespace Jain
{
    public class PlayerSkills : MonoBehaviour
    {
        bool Q;
        int Qconut = 0;
        public GameObject PlayerSkill_Q;

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
            Vector3 spawnPosition = transform.position + new Vector3(1f, 0.5f, 0f);

            Instantiate(PlayerSkill_Q, spawnPosition, transform.rotation);
            animator.SetTrigger("IsQskill");
        }
    }
}
