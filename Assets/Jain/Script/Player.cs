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

        //스탯 변수
        public float hp;
        public float damage;

        //점프 변수
        bool jump;
        bool isJump;
        public float jumpPower;

        Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

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
        }

        void Jump()
        {
            if (jump && !isJump)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJump = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Mob2_skill"))
            {
                Mob2_skill mob2_skill = GameObject.FindGameObjectWithTag("Mob2_skill").GetComponent<Mob2_skill>();

                Destroy(other.gameObject);
                hp -= mob2_skill.damage;
                if (hp <= 0f)
                {
                    Destroy(gameObject);
                }
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
