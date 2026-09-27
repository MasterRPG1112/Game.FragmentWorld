using UnityEngine;

namespace Jain
{
    public class Eskill : MonoBehaviour
    {
        //AI 사용

        public float moveSpeed = 6.0f;
        public float rotationSpeed = 15.0f;
        public float angleOffset = 90f;

        private Transform targetMob;

        void Start()
        {
            Destroy(gameObject, 3.0f);
            FindClosestMob();
        }

        void Update()
        {
            if (targetMob == null)
            {
                FindClosestMob();
                return;
            }

            RotateTowardsTarget();
            MoveTowardsTarget();
        }

        void FindClosestMob()
        {
            GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
            float closestDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (GameObject mob in mobs)
            {
                float distance = Vector2.Distance(transform.position, mob.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = mob.transform;
                }
            }

            targetMob = closest;
        }

        void RotateTowardsTarget()
        {
            Vector2 dir = (targetMob.position - transform.position);

            if (dir.sqrMagnitude > 0.001f)
            {
                float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;

                float currentAngle = transform.eulerAngles.z;
                float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
            }
        }

        void MoveTowardsTarget()
        {
            Vector3 targetPositionXY = new Vector3(targetMob.position.x, targetMob.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPositionXY, moveSpeed * Time.deltaTime);
        }
    }
}