using UnityEngine;
using UnityEngine.UI;

namespace Jain
{
    public class MobHpBar : MonoBehaviour
    {
        //AI »ç¿ë
        public Transform targetMob;
        public Vector3 worldOffset = new Vector3(0, 1.0f, 0);

        private RectTransform rectTransform;
        public Slider hpBar;

        private CommonMob mobComponent;

        public void Initialize(Transform mob)
        {
            targetMob = mob;
            rectTransform = GetComponent<RectTransform>();
            mobComponent = mob.GetComponent<CommonMob>();

            if (mobComponent != null && hpBar != null)
            {
                hpBar.minValue = 0f;
                hpBar.maxValue = mobComponent.maxHp;
                hpBar.value = mobComponent.Hp;
            }
        }

        void LateUpdate()
        {
            if (targetMob == null || mobComponent == null)
            {
                Destroy(gameObject);
                return;
            }

            if (hpBar != null)
            {
                hpBar.value = mobComponent.Hp;
            }

            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetMob.position + worldOffset);

            if (screenPos.z < 0)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }

            rectTransform.position = screenPos;
        }
    }
}
