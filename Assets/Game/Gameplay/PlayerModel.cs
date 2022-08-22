using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Funzilla
{
	internal enum CharacterState
	{
		Idle,
		Walking,
		Lose,
		Win
	}
	internal class PlayerModel : MonoBehaviour
	{
		[SerializeField] [Range(0, 10)] private float moveSpeed = 3;
		[SerializeField] [Range(0, 10)] private float rotateSpeed = 3;
		[SerializeField] [Range(0, 5)] private float distance = 4f;
		[SerializeField] [Range(0, 5)] private float getHitAnimTime = 1.5f;
		[SerializeField] private GameObject _backEgg;
		[SerializeField] private GameObject _playerEgg;
		private float Ani;

        private PlayerEgg playerEgg;

        internal PlayerEgg PlayerEgg => playerEgg;

        private Animator animator;
		internal Animator Animator => animator;


		private Player player;

		internal Player Player => player;


		internal SplineFollower SplineFollower => player.SplineFollower;

		private bool rotateNotIdentity = false;
		private void Awake()
		{
			ReferenceToComponent();
		}

		private void ReferenceToComponent()
		{
			player = GetComponentInParent<Player>();
			animator = GetComponent<Animator>();
			playerEgg = GetComponent<PlayerEgg>();
		}

		private void Update()
		{
			if (player.SplineFollower.follow == true)
			{
				//SoundManager.Instance.PlayWalking();
			}
		}

		internal void MoveTo(float x)
		{
            if (x == 0 && rotateNotIdentity)
            {
                rotateNotIdentity = false;
                transform.DORotateQuaternion(Quaternion.identity, 0.3f);
            }
            if (x == 0) return;
            rotateNotIdentity = true;
            if (x > 0)
            {
                if (transform.localPosition.x > distance) return;
            }
            else
            {
                if (transform.localPosition.x < -distance) return;
            }
            var currentPos = transform.localPosition;
			currentPos.x += x * moveSpeed * Time.smoothDeltaTime;
            transform.localPosition = currentPos;
            x = x > 0 ? 0.5f : -0.5f;
            var position = new Vector3(x, 0, 0.8f);
            Quaternion direction = Quaternion.LookRotation(position);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, direction, Time.deltaTime * rotateSpeed);

        }

        internal IEnumerator IE_OnTouchGoalLine()
		{
			CameraController.Instance.SetActiveCam(0, 10);
			player.SplineFollower.follow = false;
			_backEgg.SetActive(false);
			_playerEgg.SetActive(true);
			_playerEgg.gameObject.transform.localScale = CollisonObject.Instance.egg.transform.localScale * 1.2f;
			//PlayerEgg.IsThrow();
			animator.SetTrigger("ThrowBall");
			

			Movement.Instance.PauseInput();
			yield return new WaitForSeconds(0.1f);
			transform.DORotateQuaternion(Quaternion.identity, GameSettingData.Instance.endTaskRotateTime).OnComplete(
				delegate
				{
					if (Gameplay.Instance.GameState == GameState.End) return;
					CameraController.Instance.SetActiveCam(10, 0);
					CameraController.Instance.FocusGoalLineCam();
					DOVirtual.DelayedCall(1.5f, delegate
					{
						CameraController.Instance.FocusArenaCam();
						DOVirtual.DelayedCall(1.2f, delegate
						{
							ArenaCamUI.Instance.VS.gameObject.SetActive(true);
						});
						
					});
					//player.SplineFollower.follow = true;
					//Movement.Instance.ResumeInput();
				});
		} 


		private float jumpPower = 0.75f;
		
		IEnumerator IE_DoJumpGetHit(float percentRestart)
		{
			var endPosition = player.transform.position - new Vector3(0, 0, GameSettingData.Instance.rangeGetHit) ;
			yield return player.transform.DOJump(endPosition, jumpPower, 1, getHitAnimTime).Play().SetEase(Ease.Linear).WaitForCompletion();
			yield return new WaitForSeconds(0.2f);
			Gameplay.Instance.ChangeState(GameState.End);
			ChangeState(CharacterState.Lose, true);
		}
		


		internal void ChangeState(CharacterState state, bool keepAnim = false)
		{
			switch (state)
			{
				case CharacterState.Idle:
					player.SplineFollower.follow = false;
					break;
				case CharacterState.Walking:
					player.SplineFollower.follow = true;
					break;
				case CharacterState.Lose:
					player.SplineFollower.follow = false;
					Movement.Instance.PauseInput();
					break;
				case CharacterState.Win:
					player.SplineFollower.follow = false;
					break;
			}
		}

		internal void AnimDie()
        {
			animator.SetTrigger("Die");
        }
		
	}
}