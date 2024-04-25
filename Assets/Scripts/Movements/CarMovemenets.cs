using UnityEngine;

namespace Movements
{
    public class Move : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Animator animator;
        [SerializeField] private float carSpeed;
        private Vector3 _vector3;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsTurnUpperLeft = Animator.StringToHash("IsTurnUpperLeft");

        void Start()
        {
            Debug.Log("Hello Worlds");
            carSpeed = 0.02f;
            _vector3 = gameObject.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                _vector3.y += carSpeed;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetFloat(Speed, carSpeed);
                _vector3.x += carSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                _vector3.x -= carSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                _vector3.y -= carSpeed;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool(IsTurnUpperLeft, true);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                carSpeed += 0.01f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                carSpeed = 0.01f;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _vector3,
                1);
        }
    }
}