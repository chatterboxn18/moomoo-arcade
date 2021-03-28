using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
	private bool _isClosable;
	[SerializeField] private SimpleButton _exitButton;
	public Action<string, string> Evt_FinishTask = delegate {  };
	public Action Evt_ExitTask = delegate {  };
	protected string _parameter;
	
	public GameObject WorldObject;
	protected Dictionary<string, string> _taskData;

	protected bool _isCompleted;

	public void SetTaskData(Dictionary<string, string> taskData)
	{
		_taskData = taskData; 
	}

	public void SetParamter(string parameter)
	{
		_parameter = parameter;
	}
	protected virtual void Awake()
	{
		
	}

	protected virtual IEnumerator Start()
	{
		if (WorldObject != null)
		{
			WorldObject.transform.parent = transform.parent.parent;
			WorldObject.transform.localPosition = new Vector3(0,0,100);
			WorldObject.transform.localScale = Vector3.one;
		}

		yield return null;
	}
	
	protected virtual void Update()
	{
	}
	
	public void ButtonEvt_ExitTask()
	{
		Evt_ExitTask();
	}

}
