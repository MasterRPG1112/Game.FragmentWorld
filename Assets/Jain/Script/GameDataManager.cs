using JetBrains.Annotations;
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
        public float cooldown4;
        public int mobKill;
        public int miniBossType;
        public int miniBossItem1;
        public int miniBossItem2;
        public float hp;
        public float damage;
        public float speed;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(instance);
            LoadDate();
        }

        public void LoadDate()
        {
            //Player 좌표
            float x = PlayerPrefs.GetFloat("PlayerX", -7f);
            float y = PlayerPrefs.GetFloat("PlayerY", -3f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);

            PlayerTransform = new Vector3(x, y, z);

            //UI
            worldText = PlayerPrefs.GetInt("WorldText");
            cooldown1 = PlayerPrefs.GetFloat("Cooldown1");
            cooldown2 = PlayerPrefs.GetFloat("Cooldown2");
            cooldown3 = PlayerPrefs.GetFloat("Cooldown3");
            cooldown4 = PlayerPrefs.GetFloat("Cooldown4");

            //준보스 관련
            mobKill = PlayerPrefs.GetInt("MobKill");
            miniBossType = PlayerPrefs.GetInt("MiniBossType");

            //아이템
            miniBossItem1 = PlayerPrefs.GetInt("MiniBossItem1");
            miniBossItem2 = PlayerPrefs.GetInt("MiniBossItem2");

            //스탯
            hp = PlayerPrefs.GetFloat("Hp");
            damage = PlayerPrefs.GetFloat("Damage");
            speed = PlayerPrefs.GetFloat("Speed");
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
            PlayerPrefs.SetFloat("Cooldown4", cooldown4);

            //Player 좌표
            PlayerPrefs.SetFloat("PlayerX", PlayerTransform.x);
            PlayerPrefs.SetFloat("PlayerY", PlayerTransform.y);
            PlayerPrefs.SetFloat("PlayerZ", PlayerTransform.z);

            //준보스 관련
            PlayerPrefs.SetInt("MobKill", mobKill);
            PlayerPrefs.SetInt("MiniBossType", miniBossType);

            //아이템
            PlayerPrefs.SetInt("MiniBossItem1", miniBossItem1);
            PlayerPrefs.SetInt("MiniBossItem2", miniBossItem2);

            //스탯
            PlayerPrefs.SetFloat("Hp", hp);
            PlayerPrefs.SetFloat("Damage", damage);
            PlayerPrefs.SetFloat("Speed", speed);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
