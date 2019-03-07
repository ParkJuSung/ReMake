using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public float cameraMoveSpeed = 120.0f;
	public float inputSensitivity = 150.0f;
	public float mouseX;
	public float mouseY;
	public float rotY;
	public float rotX;
	// Use this for initialization
	void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rotX = rot.x;
		rotY = rot.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
		{

			mouseY = Input.GetAxis("Mouse Y");
			mouseX = Input.GetAxis("Mouse X");
			//rotY += mouseY * inputSensitivity * Time.deltaTime;

			rotX += mouseY * inputSensitivity * Time.deltaTime;
			rotY += mouseX * inputSensitivity * Time.deltaTime;

			rotX = Mathf.Clamp(rotX, 30, 75);

			Quaternion localRotationX = Quaternion.Euler(rotX, rotY, 0.0f);
			transform.rotation = localRotationX;
			transform.parent.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
			//Quaternion localRotation = Quaternion.Euler(transform.eulerAngles.x, rotY, 0.0f);
			//transform.rotation = localRotation;
		}

		if (Input.GetMouseButton(2))
		{
			transform.parent.Translate(new Vector3(-(Input.GetAxis("Mouse X") * transform.GetComponent<Camera>().orthographicSize * Time.deltaTime), 0,
			-(Input.GetAxis("Mouse Y") * transform.GetComponent<Camera>().orthographicSize * Time.deltaTime)));

		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			RaycastHit hit;
			Ray ray = this.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			Vector3 desiredPosition;

			if (Physics.Raycast(ray, out hit))
			{
				desiredPosition = hit.point;
			}
			else
			{
				desiredPosition = transform.position;
			}
			float distance = Vector3.Distance(desiredPosition, transform.position);
			if(distance<15 && Input.GetAxis("Mouse ScrollWheel") < 0)
			{
					Vector3 direction = Vector3.Normalize(desiredPosition - transform.position) * (distance * Input.GetAxis("Mouse ScrollWheel"));
					transform.position += direction;
			}

			if(distance>2 && Input.GetAxis("Mouse ScrollWheel") > 0)
			{
					Vector3 direction = Vector3.Normalize(desiredPosition - transform.position) * (distance * Input.GetAxis("Mouse ScrollWheel"));
					transform.position += direction;
			}
		}
	}
}
