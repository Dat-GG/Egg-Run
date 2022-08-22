using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class SawController : MonoBehaviour
    {
        [SerializeField] float delta = 1f;
        [SerializeField] float speed = 2.0f;
        [SerializeField] Vector3 rotation;
        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
        }

        private void Update()
        {
            Vector3 v = startPos;
            v.x += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
            transform.Rotate(rotation * Time.smoothDeltaTime);
        }
    }
}
