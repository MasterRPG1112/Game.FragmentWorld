using UnityEngine;

namespace Jain
{
    public class MiniBoss2 : MonoBehaviour
    {
        //패턴 1~2
        int randomPattern;
        public float bossCooldown = 0f;
        public GameObject miniBoss2Pattern1;
        public GameObject miniBoss2Pattern2;

        //패턴 1
        public float padding = 0.05f;

        void Pattern1()
        {
            Camera mainCam = Camera.main;
            if (mainCam == null || miniBoss2Pattern1 == null) return;

            float randomX = Random.Range(0f + padding, 1f - padding);
            float randomY = Random.Range(0f + padding, 1f - padding);

            Vector3 viewportPoint = new Vector3(randomX, randomY, Mathf.Abs(mainCam.transform.position.z));
            Vector3 worldSpawnPos = mainCam.ViewportToWorldPoint(viewportPoint);

            worldSpawnPos.z = 0f;

            Instantiate(miniBoss2Pattern1, worldSpawnPos, Quaternion.Euler(new Vector3(0f, 0f, Random.Range(1, 361))));
        }

        //패턴 2
        void Pattern2()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Instantiate(miniBoss2Pattern2, player.transform.position, player.transform.rotation);
            Instantiate(miniBoss2Pattern2, player.transform.position + new Vector3(0f, 0f, -1f), player.transform.rotation);
        }

        //아이템
        public GameObject miniBossItem2;

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
                BossHpBar.instance.ShowBossHP("굴복한 위대한 공간의 지배자", commonMob.maxHp);
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
                    for (int i = 0; i < 3; i++)
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
                GameDataManager.instance.isMiniBoss = 0;
                GameDataManager.instance.SaveData();
                Instantiate(miniBossItem2, transform.position, transform.rotation);
            }
            BossHpBar.instance.HideBossHP();
        }
    }
}
