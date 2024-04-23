using System;
using UnityEngine;

namespace Movements
{
    public class CameraFollower : MonoBehaviour
    {
        public GameObject car;

        private void Update()
        {
            transform.position = car.transform.position + new Vector3(0, 1, -5);
        }
    }
}