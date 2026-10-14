using UnityEngine;

namespace Jain
{
    public class BGMSetting : MonoBehaviour
    {
        void Start()
        {
        
        }

        void Update()
        {
        
        }

        public void OnBGM()
        {
            gameObject.SetActive(true);
        }
        public void OffBGM()
        { 
            gameObject.SetActive(false);
        }
    }
}
