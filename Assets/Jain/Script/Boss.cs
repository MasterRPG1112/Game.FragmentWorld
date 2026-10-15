using UnityEngine;

namespace Jain
{
    public class Boss : MonoBehaviour
    {
        //패턴 1~2
        int randomPattern;
        public float bossCooldown = 0f;
        public GameObject bossPattern1;
        public GameObject bossPattern2;
        public int worldTransitionStop = 0;

        //패턴 1
        public float padding = 0.05f;

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

        //패턴 2
        void Pattern2()
        {
            
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
            CommonMob commonMob = GameObject.FindGameObjectWithTag("MiniBoss2").GetComponent<CommonMob>();
            if (commonMob != null && BossHpBar.instance != null)
            {
                BossHpBar.instance.UpdateHP(commonMob.Hp);
            }
        }

        void Start()
        {
            worldTransitionStop = 1;
            CommonMob commonMob = GameObject.FindGameObjectWithTag("MiniBoss2").GetComponent<CommonMob>();
            GameDataManager.instance.LoadDate();
            commonMob.Hp = GameDataManager.instance.bossHp;
            bossCooldown = GameDataManager.instance.bossCooldown;
            transform.position = GameDataManager.instance.bossTransform;
            if (commonMob.Hp <= 0)
            {
                commonMob.Hp = commonMob.maxHp;
            }
            if (commonMob != null && BossHpBar.instance != null)
            {
                BossHpBar.instance.ShowBossHP("파괴자", commonMob.maxHp);
            }
        }

        void Update()
        {
            CommonMob commonMob = GameObject.FindGameObjectWithTag("MiniBoss2").GetComponent<CommonMob>();
            BossBarUpdate();
            GameDataManager.instance.bossHp = commonMob.Hp;
            GameDataManager.instance.bossTransform = transform.position;
            GameDataManager.instance.SaveData();
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
                if (randomPattern == 2)
                {
                    Pattern2();
                }
            }
            Cooldown();
        }


        private void OnDestroy()
        {
            CommonMob commonMob = GetComponent<CommonMob>();

            if (commonMob.Hp <= 0)
            {
                worldTransitionStop = 0;
                GameDataManager.instance.isBoss = 0;
                GameDataManager.instance.SaveData();
            }
            BossHpBar.instance.HideBossHP();
        }
    }
}
