using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class Ragdoll : MonoBehaviour
    {
        private bool falled = false;
        public Transform HipTransform;
        private Rigidbody[] rigidbodies;
        [SerializeField] Rigidbody hip;
        [SerializeField] private float force = 100;
	
        private void Awake()
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            DeActive();
        }

        private void DeActive()
        {
            foreach (var rib in rigidbodies)
            {
                rib.isKinematic = true;
                var col = rib.GetComponent<Collider>();
                if (col)
                {
                    col.enabled = false;
                }
            }
        }


        internal void Active( Vector3 direction)
        {
            foreach (var rib in rigidbodies)
            {
                rib.isKinematic = false;
                var col = rib.GetComponent<Collider>();
                if (col)
                {
                    col.enabled = true;
                }
            }

            hip.AddForce(direction * force, ForceMode.Impulse);
        }

        private void Update()
        {
            if (falled)
                return;

            if (rigidbodies[0].isKinematic)
                return;

            if (rigidbodies[0].position.y < 0.2f)
            {
                foreach (var rib in rigidbodies)
                {
                    var col = rib.GetComponent<Collider>();
                    col.enabled = false;
                    rib.useGravity = false;
                }
			
                falled = true;
            }
        }
    }
}
