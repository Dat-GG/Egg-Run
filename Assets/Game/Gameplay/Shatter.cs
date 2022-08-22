using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class Shatter : MonoBehaviour
    {
        private Rigidbody[] ribs;
        [SerializeField]
        private float minForce = 0.1f, maxForce = 0.3f;
        private float force;

        // [SerializeField] float fallMinDuration = 3;
        // [SerializeField] float fallMaxDuration = 5;

        private void Awake()
        {
            ribs = this.GetComponentsInChildren<Rigidbody>();
        }

        internal void Explosion(Transform actor)
        {
            Vector3 direction = Vector3.zero;
            foreach (var rib in ribs)
            {

                direction = (rib.transform.position - actor.position) + Vector3.up;
                direction.Normalize();
                force = Random.Range(minForce, maxForce);

                rib.AddForce(direction * force, ForceMode.Impulse);
            }
            
            StartCoroutine(HiddenAll());
		
        }

        private IEnumerator HiddenAll()
        {
            yield return new WaitForSeconds(3);
            foreach (var rib in ribs)
            {

                var col = rib.transform.gameObject.GetComponent<MeshCollider>();
                col.enabled = false;
			
            }
            yield return new WaitForSeconds(3);
            Destroy(transform.parent.gameObject);
        }
    }
}
