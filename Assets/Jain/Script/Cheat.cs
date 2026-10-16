using UnityEngine;

namespace Jain
{
    public class Cheat : MonoBehaviour
    {
        bool K;

        void Start()
        {
        
        }

        void Update()
        {
            GetInput();
            CheatK();
        }

        void GetInput()
        {
            K = Input.GetButtonDown("Cheat Button");
        }

        void CheatK()
        {
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            gameManager.mobKill += 25;
        }
    }
}
