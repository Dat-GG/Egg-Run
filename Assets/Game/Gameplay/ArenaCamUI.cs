using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
    public class ArenaCamUI : MonoBehaviour
    {
        //public GameObject hpBossTarget;

        //public GameObject hpMonsterTarget;

        public Image VS;

        public Image winBG;

        //public Image hpboss;

        public GameObject hpbosstarget;

        public GameObject hpmonstertarget;

        public Text taptap;


        public static ArenaCamUI Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}