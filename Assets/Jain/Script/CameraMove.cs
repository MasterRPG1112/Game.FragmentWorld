using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Jain
{
    public class CameraMove : MonoBehaviour
    {
        //AI »ç¿ë

        public Transform player;
        public float smoothTime = 0.25f;
        public Vector3 offset = new Vector3(0f, 0f, -10f);

        private Vector3 currentVelocity = Vector3.zero;

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        void LateUpdate()
        {
            if (player == null) return;

            Vector3 playerPosition = player.position + offset + new Vector3(0f, 2.9f, 0f);
            transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref currentVelocity, smoothTime);
        }
    }
}
