using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingControl : MonoBehaviour {

	GridMap gridMap;
	CameraControl cameraControl;

	public GameObject particlePrefab;
	public GameObject particleEffect;

	// Use this for initialization
	void Start () {
		gridMap = GameObject.Find("Plane").GetComponent<GridMap>();
		// gridMap = GameObject.Find("Terrain").GetComponent<GridMap>();
		cameraControl = GameObject.Find("Camera/Main Camera (1)").GetComponent<CameraControl>();
		particleEffect = (GameObject)Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
		Destroy(particleEffect, particleEffect.GetComponent<ParticleSystem>().duration - 3.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	/*	if(gridMap.moveButton.enabled)
			if(Input.GetMouseButton(1))
			{
				gridMap.moveButton.enabled = false;
				gridMap.moveButton.image.enabled = false;
				gridMap.roationButton.enabled = false;
				gridMap.roationButton.image.enabled = false;
			}*/
	}

	private void OnMouseDown()
	{
		gridMap.moveButton.enabled = true;
		gridMap.moveButton.image.enabled = true;
		gridMap.roationButton.enabled = true;
		gridMap.roationButton.image.enabled = true;
		gridMap.clickedObject = transform.gameObject;
	}

	public void MoveBuilding()
	{
		if (gridMap.clickedObject == null)
			return;

		switch (gridMap.clickedObject.tag)
		{
			case "Crystal":
				if (gridMap.clickedObject.transform.position != transform.position)
					return;
				cameraControl.overObject = Instantiate(gridMap.overMine);
				GridMap.kindBuild = GridMap.KindBuild.mine;
				gridMap.wall[GridMap.GetGridNumber(transform.position)] = false;
				gridMap.moveButton.enabled = false;
				gridMap.moveButton.image.enabled = false;
				gridMap.roationButton.enabled = false;
				gridMap.roationButton.image.enabled = false;
				Destroy(gridMap.clickedObject);
				gridMap.clickedObject = null;
				break;
			case "Blacksmith":
				if (gridMap.clickedObject.transform.position != transform.position)
					return;

				cameraControl.overObject = Instantiate(gridMap.overBlacksmith);
				GridMap.kindBuild = GridMap.KindBuild.blacksmith;
				gridMap.wall[GridMap.GetGridNumber(transform.position)] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position) + GridMap.GetGridNumber(new Vector3(-1, 0, 0))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position) + GridMap.GetGridNumber(new Vector3(0, 0, -1))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position) + GridMap.GetGridNumber(new Vector3(-1, 0, -1))] = false;
				gridMap.moveButton.enabled = false;
				gridMap.moveButton.image.enabled = false;
				gridMap.roationButton.enabled = false;
				gridMap.roationButton.image.enabled = false;
				Destroy(gridMap.clickedObject);
				break;
			case "Farm":
				if (gridMap.clickedObject.transform.position != transform.position)
					return;

				cameraControl.overObject = Instantiate(gridMap.overFarm);
				GridMap.kindBuild = GridMap.KindBuild.farm;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0, 0, 0.5f))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position- new Vector3(0, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(1, 0, 0))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(0, 0, 1))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(-1, 0, 0))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(1, 0, 1))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(-1, 0, 1))] = false;
				gridMap.moveButton.enabled = false;
				gridMap.moveButton.image.enabled = false;
				gridMap.roationButton.enabled = false;
				gridMap.roationButton.image.enabled = false;
				Destroy(gridMap.clickedObject);
				gridMap.clickedObject = null;
				break;
			case "Windmill":
				if (gridMap.clickedObject.transform.position != transform.position)
					return;

				cameraControl.overObject = Instantiate(gridMap.overWindmill);
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0.5f, 0, 0.5f))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0.5f, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(1, 0, 0))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0.5f, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(0, 0, 1))] = false;
				gridMap.wall[GridMap.GetGridNumber(transform.position - new Vector3(0.5f, 0, 0.5f)) + GridMap.GetGridNumber(new Vector3(1, 0, 1))] = false;
				GridMap.kindBuild = GridMap.KindBuild.windmill;
				gridMap.moveButton.enabled = false;
				gridMap.moveButton.image.enabled = false;
				gridMap.roationButton.enabled = false;
				gridMap.roationButton.image.enabled = false;
				Destroy(gridMap.clickedObject);
				gridMap.clickedObject = null;
				break;
		}
	}

	public void RotationButton()
	{
		if (gridMap.clickedObject == null || gridMap.clickedObject.transform.position != transform.position)
			return;
		switch(gridMap.clickedObject.tag)
		{
			case "Crystal":
			case "Blacksmith":
			case "Windmill":
			case "Farm":
				gridMap.clickedObject.transform.eulerAngles += new Vector3(0, 90, 0);
				break;
			//	gridMap.clickedObject.transform.eulerAngles += new Vector3(0, 90, 0);
			//	break;
		}
	}
}
