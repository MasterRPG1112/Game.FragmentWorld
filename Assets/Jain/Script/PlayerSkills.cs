using Codice.Client.BaseCommands.WkStatus.Printers;
using PlasticGui.WorkspaceWindow.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jain
{
    public class PlayerSkills : MonoBehaviour
    {
        //Q 스킬
        bool Q;
        public float QskillCooldown;
        public GameObject Qskill;

        //W 스킬 - 시간정지
        bool W;
        bool A;
        public int Form = 0;
        public float WskillCooldown;
        public GameObject Wskill1;
        public GameObject Wskill2;

        //E 스킬 - 공간참
        bool E;
        public float EskillCooldown;
        public GameObject Eskill;
        public float padding = 0.05f;

        //R 스킬 - 각성
        bool R;
        public float RskillCooldown;
        public GameObject Rskill1;
        public GameObject Rskill2;
        public int ReturnOfTheGuardian = 0;

        //Z 스킬 - 회복
        bool Z;
        public float ZskillCooldown;
        public GameObject Zskill;

        public GameObject Point;
        public GameObject MainCamera;

        private void Awake()
        {
            WskillCooldown = GameDataManager.instance.cooldown1;
            EskillCooldown = GameDataManager.instance.cooldown2;
            RskillCooldown = GameDataManager.instance.cooldown3;
            ZskillCooldown = GameDataManager.instance.cooldown4;
        }

        void Start()
        {
            
        }

        void Update()
        {
            GetInput();
            KeyButton();
            CooldownMin();
        }

        void GetInput()
        {
            Q = Input.GetButtonDown("Qskill Button");
            W = Input.GetButtonDown("Wskill Button");
            A = Input.GetButtonDown("Askill Button");
            E = Input.GetButtonDown("Eskill Button");
            R = Input.GetButtonDown("Rskill Button");
            Z = Input.GetButtonDown("Zskill Button");
        }

        void KeyButton()
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (QskillCooldown <= 0f)
            {
                if (Q == true)
                {
                    Skill_Q();
                    QskillCooldown = 0.5f;
                }
            }
            if (player.miniBossItem1 == 1)
            {
                if (SceneManager.GetActiveScene().name == "World1" || ReturnOfTheGuardian == 1)
                {
                    if (A == true)
                    {
                        if (Form == 0)
                        {
                            Form = 1;
                            Debug.Log(Form);
                        }
                        else if (Form == 1)
                        {
                            Form = 0;
                            Debug.Log(Form);
                        }
                    }
                    if (WskillCooldown <= 0f)
                    {
                        if (W == true)
                        {
                            if (Form == 0)
                            {
                                Skill_W1();
                                WskillCooldown = 4.5f;
                                GameDataManager.instance.cooldown1 = WskillCooldown;
                            }
                            else if (Form == 1)
                            {
                                Skill_W2();
                                WskillCooldown = 10.0f;
                                GameDataManager.instance.cooldown1 = WskillCooldown;
                            }
                            GameDataManager.instance.SaveData();
                        }
                    }
                }
            }
            if (player.miniBossItem2 == 1)
            {
                if (SceneManager.GetActiveScene().name == "World2" || ReturnOfTheGuardian == 1)
                {
                    if (EskillCooldown <= 0f)
                    {
                        if (E == true)
                        {
                            for (int i = 0; i < 3; i++) Skill_E();
                            if (ReturnOfTheGuardian == 1)
                            {
                                EskillCooldown = 1.2f;
                                GameDataManager.instance.cooldown2 = EskillCooldown;
                            }
                            else
                            {
                                EskillCooldown = 7.5f;
                                GameDataManager.instance.cooldown2 = EskillCooldown;
                            }
                            GameDataManager.instance.SaveData();
                        }
                    }
                }
            }
            if (player.miniBossItem1 == 1 && player.miniBossItem2 == 1)
            {
                if (RskillCooldown <= 0f)
                {
                    if (R == true)
                    {
                        Skill_R();
                        RskillCooldown = 50.0f;
                        GameDataManager.instance.cooldown3 = RskillCooldown;
                        GameDataManager.instance.SaveData();
                    }
                }
            }
            if (ZskillCooldown <= 0f)
            {
                if (Z == true)
                {
                    Skill_Z();
                    ZskillCooldown = 10.0f;
                    GameDataManager.instance.cooldown4 = ZskillCooldown;
                    GameDataManager.instance.SaveData();
                }
            }
        }

        void CooldownMin()
        {
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            if (QskillCooldown > 0f)
            {
                QskillCooldown -= Time.deltaTime;
                gameManager.ReloadUI();
            }
            if (WskillCooldown > 0f)
            {
                WskillCooldown -= Time.deltaTime;
                gameManager.ReloadUI();
            }
            if (EskillCooldown > 0f)
            {
                EskillCooldown -= Time.deltaTime;
                gameManager.ReloadUI();
            }
            if (RskillCooldown > 0f)
            {
                RskillCooldown -= Time.deltaTime;
                gameManager.ReloadUI();
            }
            if (ZskillCooldown > 0f)
            {
                ZskillCooldown -= Time.deltaTime;
                gameManager.ReloadUI();
            }
        }

        void Skill_Q()
        {
            Instantiate(Qskill, Point.transform.position, Point.transform.rotation);
        }

        void Skill_W1()
        {
            Instantiate(Wskill1, Point.transform.position, Point.transform.rotation);
        }

        void Skill_W2()
        {
            //AI 사용
            Vector3 Transform = MainCamera.transform.position + new Vector3(0f, 5f, 11f);
            GameObject skill1 = Instantiate(Wskill2, Transform, MainCamera.transform.rotation);
            skill1.transform.SetParent(MainCamera.transform);

            Transform = MainCamera.transform.position + new Vector3(0f, -3.5f, 11f);
            GameObject skill2 = Instantiate(Wskill2, Transform, MainCamera.transform.rotation);
            skill2.transform.SetParent(MainCamera.transform);
        }

        void Skill_E()
        {
            //AI 사용
            Camera mainCam = Camera.main;
            if (mainCam == null || Eskill == null) return;

            float randomX = Random.Range(0f + padding, 1f - padding);
            float randomY = Random.Range(0f + padding, 1f - padding);

            Vector3 viewportPoint = new Vector3(randomX, randomY, Mathf.Abs(mainCam.transform.position.z));
            Vector3 worldSpawnPos = mainCam.ViewportToWorldPoint(viewportPoint);

            worldSpawnPos.z = 0f;

            Instantiate(Eskill, worldSpawnPos, Quaternion.identity);
        }

        void Skill_R()
        {
            Vector3 Transform = MainCamera.transform.position + new Vector3(1f, 0f, 11f);
            GameObject skill1 = Instantiate(Rskill1, Transform, MainCamera.transform.rotation);
            skill1.transform.SetParent(MainCamera.transform);

            Transform = MainCamera.transform.position + new Vector3(-2.5f, 0f, 12f);
            GameObject skill2 = Instantiate(Rskill2, Transform, MainCamera.transform.rotation);
            skill2.transform.SetParent(MainCamera.transform);

            ReturnOfTheGuardian = 1;
        }

        void Skill_Z()
        {
            Instantiate(Zskill, transform.position, transform.rotation);
        }
    }
}