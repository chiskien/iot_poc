using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MqttTrafficLightController : MonoBehaviour
{

    private MqttTrafficLightReceiver _eventSender;
    private string TagOfTheMQTTReceiver = "1";
    private GameObject trafficLightGameObject;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        trafficLightGameObject = GameObject.FindGameObjectWithTag("TF1");
        _eventSender = trafficLightGameObject.GetComponent<MqttTrafficLightReceiver>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMessageArrivedHandler(TrafficlightMsg trafficlightMsg)
    {
        Debug.Log(trafficlightMsg.ToString());
        TagOfTheMQTTReceiver = trafficlightMsg.id;
        trafficLightGameObject = GameObject.FindGameObjectWithTag(TagOfTheMQTTReceiver);
        _eventSender = trafficLightGameObject.GetComponent<MqttTrafficLightReceiver>();
        animator = trafficLightGameObject.GetComponent<Animator>();
        switch (trafficlightMsg.state)
        {
            case "RED":
                animator.SetInteger("State", 1);
                break;
            case "YELLOW":
                animator.SetInteger("State", 2);
                break;
            case "GREEN":
                animator.SetInteger("State", 3);
                break;
            default:
                Debug.Log("Not found any state");
                break;
        }
    }
}
