using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TrafficLight;
using Unity.VisualScripting;
using UnityEngine;

namespace TrafficLight
{

    public class TrafficLight : MonoBehaviour
    {
        // Start is called before the first frame update
        private int trafficLightId;
        private Animator animator;
        private static readonly int State = Animator.StringToHash("State");
        private TrafficState currentState;
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            StartCoroutine(TrafficLightSimu());
        }

        // Update is called once per frame
        void Update()
        {
        }

        private IEnumerator TrafficLightSimu()
        {
            while (true)
            {
                animator.SetInteger(State, 1);
                currentState = TrafficState.RED;
                yield return new WaitForSeconds(1);
                animator.SetInteger(State, 2);
                currentState = TrafficState.YELLOW;
                yield return new WaitForSeconds(1);
                animator.SetInteger(State, 3);
                currentState = TrafficState.GREEN;
                yield return new WaitForSeconds(1);
            }
        }
    }
}