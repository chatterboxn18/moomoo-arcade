using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
	[SerializeField] private CanvasGroup _canvasGroup;
	private float _fadeOutTime = 2f;
	private float _timer = 0;
	private IEnumerator Start()
	{
		while (!MMMAUResourceManager.IsDataReady)
		{
			yield return null;
		}

		while (_timer < _fadeOutTime)
		{
			_canvasGroup.alpha = 1- (_timer / _fadeOutTime);
			_timer += Time.deltaTime;
		}

		_canvasGroup.alpha = 0;
		_canvasGroup.gameObject.SetActive(false);
		yield return null;
	}
}
