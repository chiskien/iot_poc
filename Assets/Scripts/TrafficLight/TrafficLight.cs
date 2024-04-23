using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private Animator _animator;
    // private static readonly int State = Animator.StringToHash("State");

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // _animator.SetInteger(State, (int)TrafficLightState.RED);
    }

    enum TrafficLightState
    {
        RED = 1,
        YELLOW = 2,
        GREEN = 3
    }
}