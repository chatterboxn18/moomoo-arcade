using System;
using System.Collections;
using System.Collections.Generic;
using AmongUs;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
	[SerializeField] private CanvasGroup _groupTasks;
	[SerializeField] private Transform _taskCanvas;
	[SerializeField] private Transform _mapTransform;
	
	private Task _currentTask;

	public enum TaskType
	{
		None = 0,
		Music = 1, 
		News = 2, 
		Videos = 3
	}

	[SerializeField] private MooTasks _taskSOData;
	private MooTasks.TaskObject[] _taskObjects;
	private float _timer;

	[Header("Displaying Task")]
	[SerializeField] private bool _isCreatingTask;
	[SerializeField] private float _revealTaskTime = 1;
	[SerializeField] private TaskController _taskControllerPrefab;
	
	[Header("Cleaning Task")]
	[SerializeField] private bool _isCleaningTask;
	private float _timeToCleanUp = .5f;
	[SerializeField] private float _cleanTaskTime = 1;

	private Dictionary<string, string> _taskData = new Dictionary<string, string>();
	
	[Header("Task Management")]
	private int _currentTaskIndex = -1;
	[SerializeField] private UseButton _useButton;

	[Header("DEBUG")] 
	[SerializeField] private int _DEBUGIndex;
	
	////////////////////
	private void Awake()
	{
		var taskData = _taskSOData.Tasks;
		_taskObjects = new MooTasks.TaskObject[taskData.Count];
		var counter = 0; 
		foreach (var task in taskData)
		{
			_taskObjects[counter] = task;
			counter++;
		}
	}

	private IEnumerator Start()
	{
		//_currentTaskIndex = _DEBUGIndex;

		while (!MMMAUResourceManager.IsDataReady)
		{
			yield return null;
		}
		
		for (var i = 0; i < _taskObjects.Length; i++)
		{
			CreateTask(i);
		}
	}
	
	private void Update()
	{
		if (_isCreatingTask)
		{
			if (_groupTasks.alpha <= 0)
			{
				var taskToMake = _taskObjects[_currentTaskIndex];
				_currentTask = Instantiate(taskToMake.Task, _taskCanvas);
				_currentTask.Evt_FinishTask = CleanTask;
				_currentTask.Evt_ExitTask = ExitTask;
				_currentTask.SetTaskData(_taskData);
				_currentTask.SetParamter(taskToMake.Paramters);
			}
			_timer += Time.deltaTime;
			_groupTasks.alpha = _timer / _revealTaskTime;
			if (_groupTasks.alpha >= 1)
			{
				_isCreatingTask = false;
				_timer = 0;
			}

			return;
		}
		
		if (_isCleaningTask)
		{
			if (_timeToCleanUp > 0)
			{
				_timeToCleanUp -= Time.deltaTime;
				return;
			}
			_timer += Time.deltaTime;
			_groupTasks.alpha = 1 - (_timer / _cleanTaskTime);
			if (_groupTasks.alpha <= 0)
			{
				_isCleaningTask = false;
				if (_currentTask.WorldObject != null)
					Destroy(_currentTask.WorldObject.gameObject);
				Destroy(_currentTask.gameObject);
				_currentTask = null;
				_timeToCleanUp = _cleanTaskTime;
				_timer = 0;
			}
		}
	}
	
	///////////////////////
	/// Task Management ///
	///////////////////////

	public void CreateTask(int index)
	{
		var taskController = Instantiate(_taskControllerPrefab, _mapTransform);
		var task = _taskObjects[index];
		taskController.transform.localPosition = task.Position;
		taskController.SetTaskNumber(index);
		if (task.ItemSprites.Length > 0)
			taskController.SetSprites(task.ItemSprites[0], task.ItemSprites[1]);
		taskController.Evt_PlayerEnter = Evt_SetTask;
		taskController.Evt_PlayerExit = Evt_LeaveTask;
		taskController.name = _taskObjects[index].Title;
	}
	
	public void SetTaskIndex(int index)
	{
		_currentTaskIndex = index;
	}

	public void PrepareTask(int index)
	{
		_currentTaskIndex = index;
		_isCreatingTask = true;
	}

	public void CleanTask(string key = "", string value = "")
	{
		if (!string.IsNullOrEmpty(key))
		{
			if (_taskData.ContainsKey(key))
			{
				var prevValue = _taskData[key];
				_taskData[key] = prevValue + value;
				if (_taskData[key] == _taskObjects[_currentTaskIndex].Conditions)
					Debug.Log("Done");
			}
			else
			{
				_taskData[key] = value;
			}
		}
		_isCleaningTask = true;
	}

	public void ExitTask()
	{
		_timeToCleanUp = _cleanTaskTime;
		_isCleaningTask = true;
	}
	
	//////////////////
	/// BTN Events ///
	//////////////////
	
	public void ButtonEvt_DoTask()
	{
		if (_currentTaskIndex == -1)
			return;
		PrepareTask(_currentTaskIndex);
	}

	//////////////
	/// Events ///
	//////////////
	
	public void Evt_SetTask(int index)
	{
		_currentTaskIndex = index;
		_useButton.SetButtonType(_taskObjects[_currentTaskIndex].Type);
	}

	public void Evt_LeaveTask()
	{
		_currentTaskIndex = -1; 
		_useButton.SetVisibility(false);
	}
	
}


