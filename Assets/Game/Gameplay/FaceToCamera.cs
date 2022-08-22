using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class FaceToCamera : MonoBehaviour
    {
        private Transform cam;
     
        void Awake() {
            cam = Camera.main.transform;
        }
     
        void Update() 
        {
            transform.forward = cam.forward;
            //transform.rotation = Quaternion.LookRotation( transform.position - cam.position );
        }
    }
}
