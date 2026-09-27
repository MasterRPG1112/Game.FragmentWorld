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
            LoadDate();
        }

        public void LoadDate()
        {
            float x = PlayerPrefs.GetFloat("PlayerX", -7f);
            float y = PlayerPrefs.GetFloat("PlayerY", -3f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);

            PlayerTransform = new Vector3(x, y, z);
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
