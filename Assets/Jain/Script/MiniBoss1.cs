using UnityEngine;
using static Codice.CM.Common.CmCallContext;

namespace Jain
{
    public class MiniBoss1 : MonoBehaviour
    {
        //패턴 1~3
        int randomPattern;
        public float bossCooldown;
        public GameObject miniBoss1Pattern1;
        public GameObject miniBoss1Pattern2;
        public GameObject miniBoss1Pattern3;

        //아이템
        public GameObject miniBossItem1;

        void RandomPattern()
        {
            randomPattern = Random.Range(1, 4);
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
