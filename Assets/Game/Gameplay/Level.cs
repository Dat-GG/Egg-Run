using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

namespace Funzilla
{
    internal class Level : MonoBehaviour
    {
        public static Level Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}
