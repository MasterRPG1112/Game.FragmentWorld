using UnityEngine;

namespace Jain
{
    public class Player : MonoBehaviour
    {
        //이동 변수
        public float speed;
        float moveX;
        Vector3 moveVec;
        Vector3 RotVec;

        private Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();

            GameDataManager.instance.LoadDate();
            transform.position = GameDataManager.instance.PlayerTransform;
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
            if (moveX > 0)
            {
                RotVec = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.Euler(RotVec);
            }
            else if (moveX < 0)
            {
                RotVec = new Vector3(0, 180, 0);
                transform.rotation = Quaternion.Euler(RotVec);
            }
            moveVec = new Vector3(moveX, 0, 0).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
            animator.SetBool("IsMove", moveX != 0);
        }

        private void OnDisable()
        {
            if (GameDataManager.instance != null)
            {
                GameDataManager.instance.PlayerTransform = transform.position;
                GameDataManager.instance.SaveData();
            }
        }
    }
}
