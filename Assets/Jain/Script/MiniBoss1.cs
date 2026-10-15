using UnityEngine;
using static Codice.CM.Common.CmCallContext;

namespace Jain
{
    public class MiniBoss1 : MonoBehaviour
    {
        //패턴 1~2
        int randomPattern;
        public float bossCooldown = 0f;
        public GameObject miniBoss1Pattern1;
        public GameObject miniBoss1Pattern2;

        //패턴 1
        void Pattern1()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Instantiate(miniBoss1Pattern1, player.transform.position, player.transform.rotation);
            Instantiate(miniBoss1Pattern1, player.transform.position + new Vector3(0f, 0f, -1f), player.transform.rotation);
        }

        //패턴 2
        public GameObject point1;
        public GameObject point2;
        public GameObject point3;

        void Pattern2()
        {
            int randomSpawn = Random.Range(1, 4);
            if (randomSpawn == 1)
            {
                GameObject skill = Instantiate(miniBoss1Pattern2, point1.transform.position, point1.transform.rotation);
                skill.transform.SetParent(point1.transform);
            }
            else if (randomSpawn == 2)
            {
                GameObject skill = Instantiate(miniBoss1Pattern2, point2.transform.position, point2.transform.rotation);
                skill.transform.SetParent(point2.transform);
            }
            else
            {
                GameObject skill = Instantiate(miniBoss1Pattern2, point3.transform.position, point3.transform.rotation);
                skill.transform.SetParent(point3.transform);
            }
        }

        //아이템
        public GameObject miniBossItem1;

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
            CommonMob commonMob = GameObject.FindGameObjectWithTag("MiniBoss1").GetComponent<CommonMob>();
            if (commonMob != null && BossHpBar.instance != null)
            {
                BossHpBar.instance.UpdateHP(commonMob.Hp);
            }
        }

        void Start()
        {
            CommonMob commonMob = GameObject.FindGameObjectWithTag("MiniBoss1").GetComponent<CommonMob>();
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
                BossHpBar.instance.ShowBossHP("조종당하는 지고한 시간의 조율자", commonMob.maxHp);
            }
        }

        void Update()
        {
            CommonMob commonMob = GameObject.FindGameObjectWithTag("MiniBoss1").GetComponent<CommonMob>();
            BossBarUpdate();
            GameDataManager.instance.bossHp = commonMob.Hp;
            GameDataManager.instance.bossTransform = transform.position;
            GameDataManager.instance.SaveData();
            if (bossCooldown <= 0f)
            {
                RandomPattern();
                if (randomPattern == 1)
                {
                    Pattern1();
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
                Instantiate(miniBossItem1, transform.position, transform.rotation);
            }
            BossHpBar.instance.HideBossHP();
        }
    }
}
