using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Jain
{
    public class WorldTransition : MonoBehaviour
    {
        //월드 전환 변수
        bool Tab;
        int worldBool = 1;

        void Start()
        {
        
        }

        void Update()
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Boss boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
            GetInput();
            if (boss.worldTransitionStop == 0)
            {
                if (player.cancel)
                {
                    WorldChange();
                }
            }
        }

        void GetInput()
        {
            Tab = Input.GetButtonDown("World Transition");
        }

        void WorldChange()
        {
            if (Tab == true)
            {
                if (SceneManager.GetActiveScene().name == "World1")
                {
                    SceneManager.LoadScene("World2");
                    worldBool = 0;
                }
                else
                {
                    SceneManager.LoadScene("World1");
                    worldBool = 1;
                }
            }
        }

        private void OnDisable()
        {
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.worldText = worldBool;
                GameDataManager.instance.SaveData();
            }
        }
    }
}
