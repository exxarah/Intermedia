using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RotateObject : MonoBehaviour
    {
        public float speedFactor;

        public void Update()
        {
            this.transform.Rotate(0f, speedFactor * Time.deltaTime, 0f);
        }
    }
}