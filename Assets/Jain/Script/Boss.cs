using UnityEngine;

namespace Jain
{
    public class Boss : MonoBehaviour
    {
        int randomPattern;
        public float bossCooldown = 0f;
        public GameObject bossPattern1;
        public GameObject bossPattern2;
        public int worldTransitionStop = 0;

        public float padding = 0.05f;
        private CommonMob commonMob;

        void Start()
        {
            worldTransitionStop = 1;

            // 보스 자신 또는 태그 검색을 통해 CommonMob 안전 확보
            commonMob = GetComponent<CommonMob>();
            if (commonMob == null)
            {
                GameObject miniBossObj = GameObject.FindGameObjectWithTag("MiniBoss2");
                if (miniBossObj != null)
                {
                    commonMob = miniBossObj.GetComponent<CommonMob>();
                }
            }

            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.LoadDate();
                if (commonMob != null)
                {
                    commonMob.Hp = GameDataManager.instance.bossHp;
                }
                bossCooldown = GameDataManager.instance.bossCooldown;
                transform.position = GameDataManager.instance.bossTransform;
            }

            if (commonMob != null)
            {
                if (commonMob.Hp <= 0)
                {
                    commonMob.Hp = commonMob.maxHp;
                }

                if (BossHpBar.instance != null)
                {
                    BossHpBar.instance.ShowBossHP("파괴자", commonMob.maxHp);
                }
            }
        }

        void Update()
        {
            if (commonMob == null) return;

            BossBarUpdate();

            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.bossHp = commonMob.Hp;
                GameDataManager.instance.bossTransform = transform.position;
                // 매 프레임 SaveData()를 호출하면 게임이 멈추거나 렉이 걸립니다. 꼭 필요할 때만 호출하는 것이 좋습니다.
            }

            if (bossCooldown <= 0f)
            {
                RandomPattern();
                if (randomPattern == 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Pattern1();
                    }
                }
                else if (randomPattern == 2)
                {
                    Pattern2();
                }
            }
            Cooldown();
        }

        void Pattern1()
        {
            Camera mainCam = Camera.main;
            if (mainCam == null || bossPattern1 == null) return;

            float randomX = Random.Range(0f + padding, 1f - padding);
            Vector3 viewportPoint = new Vector3(randomX, 9f, Mathf.Abs(mainCam.transform.position.z));
            Vector3 worldSpawnPos = mainCam.ViewportToWorldPoint(viewportPoint);
            worldSpawnPos.z = 0f;

            Instantiate(bossPattern1, worldSpawnPos, Quaternion.identity);
        }

        void Pattern2()
        {
            // 패턴 2 구현
        }

        void RandomPattern()
        {
            randomPattern = Random.Range(1, 3);
            bossCooldown = 2f;
        }

        void Cooldown()
        {
            if (bossCooldown > 0f)
            {
                bossCooldown -= Time.deltaTime;
            }
        }

        void BossBarUpdate()
        {
            if (commonMob != null && BossHpBar.instance != null)
            {
                BossHpBar.instance.UpdateHP(commonMob.Hp);
            }
        }

        private void OnDestroy()
        {
            if (commonMob == null)
            {
                commonMob = GetComponent<CommonMob>();
            }

            if (commonMob != null && commonMob.Hp <= 0)
            {
                worldTransitionStop = 0;
                if (GameDataManager.instance != null)
                {
                    GameDataManager.instance.isBoss = 0;
                    GameDataManager.instance.SaveData();
                }
            }

            if (BossHpBar.instance != null)
            {
                BossHpBar.instance.HideBossHP();
            }
        }
    }
}