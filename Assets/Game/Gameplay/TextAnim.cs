using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funzilla
{
	internal class TextAnim : MonoBehaviour
	{
		private Vector3 originPos;
		[SerializeField] private float range = 3f;
		private void Start()
		{
			var model = transform.gameObject;
			originPos = model.transform.localPosition;
			model.transform.DOLocalMoveY(originPos.y + range, 0.6f).SetLoops(-1, LoopType.Yoyo);
			//model.transform.DOScale(originPos * 0.1f, 1).SetLoops(-1, LoopType.Yoyo);

		}
	}
}

