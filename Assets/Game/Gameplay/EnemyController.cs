using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class EnemyController : MonoBehaviour
    {
        [SerializeField] GameObject targetpos;

        [SerializeField] Transform eggThief;

        [SerializeField] Animator animator;
        internal Animator Animator => animator;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log(animator);
                animator.SetTrigger("Catch");
                DOVirtual.DelayedCall(0.2f, delegate
                {
                    Transform egg = Instantiate(eggThief, targetpos.transform.position, targetpos.transform.rotation);
                    egg.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
                    egg.parent = transform;
                });
                
            }
        }
        //    internal enum EnemyType
        //    {
        //        Idle,
        //        Patrol
        //    }
        //    [SerializeField] EnemyType type;
        //    [SerializeField] float delta = 1f;
        //    [SerializeField] float speed = 2.0f;
        //    private Vector3 startPos;
        //    [SerializeField] private GameObject targetpoint;
        //    [SerializeField] private GameObject startpoint;
        //    // [SerializeField] private float disToWalk;
        //    private Vector3 originPos;
        //    [SerializeField] private float range = 0.2f;
        //    private void Start()
        //    {
        //        startPos = transform.position;
        //    }

        //    private void Update()
        //    {
        //        Vector3 v = startPos;
        //        v.x += delta * Mathf.Sin(Time.time * speed);
        //        if (delta * Mathf.Sin(Time.time * speed) > 0)
        //        {
        //            transform.localEulerAngles = Vector3.up * 180;
        //        }
        //        else
        //        {
        //            transform.localEulerAngles = -Vector3.up * 180;
        //        }
        //        transform.position = v;
        //        //transform.Rotate(rotation * Time.smoothDeltaTime);
        //    }
        //}
    }
}
