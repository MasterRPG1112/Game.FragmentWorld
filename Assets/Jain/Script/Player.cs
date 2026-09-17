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

        //점프 변수
        bool jump;
        bool isJump;
        public float jumpPower;
        Vector3 JumpVec;

        Rigidbody2D rb;
        private Animator animator;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();

            GameDataManager.instance.LoadDate();
            transform.position = GameDataManager.instance.PlayerTransform;
        }

        void Update()
        {
            GetInput();
            Move();
            Jump();
        }

        void GetInput()
        {
            moveX = Input.GetAxis("Horizontal");
            jump = Input.GetButtonDown("Jump");
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

        void Jump()
        {
            if (jump && !isJump)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJump = false;
            }
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
