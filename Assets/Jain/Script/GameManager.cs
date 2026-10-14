using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Codice.CM.WorkspaceServer;
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

        //Hp바
        public GameObject hpBar;
        public Transform canvas;

        //UI
        public Text worldText;
        public Text cooldown1;
        public Text cooldown2;
        public Text cooldown3;
        public Text cooldown4;

        //BGM
        public BGMSetting bgmSetting1;
        public BGMSetting bgmSetting2;

        //Random(1, 50) = 24 or mobKill 25
        public int mobKill = 0;
        public int randomSpawn = 0;
        public int miniBossType = 0;

        //준보스 관련
        private GameObject currentMiniBoss = null;
        public GameObject miniBoss1;
        public GameObject miniBoss2;
        public bool isMiniBoss;

        private void Awake()
        {
            GameDataManager.instance.LoadDate();
            mobKill = GameDataManager.instance.mobKill;
            miniBossType = GameDataManager.instance.miniBossType;
            if (bgmSetting2 != null) bgmSetting2.OffBGM();
        }

        IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(spawnWait);
            while (true)
            {
                if (isMiniBoss)
                {
                    yield return new WaitForSeconds(spawnWait);
                    continue;
                }

                for (int i = 0; i < enemyCount; i++)
                {
                    if (isMiniBoss)
                    {
                        break;
                    }

                    for (int j = listEnemys.Count - 1; j >= 0; j--)
                    {
                        if (listEnemys[j] == null)
                        {
                            listEnemys.RemoveAt(j);
                        }
                    }
                    if (listEnemys.Count < enemyCount)
                    {
                        GameObject enemy = Enemys[Random.Range(0, Enemys.Length)];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x) + player.position.x, spawnValue.y, spawnValue.z);
                        Quaternion spawnRotation = Quaternion.identity;

                        //AI 사용
                        GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, spawnRotation);
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
            if (isMiniBoss)
            {
                return;
            }

            isMiniBoss = true;

            mobKill = 0;
            randomSpawn = 0;
            GameDataManager.instance.mobKill = 0;
            GameDataManager.instance.SaveData();

            ClearAllEnemy();

            if (bgmSetting1 != null) bgmSetting1.OffBGM();
            if (bgmSetting2 != null) bgmSetting2.OnBGM();

            if (miniBossType == 0)
            {
                miniBossType = Random.Range(1, 3);
                GameDataManager.instance.miniBossType = miniBossType;
                GameDataManager.instance.SaveData();
            }
            if (miniBossType == 1)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x) + player.position.x, spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                currentMiniBoss = Instantiate(miniBoss1, spawnPosition, spawnRotation);
                miniBossType = 2;
            }
            else if (miniBossType == 2)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x) + player.position.x, spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                currentMiniBoss = Instantiate(miniBoss2, spawnPosition, spawnRotation);
                miniBossType = 1;
            }
        }

        public void ReloadUI()
        {
            PlayerSkills playerSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (GameDataManager.instance.worldText == 1)
            {
                worldText.text = "세계 : 흐르지 않는 시간";
            }
            else
            {
                worldText.text = "세계 : 변하지 않는 공간";
            }
            if (player.miniBossItem1 == 1)
            {
                if (GameDataManager.instance.cooldown1 > 0)
                {
                    cooldown1.text = "시간정지 : " + playerSkills.WskillCooldown.ToString("F2");
                }
                else
                {
                    cooldown1.text = "시간정지 : 0";
                }
            }
            else
            {
                cooldown1.text = "시간정지 : 봉인";
            }
            if (player.miniBossItem2 == 1)
            {
                if (GameDataManager.instance.cooldown2 > 0)
                {
                    cooldown2.text = "공간참 : " + playerSkills.EskillCooldown.ToString("F2");
                }
                else
                {
                    cooldown2.text = "공간참 : 0";
                }
            }
            else
            {
                cooldown2.text = "공간참 : 봉인";
            }
            if (player.miniBossItem1 == 1 && player.miniBossItem2 == 1)
            {
                if (GameDataManager.instance.cooldown3 > 0)
                {
                    cooldown3.text = "각성 : " + playerSkills.RskillCooldown.ToString("F2");
                }
                else
                {
                    cooldown3.text = "각성 : 0";
                }
            }
            else
            {
                cooldown3.text = "각성 : 봉인";
            }
            if (GameDataManager.instance.cooldown4 > 0)
            {
                cooldown4.text = "회복 : " + playerSkills.ZskillCooldown.ToString("F2");
            }
            else
            {
                cooldown4.text = "회복 : 0";
            }
        }

        public void RandomSpawn()
        {
            randomSpawn = Random.Range(1, 50);
        }

        void Start()
        {
            StartCoroutine(SpawnEnemy());
            GameDataManager.instance.LoadDate();
            ReloadUI();
        }

        void Update()
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
    }
}
