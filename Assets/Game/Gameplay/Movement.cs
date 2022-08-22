using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Facebook.Unity;
using UnityEngine;

namespace Funzilla
{
	internal class Movement : MonoBehaviour
	{
		private PlayerModel playerModel;
		[SerializeField] private FloatingJoystick joystick;

		private bool pauseInput = false;
		public static Movement Instance;
		

		private void Awake()
		{
			Instance = this;
			playerModel = GetComponentInChildren<PlayerModel>();
		}
		
		private void Update()
		{
			if (pauseInput || !joystick.enabled) return;
			playerModel.MoveTo(joystick.Direction.x);
		
		}
		internal void PauseInput()
		{
			joystick.enabled = false;
			pauseInput = true;
		}

		internal void ResumeInput()
		{
			joystick.gameObject.SetActive(false);
			joystick.gameObject.SetActive(true);
			joystick.enabled = true;
			joystick.Restart();
			pauseInput = false;
		}
	}
}
