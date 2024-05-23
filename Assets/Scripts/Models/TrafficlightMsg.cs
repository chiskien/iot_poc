using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;

[Serializable]
public class TrafficlightMsg
{
    public string id;
    public string state;
    public override string ToString()
    {
        return "Id: " + id + ", State: " + state;
    }
}