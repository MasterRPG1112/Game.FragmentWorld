using PlasticGui.WorkspaceWindow.Attributes;
using UnityEngine;
using UnityEngine.Assemblies;
using UnityEngine.SceneManagement;

namespace Jain
{
    public class Player : MonoBehaviour
    {
        //이동 변수
        float moveX;
        Vector3 moveVec;
        Vector3 RotVec;

        //스탯 변수 - 기본 값 hp 100 / damage 5 / miniBossItem1 0 / miniBossItem2 0
        public float hp;
        public float damage;
        public float speed;
        public int miniBossItem1;
        public int miniBossItem2;

        //점프 변수
        bool jump;
        bool isJump;
        public float jumpPower;

        //준보스1 패턴1 관련
        public bool cancel = true;

        Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            GameDataManager.instance.LoadDate();
            transform.position = GameDataManager.instance.PlayerTransform;
            miniBossItem1 = GameDataManager.instance.miniBossItem1;
            miniBossItem2 = GameDataManager.instance.miniBossItem2;
            hp = GameDataManager.instance.hp;
            damage = GameDataManager.instance.damage;
            speed = GameDataManager.instance.speed;
        }

        void Update()
        {
            GetInput();
            if (cancel)
            {
                Move();
                Jump();
            }
        }

        void GetInput()
        {
            moveX = Input.GetAxis("Horizontal");
            jump = Input.GetButtonDown("Jump");
        }

        void Move()
        {
            float currentSpeed;
            if (SceneManager.GetActiveScene().name == "World1")
            {
                currentSpeed = speed - (speed / 2);
            }
            else
            {
                currentSpeed = speed;
            }
            if (moveX > 0)
            {
                RotVec = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.Euler(RotVec);
            }
            else if (moveX < 0)
            {
                RotVec = new Vector3(0, 180, 0);
                transform.rotation = Quaternion.Euler(RotVec);
            }
            moveVec = new Vector3(moveX, 0, 0).normalized;
            transform.position += moveVec * currentSpeed * Time.deltaTime;
        }

        void Jump()
        {
            if (jump && !isJump)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }

        void Cancel()
        {
            cancel = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerSkills playerSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            if (other.CompareTag("Mob2_skill"))
            {
                Mob2_skill mob2_skill = GameObject.FindGameObjectWithTag("Mob2_skill").GetComponent<Mob2_skill>();

                Destroy(other.gameObject);
                
                if (hp <= 0f)
                {
                    DataReset();
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    hp -= mob2_skill.damage;
                    GameDataManager.instance.hp = hp;
                    GameDataManager.instance.SaveData();
                }
            }
            if (other.CompareTag("Mob1_skill"))
            {
                Mob1_skill mob1_skill = GameObject.FindGameObjectWithTag("Mob1_skill").GetComponent<Mob1_skill>();

                Destroy(other.gameObject);
                
                if (hp <= 0f)
                {
                    DataReset();
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    hp -= mob1_skill.damage;
                    GameDataManager.instance.hp = hp;
                    GameDataManager.instance.SaveData();
                }
            }
            if (other.CompareTag("MiniBoss1_Pattern1"))
            {
                cancel = false;
                Invoke("Cancel", 1.5f);
            }
            if (other.CompareTag("MiniBoss1_Pattern2"))
            {
                MiniBoss1_Pattern2 miniBoss1_Pattern2 = GameObject.FindGameObjectWithTag("MiniBoss1_Pattern2").GetComponent<MiniBoss1_Pattern2>();
                if (hp <= 0f)
                {
                    DataReset();
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    hp -= miniBoss1_Pattern2.damage;
                    GameDataManager.instance.hp = hp;
                    GameDataManager.instance.SaveData();
                }
            }
            if (other.CompareTag("MiniBoss2_Pattern1"))
            {
                MiniBoss2_Pattern1 miniBoss2_Pattern1 = GameObject.FindGameObjectWithTag("MiniBoss2_Pattern1").GetComponent<MiniBoss2_Pattern1>();
                if (hp <= 0f)
                {
                    DataReset();
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    hp -= miniBoss2_Pattern1.damage;
                    GameDataManager.instance.hp = hp;
                    GameDataManager.instance.SaveData();
                }
            }
            if (other.CompareTag("MiniBoss2_Pattern2"))
            {
                MiniBoss2_Pattern2 miniBoss2_Pattern2 = GameObject.FindGameObjectWithTag("MiniBoss2_Pattern2").GetComponent<MiniBoss2_Pattern2>();
                if (hp <= 0f)
                {
                    DataReset();
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    hp -= miniBoss2_Pattern2.damage;
                    GameDataManager.instance.hp = hp;
                    GameDataManager.instance.SaveData();
                }
            }
            //시간 관련 보스 드랍템
            if (other.CompareTag("MiniBossItem1"))
            {
                Destroy(other.gameObject);
                miniBossItem1 = 1;
                playerSkills.WskillCooldown = 0f;
                gameManager.ReloadUI();
                GameDataManager.instance.miniBossItem1 = miniBossItem1;
                GameDataManager.instance.SaveData();
                if (miniBossItem2 == 1)
                {
                    playerSkills.RskillCooldown = 0f;
                }
                speed = 10;
                GameDataManager.instance.speed = speed;
                GameDataManager.instance.SaveData();
            }
            //공간 관련 보스 드랍템
            if (other.CompareTag("MiniBossItem2"))
            {
                Destroy(other.gameObject);
                miniBossItem2 = 1;
                playerSkills.EskillCooldown = 0f;
                gameManager.ReloadUI();
                GameDataManager.instance.miniBossItem2 = miniBossItem2;
                GameDataManager.instance.SaveData();
                if (miniBossItem1 == 1)
                {
                    playerSkills.RskillCooldown = 0f;
                }
                damage = 15;
                GameDataManager.instance.damage = damage;
                GameDataManager.instance.SaveData();
            }
        }

        void DataReset()
        {
            GameDataManager.instance.mobKill = 0;
            GameDataManager.instance.miniBossType = 0;
            GameDataManager.instance.PlayerTransform = Vector3.zero + (-Vector3.up * 2.9f);
            GameDataManager.instance.miniBossItem1 = 0;
            GameDataManager.instance.miniBossItem2 = 0;
            GameDataManager.instance.cooldown1 = 0;
            GameDataManager.instance.cooldown2 = 0;
            GameDataManager.instance.cooldown3 = 0;
            GameDataManager.instance.cooldown4 = 0;
            GameDataManager.instance.damage = 5;
            GameDataManager.instance.speed = 5;
            GameDataManager.instance.hp = 100;
            GameDataManager.instance.isMiniBoss = 0;
            GameDataManager.instance.bossHp = 0;
            GameDataManager.instance.last = 0;
            GameDataManager.instance.isBoss = 0;
            GameDataManager.instance.SaveData();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJump = false;
            }
        }

        private void OnDisable()
        {
            if (hp > 0f)
            {
                if (GameDataManager.instance != null)
                {
                    GameDataManager.instance.PlayerTransform = transform.position;
                    GameDataManager.instance.SaveData();
                }
            }
        }
    }
}
