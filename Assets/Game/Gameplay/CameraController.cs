
using System;
using Cinemachine;
using UnityEngine;

namespace Funzilla
{
	internal class CameraController : MonoBehaviour
	{
		public static CameraController Instance;
		[SerializeField] private CinemachineVirtualCamera followCam;
		[SerializeField] private CinemachineVirtualCamera taskCam;
		[SerializeField] private CinemachineVirtualCamera goalLineCam;
		[SerializeField] private CinemachineVirtualCamera arenaCam;
		[SerializeField] private bool useTaskCam = true;
		internal CinemachineVirtualCamera FightingCam => followCam;

		[SerializeField] private ParticleSystem completeFX1;
		[SerializeField] private ParticleSystem completeFX2;

		private void Awake()
		{
			Instance = this;
			followCam.Priority = 10;
			taskCam.Priority = 0;
		}

		internal void SetActiveCam(int followCamPriority, int taskCamPriority)
		{
			if (!useTaskCam) return;
			followCam.Priority = followCamPriority;
			taskCam.Priority = taskCamPriority;
        }
        internal void PlayCompleteFx()
        {
            completeFX1.Play();
            completeFX2.Play();
        }

        internal void FocusGoalLineCam()
        {
			followCam.gameObject.SetActive(false);
			goalLineCam.gameObject.SetActive(true);
        }

		internal void FocusArenaCam()
        {
			goalLineCam.gameObject.SetActive(false);
			arenaCam.gameObject.SetActive(true);
        }

		private void Update()
		{
			// if(Time.frameCount % 5 != 0) return;
			// var r = followCam.transform.rotation;
			// r.y = Gameplay.Instance.PlayerModel.transform.rotation.y;
			// followCam.transform.rotation = r;
		}
	}
}
