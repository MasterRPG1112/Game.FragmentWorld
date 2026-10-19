using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jain
{
    public class CommonMob : MonoBehaviour
    {
        public float maxHp = 100f; // 기본값 명시 지정
        public float Hp = 100f;
        public float StopTime;

        public GameObject Player;
        public int type;

        private bool isWskill2 = false;
        private bool isDead = false;

        void Awake()
        {
            // Start보다 빠른 Awake에서 체력이 0 이하인 경우 maxHp로 강제 설정
            if (maxHp <= 0f) maxHp = 100f;
            if (Hp <= 0f) Hp = maxHp;
        }

        void Start()
        {
            if (Hp <= 0f) Hp = maxHp;
        }

        void Update()
        {
            if (isDead) return;

            GameObject wSkill = GameObject.FindWithTag("Wskill2");

            if (type == 1 && StopTime <= 0f)
            {
                Move();
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                float xDistance = player.transform.position.x - transform.position.x;

                if (xDistance > 0.1f)
                {
                    Vector3 dir = new Vector3(2.5f, 0f, 0f);
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                    transform.position += dir * Time.deltaTime;
                }
                else if (xDistance < -0.1f)
                {
                    Vector3 dir = new Vector3(-2.5f, 0f, 0f);
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
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
            if (isDead) return;

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");

            if (playerObj == null || gameManagerObj == null) return;

            Player player = playerObj.GetComponent<Player>();
            GameManager gameManager = gameManagerObj.GetComponent<GameManager>();

            if (player == null) return;

            float currentDamage = (SceneManager.GetActiveScene().name == "World2")
                ? player.damage - (player.damage / 4f)
                : player.damage;

            if (other.CompareTag("Qskill"))
            {
                Destroy(other.gameObject);
                TakeDamage(currentDamage, gameManager);
            }
            else if (other.CompareTag("Wskill1"))
            {
                PlayerSkills playerSkills = playerObj.GetComponent<PlayerSkills>();
                Destroy(other.gameObject);

                if (playerSkills != null)
                {
                    CancelInvoke("Move");
                    StopTime = (playerSkills.ReturnOfTheGuardian == 1) ? 2.5f : 1.2f;
                    Invoke("StopTimeReset", StopTime);
                }
            }
            else if (other.CompareTag("Eskill"))
            {
                Destroy(other.gameObject);
                TakeDamage(currentDamage * 0.8f, gameManager);
            }
        }

        public void TakeDamage(float amount, GameManager gameManager)
        {
            if (isDead) return;

            // 들어온 데미지가 0 이하인 오작동 데미지 무시
            if (amount <= 0f) return;

            Hp -= amount;

            if (Hp <= 0f)
            {
                Hp = 0f;
                isDead = true; // 다중 실행 원천 차단

                bool isBossObject = (GetComponent<Boss>() != null || GetComponent<MiniBoss2>() != null);

                if (!isBossObject)
                {
                    if (gameManager != null)
                    {
                        gameManager.mobKill += 1;

                        // 디버그 확인용 (콘솔창 확인)
                        Debug.Log($"[정상 처치] 몬스터 파괴됨! mobKill: {gameManager.mobKill}");

                        gameManager.RandomSpawn();

                        if (GameDataManager.instance != null)
                        {
                            GameDataManager.instance.mobKill = gameManager.mobKill;
                            GameDataManager.instance.SaveData();
                        }
                    }

                    Destroy(gameObject);
                }
            }
        }

        public void Wskill2Detection()
        {
            if (isDead) return;

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj == null) return;

            PlayerSkills playerSkills = playerObj.GetComponent<PlayerSkills>();

            if (playerSkills != null)
            {
                CancelInvoke("Move");
                StopTime = (playerSkills.ReturnOfTheGuardian == 1) ? 5.2f : 3.0f;
                Invoke("StopTimeReset", StopTime);
            }
        }
    }
}