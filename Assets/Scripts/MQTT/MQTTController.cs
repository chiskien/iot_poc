using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to actively use the values received by the MQTT client

public class MQTTController : MonoBehaviour
{
    public string NameController = "Controller 1";
    public string TagOfTheMQTTReceiver = "";
    public MQTTReceiver _eventSender;
    public GameObject car;  
            private static readonly int IsOpenDoor = Animator.StringToHash("IsOpenDoor");

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.FindGameObjectWithTag(TagOfTheMQTTReceiver);
        _eventSender = GameObject.FindGameObjectsWithTag(TagOfTheMQTTReceiver)[0].gameObject
        .GetComponent<MQTTReceiver>();
        animator = car.GetComponent<Animator>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }
    private void OnMessageArrivedHandler(string newMsg)
    {
        Debug.Log(newMsg);
        if (newMsg == "open_door")
        {
            Debug.Log("Event Fired. The message, from Object " + NameController + " is = " + newMsg);
            animator.SetBool(IsOpenDoor, true);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
