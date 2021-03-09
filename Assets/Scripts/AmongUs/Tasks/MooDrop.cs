using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooDrop : Task
{
	[SerializeField] private Transform _leftHandle;
	[SerializeField] private Transform _rightHandle;
	[SerializeField] private Transform _mooContainer;

	private bool _isReleasing;

	private bool _isFinished;
	
	public void ButtonEvt_ReleaseHandles()
	{
		_isReleasing = true;
	}

	public void ButtonEvt_StopHandles()
	{
		_isReleasing = false;
	}

	protected override void Update()
	{
		base.Update();
		
		if (!_isFinished && _mooContainer.transform.childCount == 0)
		{
			_isFinished = true;
			Evt_FinishTask("","");
		}
		
		if (!_isReleasing)
			return;
		if (_leftHandle.rotation.eulerAngles.z >= 270 || Math.Abs(_leftHandle.rotation.eulerAngles.z) < 0.01)
			_leftHandle.rotation = _leftHandle.rotation * Quaternion.Euler(0, 0, -1);
		if (_rightHandle.rotation.eulerAngles.z <= 90)
			_rightHandle.rotation = _rightHandle.rotation * Quaternion.Euler(0, 0, 1);

	}
}
