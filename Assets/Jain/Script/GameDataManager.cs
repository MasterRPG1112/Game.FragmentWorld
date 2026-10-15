using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Jain
{
    public class GameDataManager : MonoBehaviour
    {
        public static GameDataManager instance;

        //UI °ü·Ã
        public int worldText;

        //ÄðÅ¸ÀÓ
        public float cooldown1;
        public float cooldown2;
        public float cooldown3;
        public float cooldown4;

        //ÇÃ·¹ÀÌ¾î °ü·Ã
        public Vector3 PlayerTransform;
        public float hp;
        public float damage;
        public float speed;

        //º¸½º °ü·Ã
        public int mobKill;
        public float bossCooldown;
        public float bossHp;
        public int miniBossType;
        public int miniBossItem1;
        public int miniBossItem2;
        public int isMiniBoss;
        public Vector3 bossTransform;

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(instance);
            LoadDate();
        }

        public void LoadDate()
        {
            //Player ÁÂÇ¥
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY", -2.9f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);

            PlayerTransform = new Vector3(x, y, z);

            //UI
            worldText = PlayerPrefs.GetInt("WorldText");
            cooldown1 = PlayerPrefs.GetFloat("Cooldown1");
            cooldown2 = PlayerPrefs.GetFloat("Cooldown2");
            cooldown3 = PlayerPrefs.GetFloat("Cooldown3");
            cooldown4 = PlayerPrefs.GetFloat("Cooldown4");

            //º¸½º °ü·Ã
            mobKill = PlayerPrefs.GetInt("MobKill");
            bossCooldown = PlayerPrefs.GetFloat("BossCooldown");
            bossHp = PlayerPrefs.GetFloat("BossHp");
            miniBossType = PlayerPrefs.GetInt("MiniBossType");
            isMiniBoss = PlayerPrefs.GetInt("IsMiniBoss");

            //º¸½º ÁÂÇ¥
            float bossX = PlayerPrefs.GetFloat("BossX");
            float bossY = PlayerPrefs.GetFloat("BossY", -2.9f);
            float bossZ = PlayerPrefs.GetFloat("BossZ", 0f);

            bossTransform = new Vector3(bossX, bossY, bossZ);

            //¾ÆÀÌÅÛ
            miniBossItem1 = PlayerPrefs.GetInt("MiniBossItem1");
            miniBossItem2 = PlayerPrefs.GetInt("MiniBossItem2");

            //½ºÅÈ
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

            //Player ÁÂÇ¥
            PlayerPrefs.SetFloat("PlayerX", PlayerTransform.x);
            PlayerPrefs.SetFloat("PlayerY", PlayerTransform.y);
            PlayerPrefs.SetFloat("PlayerZ", PlayerTransform.z);

            //º¸½º °ü·Ã
            PlayerPrefs.SetInt("MobKill", mobKill);
            PlayerPrefs.SetFloat("BossCooldown", bossCooldown);
            PlayerPrefs.SetFloat("BossHp", bossHp);
            PlayerPrefs.SetInt("MiniBossType", miniBossType);
            PlayerPrefs.SetInt("IsMiniBoss", isMiniBoss);

            //º¸½º ÁÂÇ¥
            PlayerPrefs.SetFloat("BossX", bossTransform.x);
            PlayerPrefs.SetFloat("BossY", bossTransform.y);
            PlayerPrefs.SetFloat("BossZ", bossTransform.z);

            //¾ÆÀÌÅÛ
            PlayerPrefs.SetInt("MiniBossItem1", miniBossItem1);
            PlayerPrefs.SetInt("MiniBossItem2", miniBossItem2);

            //½ºÅÈ
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
