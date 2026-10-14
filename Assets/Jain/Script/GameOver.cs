using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jain
{
    public class GameOver : MonoBehaviour
    {
        void Start()
        {
            Invoke("World", 30.0f);
        }

        void World()
        {
            SceneManager.LoadScene("Menu");
        }

        void Update()
        {
        
        }
    }
}
