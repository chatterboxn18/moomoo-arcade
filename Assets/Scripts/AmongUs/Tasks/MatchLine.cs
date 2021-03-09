using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLine : Task
{
	[SerializeField] private LineRenderer _lineRenderer;
	[SerializeField] private Vector3 _startPositions;
	[SerializeField] private Camera _taskCamera;
	private Vector3 _startPosition;

	protected override void Start()
	{
		_lineRenderer.positionCount = 2; 
		_lineRenderer.SetPosition(0, new Vector3(_startPositions.x, _startPositions.y, _startPositions.z));
		_lineRenderer.SetPosition(1, new Vector3(_startPositions.x, _startPositions.y, _startPositions.z));
	}

	public void SetCamera(Camera camera)
	{
		_taskCamera = camera;
	}

	protected override void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_startPosition = _taskCamera.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.GetMouseButton(0))
		{
			var mousePosition = _taskCamera.ScreenToWorldPoint(Input.mousePosition);
			_lineRenderer.SetPosition(1, new Vector3(mousePosition.x-_startPosition.x, mousePosition.y-_startPosition.y, 0f));
		}
	}
}
