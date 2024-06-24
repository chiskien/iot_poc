using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;

[Serializable]
public class CarMsg
{
    public string id;
    public string action;

    public override string ToString()
    {
        return "id: " + id + " action: " + action;
    }
}