using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class GoalLine : MonoBehaviour
    {
        private Collider _collider;
        internal Collider Collider => _collider;


        private void Awake()
        {       
            ReferenceToComponent();
        }

        private void ReferenceToComponent()
        {
            _collider = GetComponent<Collider>();

        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;           
            var player = other.transform.parent.gameObject.GetComponent<PlayerModel>();           
            StartCoroutine(player.IE_OnTouchGoalLine());
        }
    }
                
}

