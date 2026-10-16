using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jain
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] Enemys;
        public Transform player;
        public Vector3 spawnValue;
        public int enemyCount;

        public float spawnWait;
        public List<GameObject> listEnemys = new List<GameObject>();

        public GameObject hpBar;
        public Transform canvas;

        public Text worldText;
        public Text cooldown1;
        public Text cooldown2;
        public Text cooldown3;
        public Text cooldown4;

        public BGMSetting bgmSetting1;
        public BGMSetting bgmSetting2;

        public int mobKill = 0;
        public int randomSpawn = 0;
        public int miniBossType = 0;

        private GameObject currentMiniBoss = null;
        public GameObject miniBoss1;
        public GameObject miniBoss2;
        public bool isMiniBoss;

        private GameObject currentBoss = null;
        public GameObject boss;
        public bool last;
        public bool isBoss;

        private void Awake()
        {
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.LoadDate();
                mobKill = GameDataManager.instance.mobKill;
                miniBossType = GameDataManager.instance.miniBossType;
                isMiniBoss = GameDataManager.instance.isMiniBoss == 1 ? true : false;
                last = GameDataManager.instance.last == 1 ? true : false;
                isBoss = GameDataManager.instance.isBoss == 1 ? true : false;
            }

            if (isMiniBoss)
            {
                isMiniBoss = false;
                MiniBossSpawn();
            }
            if (isBoss)
            {
                isBoss = false;
                BossSpawn();
            }
            if (bgmSetting2 != null) bgmSetting2.OffBGM();
        }

        IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(spawnWait);
            while (true)
            {
                // 미니보스나 보스가 활성화되어 있으면 적 스폰을 멈추고 대기 (무한 루프 방지)
                if (isMiniBoss || isBoss)
                {
                    yield return new WaitForSeconds(1.0f);
                    continue;
                }

                for (int i = 0; i < enemyCount; i++)
                {
                    if (isMiniBoss || isBoss) break;

                    for (int j = listEnemys.Count - 1; j >= 0; j--)
                    {
                        if (listEnemys[j] == null)
                        {
                            listEnemys.RemoveAt(j);
                        }
                    }

                    if (listEnemys.Count < enemyCount && player != null && Enemys.Length > 0)
                    {
                        GameObject enemy = Enemys[Random.Range(0, Enemys.Length)];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x) + player.position.x, spawnValue.y, spawnValue.z);
                        GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                        listEnemys.Add(spawnedEnemy);

                        if (hpBar != null && canvas != null)
                        {
                            GameObject hpBarObj = Instantiate(hpBar, canvas);
                            MobHpBar mobHpBar = hpBarObj.GetComponent<MobHpBar>();
                            if (mobHpBar != null)
                            {
                                mobHpBar.Initialize(spawnedEnemy.transform);
                            }
                        }
                    }
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return null;
            }
        }

        public void ClearAllEnemy()
        {
            for (int i = listEnemys.Count - 1; i >= 0; i--)
            {
                if (listEnemys[i] != null)
                {
                    Destroy(listEnemys[i]);
                }
            }
            listEnemys.Clear();
        }

        public void MiniBossSpawn()
        {
            isMiniBoss = true;
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.isMiniBoss = 1;
                mobKill = 0;
                randomSpawn = 0;
                GameDataManager.instance.mobKill = 0;
                GameDataManager.instance.SaveData();
            }

            ClearAllEnemy();

            if (bgmSetting1 != null) bgmSetting1.OffBGM();
            if (bgmSetting2 != null) bgmSetting2.OnBGM();

            if (miniBossType == 0)
            {
                miniBossType = Random.Range(1, 3);
                if (GameDataManager.instance != null)
                {
                    GameDataManager.instance.miniBossType = miniBossType;
                    GameDataManager.instance.SaveData();
                }
            }

            if (player != null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x) + player.position.x, spawnValue.y, spawnValue.z);
                if (miniBossType == 1 && miniBoss1 != null)
                {
                    currentMiniBoss = Instantiate(miniBoss1, spawnPosition, Quaternion.identity);
                    miniBossType = 2;
                }
                else if (miniBoss2 != null)
                {
                    currentMiniBoss = Instantiate(miniBoss2, spawnPosition, Quaternion.identity);
                    miniBossType = 1;
                }
            }
        }

        public void BossSpawn()
        {
            isBoss = true;
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.isBoss = 1;
                mobKill = 0;
                GameDataManager.instance.mobKill = 0;
                GameDataManager.instance.SaveData();
            }

            ClearAllEnemy();

            if (bgmSetting1 != null) bgmSetting1.OffBGM();
            if (bgmSetting2 != null) bgmSetting2.OnBGM();

            if (player != null && boss != null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x) + player.position.x, spawnValue.y, spawnValue.z);
                currentBoss = Instantiate(boss, spawnPosition, Quaternion.identity);
            }
        }

        public void ReloadUI()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj == null) return;

            PlayerSkills playerSkills = playerObj.GetComponent<PlayerSkills>();
            Player playerComp = playerObj.GetComponent<Player>();

            if (worldText != null && GameDataManager.instance != null)
            {
                worldText.text = (GameDataManager.instance.worldText == 1) ? "세계 : 흐르지 않는 시간" : "세계 : 변하지 않는 공간";
            }

            if (playerComp != null && playerSkills != null && GameDataManager.instance != null)
            {
                if (cooldown1 != null)
                    cooldown1.text = (playerComp.miniBossItem1 == 1) ? (GameDataManager.instance.cooldown1 > 0 ? "시간정지 : " + playerSkills.WskillCooldown.ToString("F2") : "시간정지 : 0") : "시간정지 : 봉인";

                if (cooldown2 != null)
                    cooldown2.text = (playerComp.miniBossItem2 == 1) ? (GameDataManager.instance.cooldown2 > 0 ? "공간참 : " + playerSkills.EskillCooldown.ToString("F2") : "공간참 : 0") : "공간참 : 봉인";

                if (cooldown3 != null)
                    cooldown3.text = (playerComp.miniBossItem1 == 1 && playerComp.miniBossItem2 == 1) ? (GameDataManager.instance.cooldown3 > 0 ? "각성 : " + playerSkills.RskillCooldown.ToString("F2") : "각성 : 0") : "각성 : 봉인";

                if (cooldown4 != null)
                    cooldown4.text = GameDataManager.instance.cooldown4 > 0 ? "회복 : " + playerSkills.ZskillCooldown.ToString("F2") : "회복 : 0";
            }
        }

        public void RandomSpawn()
        {
            randomSpawn = Random.Range(1, 50);
        }

        void Start()
        {
            StartCoroutine(SpawnEnemy());
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.LoadDate();
            }
            ReloadUI();
        }

        void Update()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj == null) return;

            Player playerComp = playerObj.GetComponent<Player>();
            if (playerComp != null && playerComp.miniBossItem1 == 1 && playerComp.miniBossItem2 == 1)
            {
                last = true;
                if (GameDataManager.instance != null)
                {
                    GameDataManager.instance.last = 1;
                    GameDataManager.instance.SaveData();
                }
            }

            if (!last)
            {
                if (!isMiniBoss)
                {
                    if (mobKill >= 25 || randomSpawn == 24)
                    {
                        MiniBossSpawn();
                    }
                }
                else
                {
                    if (currentMiniBoss == null)
                    {
                        if (bgmSetting1 != null) bgmSetting1.OnBGM();
                        if (bgmSetting2 != null) bgmSetting2.OffBGM();
                        isMiniBoss = false;
                    }
                }
            }
            else
            {
                if (!isBoss)
                {
                    if (mobKill >= 50)
                    {
                        BossSpawn();
                    }
                }
                else
                {
                    if (currentBoss == null)
                    {
                        if (bgmSetting1 != null) bgmSetting1.OnBGM();
                        if (bgmSetting2 != null) bgmSetting2.OffBGM();
                        isBoss = false;
                    }
                }
            }
        }
    }
}