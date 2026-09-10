using UnityEngine;

namespace Jain
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager instance;
        public Vector3 PlayerTransform;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        public void LoadDate()
        {

        }

        public void SaveData()
        {
            if (PlayerPrefs.HasKey("id"))
            {
                string id = PlayerPrefs.GetString("id");
                Debug.Log(id);
            }
            else
            {
                PlayerPrefs.SetString("id", "Jain");
            }

            PlayerPrefs.SetFloat("PlayerX", PlayerTransform.x);
            PlayerPrefs.SetFloat("PlayerY", PlayerTransform.y);
            PlayerPrefs.SetFloat("PlayerZ", PlayerTransform.z);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
