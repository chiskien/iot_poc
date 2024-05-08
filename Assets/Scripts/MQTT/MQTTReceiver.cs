using System;
using System.Collections;
using System.Collections.Generic;
using M2MqttUnity;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MQTTReceiver : M2MqttUnityClient
{
    // Start is called before the first frame update
    [Header("MQTT Topics")]
    [Tooltip("Set the topic to subscribers. ")]
    public string topicSubscribe = "test";
    public string topicPublish = "";
    public string messagePublish = "";

    private string _message;
    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            if (_message == value) return;
            _message = value;
            OnMessageArrived?.Invoke(_message);
        }
    }
    // The variables msg and isConnected use the C# properties GET/SET. Instead of using an Update function or a Coroutine, by using these properties it is possible to create a more efficient event system.

    public event OnMessageArrivedDelegate OnMessageArrived;
    public delegate void OnMessageArrivedDelegate(string newMessage);
    public bool autoTest = false;
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

    private readonly List<string> eventMessages = new List<string>();

    public void Publish()
    {
        Client.Publish(topicPublish, System.Text.Encoding.UTF8.GetBytes(messagePublish), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
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
        IsConnected = true;
        if (autoTest)
        {
            Publish();
        }
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
        Debug.Log("Subscribe to topic");
    }

    protected override void UnsubscribeTopics()
    {
        Client.Unsubscribe(new string[] { topicSubscribe });
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void DecodeMessage(string topic, byte[] msg)
    {
        Message = System.Text.Encoding.UTF8.GetString(msg);
        Debug.Log("Received: " + Message);
        Debug.Log("from topic: " + _message);

        StoreMessage(Message);
        if (topic == topicSubscribe)
        {
            if (autoTest)
            {
                autoTest = false;
                Disconnect();
            }
        }
    }

    private void StoreMessage(string message)
    {
        if (eventMessages.Count > 50)
        {
            eventMessages.Clear();
        }
        eventMessages.Add(message);
    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
        if (autoTest)
        {
            autoConnect = true;
        }
    }
}
