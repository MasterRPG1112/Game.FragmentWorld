using System.Drawing;
using UnityEngine;

namespace Jain
{
    public class Mob2 : MonoBehaviour
    {
        public GameObject Mob2_skill;
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
            Instantiate(Mob2_skill, Point.transform.position, Point.transform.rotation);
            skillCooldown = 2f;
        }
    }
}
