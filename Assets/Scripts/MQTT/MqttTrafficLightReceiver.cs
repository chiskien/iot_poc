using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using M2MqttUnity;
using Unity.VisualScripting;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MqttTrafficLightReceiver : M2MqttUnityClient
{
    // Start is called before the first frame update
    [Header("MQTT Topics")]
    [Tooltip("Set the topic to subscribers. ")]
    public string topicSubscribe = "M2MQTT/trafficlight";
    public string topicPublish = "";
    public string messagePublish = "";

    private TrafficlightMsg _message;
    public TrafficlightMsg Message
    {
        get
        {
            return _message;
        }
        set
        {
            _message = value;
            OnMessageArrived?.Invoke(_message);
        }
    }
    // The variables msg and isConnected use the C# properties GET/SET. Instead of using an Update function or a Coroutine, by using these properties it is possible to create a more efficient event system.

    public event OnMessageArrivedDelegate OnMessageArrived;
    public delegate void OnMessageArrivedDelegate(TrafficlightMsg newMessage); //change the input according to the message received  
    private bool _isConnected;

    public bool IsConnected
    {
        get
        {
            return _isConnected;
        }
        set
        {
            if (_isConnected == value) return;
            _isConnected = true;
            OnConnectionSucceed?.Invoke(IsConnected);
        }
    }
    public event OnConnectionSucceedDelegate OnConnectionSucceed;
    public delegate void OnConnectionSucceedDelegate(bool isConnected);
    public void Publish()
    {
        Client.Publish(topicPublish, System.Text.Encoding.UTF8.GetBytes(messagePublish),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    protected override void OnConnecting()
    {
        Debug.Log("Connecting");
        base.OnConnecting();
    }
    protected override void OnConnected()
    {
        base.OnConnected();
    }
    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("Disconnected");
    }
    protected override void OnConnectionLost()
    {
        Debug.Log("Connection lost");
    }
    protected override void SubscribeTopics()
    {
        Client.Subscribe(new string[] { topicSubscribe }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        Debug.Log("Subscribe to topic" + topicSubscribe);
    }

    protected override void UnsubscribeTopics()
    {
        Client.Unsubscribe(new string[] { topicSubscribe });
    }

    protected override void DecodeMessage(string topic, byte[] msg)
    {
        string jsonData = Encoding.UTF8.GetString(msg);
        TrafficlightMsg trafficlightMsg = JsonUtility.FromJson<TrafficlightMsg>(jsonData);
        Message = trafficlightMsg;
        Debug.Log(trafficlightMsg.ToString());
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()
    }

    private void OnDestroy()
    {
        Disconnect();
    }
}
