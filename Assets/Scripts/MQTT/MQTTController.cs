using System;
using System.Collections;
using System.Collections.Generic;
using Movements;

using UnityEngine;

//This script is used to actively use the values received by the MQTT client
namespace MQTT
{

    public class MQTTController : MonoBehaviour
    {
        public string NameController = "Controller 1";
        public string TagOfTheMQTTReceiver = "MQTT_Receiver";

        private MQTTReceiver _eventSender;
        private CarMovement _carMovement;
        [SerializeField]
        private GameObject car;
        private string isOpen;

        [SerializeField]
        private float carSpeed;
        private Animator animator;
        private Rigidbody2D _rigidbody2D;
        private static readonly int IsOpenDoor = Animator.StringToHash("IsOpenDoor");

        // Start is called before the first frame update
        private void Awake()
        {

            isOpen = "false";
            _eventSender = car.GetComponent<MQTTReceiver>();
            _carMovement = car.GetComponent<CarMovement>();
            _rigidbody2D = car.GetComponent<Rigidbody2D>();
            animator = car.GetComponent<Animator>();
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
            Debug.Log(newMsg);
            if (newMsg.Equals(Operation.OPEN_DOOR))
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
            if (newMsg.Equals(Operation.CLOSE_DOOR))
            {
                animator.SetBool(IsOpenDoor, false);
                isOpen = "false";
            }
        }

        // Update is called once per frame
    }
}