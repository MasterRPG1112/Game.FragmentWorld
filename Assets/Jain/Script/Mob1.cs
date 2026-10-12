using UnityEngine;

namespace Jain
{
    public class Mob1 : MonoBehaviour
    {
        public GameObject Mob1_skill;
        public GameObject Point;
        public float skillCooldown;

        void Start()
        {

        }

        void Update()
        {
            if (skillCooldown <= 0)
            {
                Skill();
            }
            CooldownMin();
        }

        void CooldownMin()
        {
            if (skillCooldown > 0f)
            {
                skillCooldown -= Time.deltaTime;
            }
        }

        void Skill()
        {
            Instantiate(Mob1_skill, Point.transform.position, Point.transform.rotation);
            skillCooldown = 1.2f;
        }
    }
}
