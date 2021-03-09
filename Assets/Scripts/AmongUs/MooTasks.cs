using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MMMAU", menuName = "MMMAU/MooTask")]
public class MooTasks : ScriptableObject
{
	public List<TaskObject> Tasks;
	
	[Serializable]
	public struct TaskObject
	{
		public string Title;
		public Vector2 Position;
		public TaskManager.TaskType Type;
		public Task Task;
		public Sprite[] ItemSprites;
		public string Paramters;
		public string Conditions;
	}

	
}
