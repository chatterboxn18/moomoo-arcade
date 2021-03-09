using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmongUs
{
	public class FourSeasons : Task
	{
		[SerializeField] private string _taskKey;
		
		public void ButtonEvt_Collect()
		{
			var index = int.Parse(_parameter);
			if (_taskData.ContainsKey(_taskKey))
			{
				if (_taskData[_taskKey].Length == index)
					Evt_FinishTask(_taskKey, _parameter);
				else
				{
					Debug.Log("Wrong Order");
				}
			}
			else
			{
				Evt_FinishTask(_taskKey, _parameter);
			}
			
		}
	}
}


