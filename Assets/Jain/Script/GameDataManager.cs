using UnityEngine;

namespace Jain
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager instance;
        public Vector3 PlayerTransform;
        public int worldText;
        public float cooldown1;
        public float cooldown2;
        public float cooldown3;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(instance);
            LoadDate();
        }

        public void LoadDate()
        {
            //Player ÁÂÇ¥
            float x = PlayerPrefs.GetFloat("PlayerX", -7f);
            float y = PlayerPrefs.GetFloat("PlayerY", -3f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);

            PlayerTransform = new Vector3(x, y, z);

            //UI
            worldText = PlayerPrefs.GetInt("WorldText");
            cooldown1 = PlayerPrefs.GetFloat("Cooldown1");
            cooldown2 = PlayerPrefs.GetFloat("Cooldown2");
            cooldown3 = PlayerPrefs.GetFloat("Cooldown3");
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

            //UI
            PlayerPrefs.SetInt("WorldText", worldText);
            PlayerPrefs.SetFloat("Cooldown1", cooldown1);
            PlayerPrefs.SetFloat("Cooldown2", cooldown2);
            PlayerPrefs.SetFloat("Cooldown3", cooldown3);

            //Player ÁÂÇ¥
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
