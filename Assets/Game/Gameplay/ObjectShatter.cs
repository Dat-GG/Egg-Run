using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class ObjectShatter : MonoBehaviour
    {
        [SerializeField] private GameObject beforeObj;
        [SerializeField] private GameObject afterObj;

        private void Awake()
        {
            beforeObj.SetActive(true);
            afterObj.SetActive(false);
        }

        internal void ActiveShatterObj(Transform actor)
        {
            beforeObj.SetActive(false);
            afterObj.SetActive(true);
            GetComponent<Collider>().enabled = false;
            afterObj.GetComponentInChildren<Shatter>().Explosion(actor);
           
        }
    }
}
