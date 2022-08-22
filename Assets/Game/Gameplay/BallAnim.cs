using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
    internal class BallAnim : MonoBehaviour
    {
		private Vector3 originPos;
		[SerializeField] private float range = 0.2f;
		private void Start()
		{
			var model = transform.gameObject;
			originPos = model.transform.localPosition;
			model.transform.DOLocalMoveY(originPos.y + range, 1).SetLoops(-1, LoopType.Yoyo);

		}
	}
}
