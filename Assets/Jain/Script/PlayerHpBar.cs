using UnityEngine;
using UnityEngine.UI;

namespace Jain
{
    public class PlayerHpBar : MonoBehaviour
    {
        //AI »ç¿ë
        public Vector3 worldOffset = new Vector3(0, 1.1f, 0);

        private RectTransform rectTransform;
        private Player playerComponent;

        public Slider hpBar;
        public float maxHp;
        public float currentHp;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerComponent = playerObj.GetComponentInParent<Player>();

                if (playerComponent != null)
                {
                    maxHp = playerComponent.hp;
                    currentHp = maxHp;
                }
            }

            if (hpBar != null)
            {
                hpBar.minValue = 0f;
                hpBar.maxValue = maxHp;
                hpBar.value = currentHp;
            }
        }

        void LateUpdate()
        {
            if (playerComponent == null) return;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerComponent.transform.position + worldOffset);

            if (screenPos.z < 0)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }

            if (hpBar != null)
            {
                currentHp = playerComponent.hp;
                hpBar.value = currentHp;
            }

            rectTransform.position = screenPos;
        }
    }
}
