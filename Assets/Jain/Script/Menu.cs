using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jain
{
    public class Menu : MonoBehaviour
    {
        public GameObject MenuBG;

        //MenuBG 버튼 기능
        public void BtnStart()
        {
            SceneManager.LoadScene("World1");
        }
        public void BtnSetting()
        {
            
        }
        public void BtnExit()
        {
            Application.Quit();
        }

        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}
