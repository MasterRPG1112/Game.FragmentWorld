using UnityEngine;
using static Codice.CM.Common.CmCallContext;

namespace Jain
{
    public class MiniBoss1 : MonoBehaviour
    {
        //패턴 1~3
        int randomPattern;
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
            if (commonMob != null && BossHpBar.instance != null)
            {
                BossHpBar.instance.ShowBossHP("조종당하는 지고한 시간의 조율자", commonMob.maxHp);
            }
        }

        void Update()
        {
            BossBarUpdate();
        }

        private void OnDestroy()
        {
            Instantiate(miniBossItem1, transform.position, transform.rotation);
            BossHpBar.instance.HideBossHP();
        }
    }
}
