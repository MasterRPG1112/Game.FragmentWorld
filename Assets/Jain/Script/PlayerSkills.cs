using UnityEngine;

namespace Jain
{
    public class PlayerSkills : MonoBehaviour
    {
        bool Q;
        public GameObject PlayerSkill_Q;


        void Start()
        {
        
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
            Instantiate(PlayerSkill_Q, transform.position, transform.rotation);
        }
    }
}
