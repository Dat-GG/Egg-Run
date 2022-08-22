using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Funzilla
{
	internal class GameplayUI : MonoBehaviour
	{
		public static GameplayUI Instance;
		[SerializeField] private Text tapToPlayText;
		[SerializeField] private FloatingJoystick joystick;
		[SerializeField] private Button restartBtn;
		internal bool EnableInput => joystick.enabled;

		private void Awake()
		{
			Instance = this;
			HiddenUI(false);
			restartBtn.onClick.AddListener(OnRestartBtnClick);
		}

		private void OnRestartBtnClick()
		{
			SceneManager.Instance.ReloadScene(SceneID.Gameplay);
			SceneManager.Instance.CloseScene(SceneID.Lose);
		}

		internal void HiddenUI(bool active)
		{
			tapToPlayText.gameObject.SetActive(active);
		}

	
	}

}