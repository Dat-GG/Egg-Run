using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Funzilla
{
    public class PlayerEgg : MonoBehaviour
    {
        [SerializeField] Rigidbody Rigidbody;
        [SerializeField] private ParticleSystem smoke;

        private Player player;
        private float ani;


        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            DOVirtual.DelayedCall(1.9f, delegate { IsThrow(); });
            
        }

        internal void IsThrow()
        {
            
            Rigidbody.transform.parent = null;
            Rigidbody.isKinematic = false;
            //transform.localScale = CollisonObject.Instance.egg.transform.localScale * 2;
            ani += Time.smoothDeltaTime;
            ani = ani % 2f;
            var a = transform.position;

            //transform.position = Vector3.MoveTowards(a, Arena.Instance.targetpos.transform.position, 20 * (Time.smoothDeltaTime /1.2f));
            transform.position = ParabolMath.Parabola(a, Arena.Instance.targetpos.transform.position, 2f, ani / 2f);
            smoke.Play();
        }
    }
}
