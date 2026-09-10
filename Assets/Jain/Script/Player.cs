using UnityEngine;

namespace Jain
{
    public class Player : MonoBehaviour
    {
        //이동 변수
        public float speed;
        float moveX;
        Vector3 moveVec;

        void Start()
        {
            
        }

        void Update()
        {
            GetInput();
            Move();
        }

        void GetInput()
        {
            moveX = Input.GetAxis("Horizontal");
        }

        void Move()
        {
            moveVec = new Vector3(moveX, 0, 0).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
        }
    }
}
