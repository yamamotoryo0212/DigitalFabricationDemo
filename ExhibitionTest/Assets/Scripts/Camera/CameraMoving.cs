using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
	[SerializeField]
	private float _sensitivity = 0;
	[SerializeField]
	private float _speed = 0;
	private Vector3 _lastMousePosition = Vector3.zero;
	private Vector3 _newAngle = Vector3.zero;
	private Rigidbody _rigidbody = null;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_newAngle = gameObject.transform.localEulerAngles;
		_lastMousePosition = Input.mousePosition;
	}

	private void Update()
	{
		ViewpointShift();
		CameraMove();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UnityEditor.EditorApplication.isPlaying = false;
		}
	}

	private void ViewpointShift()
	{
		if (Cursor.lockState == CursorLockMode.None)
		{
			_newAngle.y += ((Input.mousePosition.x - _lastMousePosition.x) * _sensitivity);
			_newAngle.x -= ((Input.mousePosition.y - _lastMousePosition.y) * _sensitivity);
			_newAngle.x = Mathf.Min(_newAngle.x, 90);
			_newAngle.x = Mathf.Max(_newAngle.x, -90);

			gameObject.transform.localEulerAngles = _newAngle;
		}

		if (Mathf.Abs(Screen.width / 2 - Input.mousePosition.x) > 50 || Mathf.Abs(Screen.height / 2 - Input.mousePosition.y) > 50)
		{
			Cursor.lockState = CursorLockMode.Locked;
			_lastMousePosition = Input.mousePosition;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			_lastMousePosition = Input.mousePosition;
		}
	}

	private void CameraMove()
	{
		if (Input.GetKey(KeyCode.W))
		{
			_rigidbody.velocity = new Vector3(gameObject.transform.forward.x * _speed, gameObject.transform.forward.y * _speed, gameObject.transform.forward.z * _speed);
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			_rigidbody.velocity = Vector3.zero;
		}
		if (Input.GetKey(KeyCode.S))
		{
			_rigidbody.velocity = new Vector3(-gameObject.transform.forward.x * _speed, -gameObject.transform.forward.y * _speed, -gameObject.transform.forward.z * _speed);
		}
		else if (Input.GetKeyUp(KeyCode.S))
		{
			_rigidbody.velocity = Vector3.zero;
		}
		if (Input.GetKey(KeyCode.A))
		{
			_rigidbody.velocity = new Vector3(-gameObject.transform.right.x * _speed, -gameObject.transform.right.y * _speed, -gameObject.transform.right.z * _speed);
		}
		else if (Input.GetKeyUp(KeyCode.A))
		{
			_rigidbody.velocity = Vector3.zero;
		}
		if (Input.GetKey(KeyCode.D))
		{
			_rigidbody.velocity = new Vector3(gameObject.transform.right.x * _speed, gameObject.transform.right.y * _speed, gameObject.transform.right.z * _speed);
		}
		else if (Input.GetKeyUp(KeyCode.D))
		{
			_rigidbody.velocity = Vector3.zero;
		}
	}
}

