using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class BallController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem collectTool;
        [SerializeField] private GameObject aura;
        //[SerializeField] private Transform playerEgg;
        [SerializeField] private float t = 1;
        private Player Player;

        private Animator _animator;
        private bool collected = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Player = FindObjectOfType<Player>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (collected) return;
            collected = true;
            var player = other.transform.parent.gameObject.GetComponent<PlayerModel>();
            if (!player) return;
            collectTool.Play();
            transform.gameObject.SetActive(false);
            aura.SetActive(false);
            Vector3 a = transform.position;
            Vector3 b = Player.transform.position;
            transform.position = Vector3.MoveTowards(a, b, t * Time.smoothDeltaTime);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
