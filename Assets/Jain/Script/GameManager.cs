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

        IEnumerator SpawnEnemy()
        {
            yield return new WaitForSeconds(spawnWait);
            while (true)
            {
                for (int i = 0; i < enemyCount; i++)
                {
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
        }

        void Start()
        {
            StartCoroutine(SpawnEnemy());
            GameDataManager.instance.LoadDate();
            ReloadUI();
        }

        void Update()
        {

        }
    }
}
