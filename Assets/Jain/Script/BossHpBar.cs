using UnityEngine;
using UnityEngine.UI;

namespace Jain
{
    public class BossHpBar : MonoBehaviour
    {
        //AI »ç¿ë
        public static BossHpBar instance;

        public GameObject bossBar;
        public Text bossName;
        public Slider bossHpBar;

        private void Awake()
        {
            instance = this;
            if (bossBar != null)
                bossBar.SetActive(false);
        }

        public void ShowBossHP(string bossNameText, float maxHp)
        {
            if (bossBar == null) return;

            bossBar.SetActive(true);
            bossName.text = bossNameText;
            bossHpBar.maxValue = maxHp;
            bossHpBar.value = maxHp;
        }

        public void UpdateHP(float currentHp)
        {
            if (bossHpBar != null)
            {
                bossHpBar.value = Mathf.Max(0f, currentHp);
            }
        }

        public void HideBossHP()
        {
            if (bossBar != null)
                bossBar.SetActive(false);
        }
    }
}
