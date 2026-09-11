using UnityEngine;

namespace Jain
{
    public class PlayerSkillQ : MonoBehaviour
    {
        public float speed = 10f;
        Vector3 QVec;

        void Start()
        {
            QVec = new Vector3(10, 0, 0);   
        }

        void Update()
        {
            transform.position += QVec * speed * Time.deltaTime;
        }
    }
}