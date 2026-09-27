using UnityEngine;

namespace Jain
{
    public class Rskill : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 6.5f);
        }
        void Update()
        {
        
        }

        private void OnDestroy()
        {
            PlayerSkills playerSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();
            playerSkills.ReturnOfTheGuardian = 0;
        }
    }
}
