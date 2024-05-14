using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to actively use the values received by the MQTT client

public class MQTTController : MonoBehaviour
{
    public string NameController = "Controller 1";
    public string TagOfTheMQTTReceiver = "MQTT_Receiver";

    private MQTTReceiver _eventSender;

    [SerializeField]
    private GameObject car;
    private string isOpen;
    private Animator animator;
    private static readonly int IsOpenDoor = Animator.StringToHash("IsOpenDoor");

    // Start is called before the first frame update
    private void Awake() {
        isOpen = "false";
    }
    void Start()
    {
        // car = GameObject.FindGameObjectWithTag(TagOfTheMQTTReceiver);
        // _eventSender = GameObject.FindGameObjectsWithTag(TagOfTheMQTTReceiver)[0].gameObject
        // .GetComponent<MQTTReceiver>();
        _eventSender = car.GetComponent<MQTTReceiver>();
        animator = car.GetComponent<Animator>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }
    private void OnMessageArrivedHandler(string newMsg)
    {
        Debug.Log(newMsg);
        if (newMsg.Equals("open_door"))
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
    // Update is called once per frame
}
