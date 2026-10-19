using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jain
{
    public class Boss : MonoBehaviour
    {
        private int randomPattern;
        public float bossCooldown = 0f;
        public GameObject bossPattern1;
        public GameObject bossPattern2;
        public int worldTransitionStop = 0;

        public float padding = 0.05f;
        private CommonMob commonMob;
        private bool isDead = false;

        void Start()
        {
            worldTransitionStop = 1;

            // 자기 자신의 CommonMob 가져오기
            commonMob = GetComponent<CommonMob>();

            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.LoadDate();

                if (commonMob != null)
                {
                    // 저장된 HP가 0 이하이면 maxHp로 설정
                    if (GameDataManager.instance.bossHp <= 0)
                    {
                        commonMob.Hp = commonMob.maxHp;
                    }
                    else
                    {
                        commonMob.Hp = GameDataManager.instance.bossHp;
                    }
                }

                bossCooldown = GameDataManager.instance.bossCooldown;
                transform.position = GameDataManager.instance.bossTransform;
            }

            if (commonMob != null && BossHpBar.instance != null)
            {
                BossHpBar.instance.ShowBossHP("파괴자", commonMob.maxHp);
            }
        }

        void Update()
        {
            // Null 체크 및 사망 상태 확인 (에러 방지 핵심)
            if (commonMob == null || isDead) return;

            // HP 바 실시간 갱신
            if (BossHpBar.instance != null)
            {
                BossHpBar.instance.UpdateHP(commonMob.Hp);
            }

            // [사망 체크] HP가 0 이하가 되면 Boss 스크립트가 직접 사망 처리
            if (commonMob.Hp <= 0f)
            {
                Die();
                return;
            }

            // 보스 패턴 실행
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

        // 보스 사망 처리 전용 함수
        void Die()
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            isDead = true;
            worldTransitionStop = 0;

            // 데이터 저장 및 보스 상태 해제
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.mobKill = 0;
                GameDataManager.instance.isBoss = 0;
                GameDataManager.instance.bossHp = 0;
                GameDataManager.instance.SaveData();
            }

            // HP 바 숨기기
            if (BossHpBar.instance != null)
            {
                BossHpBar.instance.HideBossHP();
            }

            // 보스 파괴 (여기서 단 1번만 호출)
            Destroy(gameObject);

            player.DataReset();
            SceneManager.LoadScene("Clear");
        }

        void Pattern1()
        {
            Camera mainCam = Camera.main;
            if (mainCam == null || bossPattern1 == null) return;

            float randomX = Random.Range(0f + padding, 1f - padding);
            Vector3 viewportPoint = new Vector3(randomX, 0.9f, Mathf.Abs(mainCam.transform.position.z));
            Vector3 worldSpawnPos = mainCam.ViewportToWorldPoint(viewportPoint);
            worldSpawnPos.z = 0f;

            Instantiate(bossPattern1, worldSpawnPos, Quaternion.identity);
        }

        void Pattern2()
        {
            // 패턴 2 로직
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

        private void OnDestroy()
        {
            // Destroy(gameObject)를 절대 지우고 UI 정리용으로만 사용
            if (BossHpBar.instance != null)
            {
                BossHpBar.instance.HideBossHP();
            }
        }
    }
}