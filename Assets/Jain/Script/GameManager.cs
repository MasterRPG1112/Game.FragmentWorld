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
        public Vector3 spawnValue;
        public int enemyCount;

        public float spawnWait;

        public List<GameObject> listEnemys = new List<GameObject>();

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
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        listEnemys.Add(Instantiate(enemy, spawnPosition, enemy.transform.rotation));
                    }
                    yield return new WaitForSeconds(spawnWait);
                }
            }
        }
        public void ReloadUI()
        {
            if (GameDataManager.instance.worldText == 1)
            {
                worldText.text = "세계 : 흐르지 않는 시간";
            }
            else
            {
                worldText.text = "세계 : 변하지 않는 공간";
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
