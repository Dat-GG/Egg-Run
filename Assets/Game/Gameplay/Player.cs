using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Playables;

namespace Funzilla
{
	internal class Player : MonoBehaviour
	{
		public static Player Instance;

		public GameObject startpos;
		private SplineFollower splineFollower;

		internal SplineFollower SplineFollower => splineFollower;

		private void Awake()
		{
			Instance = this;
			ReferenceToComponent();
			splineFollower.followSpeed = GameSettingData.Instance.moveSpeed;

		}

		private void ReferenceToComponent()
		{
			splineFollower = GetComponent<SplineFollower>();
		}

        private void Update()
        {
			NextLv();
		}

        public void NextLv()
        {
   //         if (Input.GetMouseButtonDown(0))
   //         {
			//	SceneManager.Instance.ReloadScenes();
			//}
			
        }
		
	}
}
