using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Movements;

using UnityEngine;

//This script is used to actively use the values received by the MQTT client
namespace MQTT
{

    public class MqttCarController : MonoBehaviour
    {
        public string NameController = "Controller 1";
        public string TagOfTheMQTTReceiver = "MQTT_Receiver";

        private MqttCarReceiver _eventSender;

        [SerializeField]
        private GameObject car;
        private string _isOpen;

        [SerializeField]
        private float _carSpeed;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _movement;
        private static readonly int IsOpenDoor = Animator.StringToHash("IsOpenDoor");
        private static readonly int IsRight = Animator.StringToHash("IsRight");
        private static readonly int IsLeft = Animator.StringToHash("IsLeft");
        private static readonly int IsUp = Animator.StringToHash("IsUp");
        private static readonly int IsDown = Animator.StringToHash("IsDown");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        // Start is called before the first frame update
        private void Awake()
        {

            _isOpen = "false";
            _carSpeed = 2f;
            _eventSender = car.GetComponent<MqttCarReceiver>();
            _rigidbody2D = car.GetComponent<Rigidbody2D>();
            _animator = car.GetComponent<Animator>();
        }
        void Start()
        {
            // car = GameObject.FindGameObjectWithTag(TagOfTheMQTTReceiver);
            // _eventSender = GameObject.FindGameObjectsWithTag(TagOfTheMQTTReceiver)[0].gameObject
            // .GetComponent<MQTTReceiver>();

            _eventSender.OnMessageArrived += OnMessageArrivedHandler;
        }
        private void OnMessageArrivedHandler(string newMsg)
        {

            if (newMsg.Equals(Command.OPEN_DOOR))
            {
                if (_isOpen.Equals("false"))
                {
                    _animator.SetBool(IsOpenDoor, true);
                    _isOpen = "true";
                }
                else
                {
                    _animator.SetBool(IsOpenDoor, false);
                    _isOpen = "false";
                }
            }
            if (newMsg.Equals(Command.CLOSE_DOOR))
            {
                _animator.SetBool(IsOpenDoor, false);
                _isOpen = "false";
            }
            if (newMsg.Equals(Command.GO_RIGHT))
            {
                _carSpeed = 2f;
                _movement.x = 1;
                _movement.y = 0;
                _animator.SetBool(IsLeft, false);
                _animator.SetBool(IsRight, true);
                _animator.SetBool(IsUp, false);
                _animator.SetBool(IsDown, false);
            }
            if (newMsg.Equals(Command.GO_LEFT))
            {
                _carSpeed = 2f;
                _movement.x = -1;
                _movement.y = 0;
                _animator.SetBool(IsLeft, true);
                _animator.SetBool(IsRight, false);
                _animator.SetBool(IsUp, false);
                _animator.SetBool(IsDown, false);
            }
            if (newMsg.Equals(Command.GO_UP))
            {
                _carSpeed = 2f;
                _movement.y = 1;
                _movement.x = 0;
                _animator.SetBool(IsLeft, false);
                _animator.SetBool(IsRight, false);
                _animator.SetBool(IsUp, true);
                _animator.SetBool(IsDown, false);
            }
            if (newMsg.Equals(Command.GO_DOWN))
            {
                _carSpeed = 2f;
                _movement.y = -1;
                _movement.x = 0;
                _animator.SetBool(IsLeft, false);
                _animator.SetBool(IsRight, false);
                _animator.SetBool(IsUp, false);
                _animator.SetBool(IsDown, true);
            }
            if (newMsg.Equals(Command.STOP))
            {
                _carSpeed = 0f;
            }
            if (newMsg.Equals(Command.BOOST_SPEED))
            {
                float i = 0;
                while (i < 5)
                {
                    _carSpeed += i;
                    i++;
                }
            }
        }
        private void Update()
        {
            _animator.SetFloat(Horizontal, _movement.x);
            _animator.SetFloat(Vertical, _movement.y);
            _animator.SetFloat(Speed, _movement.sqrMagnitude);
        }
        private void FixedUpdate()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _carSpeed * Time.fixedDeltaTime * _movement);
        }
        // Update is called once per frame
    }
}