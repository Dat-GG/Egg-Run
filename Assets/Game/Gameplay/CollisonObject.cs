using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

namespace Funzilla
{
    internal class CollisonObject : MonoBehaviour
    {
        public GameObject egg;
        [SerializeField] private float scaleRate;
        [SerializeField] private float scaleRateThief;

        public static CollisonObject Instance;

        [SerializeField] PlayerModel PlayerModel;
        private EnemyController enemy;

        private void Awake()
        {
            Instance = this;

            enemy = FindObjectOfType<EnemyController>();
        }



        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Ball"))
            {
                //col.gameObject.SetActive(false);
                egg.transform.localScale += Vector3.one * scaleRate;
                DOVirtual.DelayedCall(0.3f, delegate
                {
                    egg.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
                });
                //egg.transform.localPosition += new Vector3(0.025f, 0, -0.05f);
            }
            if (col.gameObject.CompareTag("Saw"))
            {
                PlayerModel.AnimDie();
                egg.transform.DOLocalMove(egg.transform.position - new Vector3(0, 0, 2), 2f);
                DOVirtual.DelayedCall(1f, delegate
                {
                    //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                    //Time.timeScale = 0;
                    Gameplay.Instance.ChangeState(GameState.End);
                });
                
            }

            if (col.gameObject.CompareTag("Enemy"))
            {

                egg.transform.localScale -= Vector3.one * scaleRateThief;
                //DOVirtual.DelayedCall(0.6f, delegate
                //{
                //    egg.transform.localScale += new Vector3(0.35f, 0.35f, 0.35f);
                //});
                //egg.transform.localPosition -= new Vector3(0.05f, 0, 0);
            }        
        }

    }
}