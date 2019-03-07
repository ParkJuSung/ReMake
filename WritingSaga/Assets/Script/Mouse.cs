using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {


	[SerializeField]private float distance = 5.0f;
	GridMap gridMap;
	// Use this for initialization
	void Start()
	{
		gridMap = GameObject.Find("Plane").GetComponent<GridMap>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 MousePosition = Input.mousePosition + new Vector3(0.0f, 5.0f, 0.0f);
		MousePosition.z = distance;
		transform.position = Camera.main.ScreenToWorldPoint(MousePosition);

		if (Input.GetMouseButtonDown(1))
		{
			GridMap.kindBuild = GridMap.KindBuild.none;
			gridMap.moveButton.enabled = false;
			if (gameObject.tag == "Blacksmith")
			{
				SResource.Instance.GOLD += 150;
				SResource.Instance.IRON += 100;
			}
			else if (gameObject.tag == "Farm")
			{
				SResource.Instance.GOLD += 150;
				SResource.Instance.FOOD += 100;
			}
			else if(gameObject.tag == "Crystal")
			{
				SResource.Instance.GOLD += 300;
				SResource.Instance.IRON += 200;
			}

			Destroy(gameObject);
		}
	}
}
