using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Jain
{
    public class Zskill : MonoBehaviour
    {
        public float hill;

        void Start()
        {
            Destroy(gameObject, 0.3f);
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.hp += hill;
        }

        void Update()
        {
        
        }
    }
}
