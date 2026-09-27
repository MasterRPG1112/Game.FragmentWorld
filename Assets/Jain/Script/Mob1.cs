using Codice.Client.BaseCommands.WkStatus.Printers;
using Codice.CM.Common;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;

namespace Jain
{
    public class Mob1 : MonoBehaviour
    {
        public float Hp = 25.0f;
        public float StopTime;

        public GameObject Player;

        Vector3 dir;

        private bool isWskill2 = false;

        void Start()
        {
        
        }

        void Update()
        {
            GameObject wSkill = GameObject.FindWithTag("Wskill2");

            if (StopTime <= 0f)
            {
                Move();
            }

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
                    Vector3 dir = new Vector3(2.5f, 0f, 0f);
                    transform.position += dir * Time.deltaTime;
                }
                else if (xDistance < -0.1f)
                {
                    Vector3 dir = new Vector3(-2.5f, 0f, 0f);
                    transform.position += dir * Time.deltaTime;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Qskill"))
            {
                Hp -= 5.0f;
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
                Hp -= 4.5f;
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
