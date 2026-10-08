using Codice.Client.BaseCommands.WkStatus.Printers;
using Codice.CM.Common;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;

namespace Jain
{
    public class CommonMob : MonoBehaviour
    {
        public float Hp = 25.0f;
        public float StopTime;

        public GameObject Player;

        public int type;

        Vector3 dir;

        private bool isWskill2 = false;

        void Start()
        {
        
        }

        void Update()
        {
            GameObject wSkill = GameObject.FindWithTag("Wskill2");

            //type 1 : Walk / type 2 : Idle
            if (type == 1)
            {
                if (StopTime <= 0f)
                {
                    Move();
                }
            }

            Rotation();

            if (wSkill != null && !isWskill2)
            {
                isWskill2 = true;
                Wskill2Detection();
            }
            else if (wSkill == null)
            {
                isWskill2 = false;
            }
        }

        void StopTimeReset()
        {
            StopTime = 0f;
        }

        void Move()
        {
            //AI »ç¿ë
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                float xDistance = player.transform.position.x - transform.position.x;

                if (xDistance > 0.1f)
                {
                    Vector3 dir = new Vector3(2.5f, transform.position.y, 0f);
                    transform.rotation = Quaternion.LookRotation(new Vector3(0f, 180f, 0f));
                    transform.position += dir * Time.deltaTime;
                }
                else if (xDistance < -0.1f)
                {
                    Vector3 dir = new Vector3(-2.5f, transform.position.y, 0f);
                    transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 0f));
                    transform.position += dir * Time.deltaTime;
                }
            }
        }

        void Rotation()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                float xDistance = player.transform.position.x - transform.position.x;

                if (xDistance > 0.1f)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                }
                else if (xDistance < -0.1f)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (other.CompareTag("Qskill"))
            {
                Hp -= player.damage;
                if (Hp <= 0f)
                {
                    Destroy(gameObject);
                }
            }
            if (other.CompareTag("Wskill1"))
            {
                PlayerSkills playerSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();

                Destroy(other.gameObject);

                CancelInvoke("Move");
                if (playerSkills.ReturnOfTheGuardian == 1) StopTime = 2.5f;
                else StopTime = 1.2f;
                Invoke("StopTimeReset", StopTime);
            }
            if (other.CompareTag("Eskill"))
            {
                Destroy(other.gameObject);
                Hp -= player.damage - (player.damage * 0.2f);
                if (Hp <= 0f)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Wskill2Detection()
        {
            PlayerSkills playerSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();

            CancelInvoke("Move");
            if (playerSkills.ReturnOfTheGuardian == 1) StopTime = 5.2f;
            else StopTime = 3.0f;
            Invoke("StopTimeReset", StopTime);
        }
    }
}
