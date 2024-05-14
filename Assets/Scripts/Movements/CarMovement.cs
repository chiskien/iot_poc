using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movements
{
    public class CarMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Animator animator;
        [SerializeField] private float carSpeed;

        private new Rigidbody2D rigidbody;
        private string isOpen;
        private Vector2 movement;
        private float initialSpeed;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IsOpenDoor = Animator.StringToHash("IsOpenDoor");
        private static readonly int IsRight = Animator.StringToHash("IsRight");
        private static readonly int IsLeft = Animator.StringToHash("IsLeft");
        private static readonly int IsUp = Animator.StringToHash("IsUp");
        private static readonly int IsDown = Animator.StringToHash("IsDown");

        public float CarSpeed { get => carSpeed; set => carSpeed = value; }
        public Vector2 Movement { get => movement; set => movement = value; }

        private void Awake()
        {
            CarSpeed = 2f;
            initialSpeed = 2f;
            isOpen = "false";
        }
        void Start()
        {
            rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + CarSpeed * Time.fixedDeltaTime * Movement);
        }
        void Update()
        {

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat(Horizontal, Movement.x);
            animator.SetFloat(Vertical, Movement.y);
            animator.SetFloat(Speed, Movement.sqrMagnitude);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool(IsLeft, false);
                animator.SetBool(IsRight, true);
                animator.SetBool(IsUp, false);
                animator.SetBool(IsDown, false);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetBool(IsLeft, true);
                animator.SetBool(IsRight, false);
                animator.SetBool(IsUp, false);
                animator.SetBool(IsDown, false);
            }
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool(IsLeft, false);
                animator.SetBool(IsRight, false);
                animator.SetBool(IsUp, true);
                animator.SetBool(IsDown, false);
            }
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool(IsLeft, false);
                animator.SetBool(IsRight, false);
                animator.SetBool(IsUp, false);
                animator.SetBool(IsDown, true);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CarSpeed += 0.5f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                CarSpeed = initialSpeed;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (isOpen.Equals("false"))
                {
                    animator.SetBool(IsOpenDoor, true);
                    isOpen = "true";
                }
                else
                {
                    animator.SetBool(IsOpenDoor, false);
                    isOpen = "false";
                }
            }


        }
    }
}