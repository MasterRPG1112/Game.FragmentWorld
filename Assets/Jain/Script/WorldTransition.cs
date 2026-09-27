using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Jain
{
    public class WorldTransition : MonoBehaviour
    {
        //월드 전환 변수
        bool Tab;

        void Start()
        {
        
        }

        void Update()
        {
            GetInput();
            WorldChange();
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
                }
                else
                {
                    SceneManager.LoadScene("World1");
                }
            }
        }
    }
}
