using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Camera main;
	public Camera bulidingCamera;
	public Canvas menuCanvas;
	public Canvas buildCanvas;
	public GridMap gridMap;
	public GameObject overObject;
	public Text text;
	// Use this for initialization
	void Start () {
		buildCanvas.enabled = false;
		gridMap = GameObject.Find("Plane").GetComponent<GridMap>();
		//gridMap = GameObject.Find("Terrain").GetComponent<GridMap>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMainCamera()
	{
		main.depth = -1;
		bulidingCamera.depth = -2;
		menuCanvas.enabled = true;
		buildCanvas.enabled = false;
		text.enabled = false;


	}

	public void SetBulidingCamera()
	{
		main.depth = -2;
		bulidingCamera.depth = -1;
		menuCanvas.enabled = false;
		buildCanvas.enabled = true;
		gridMap.moveButton.enabled = false;
		gridMap.moveButton.image.enabled = false;
		if (GridMap.stageState == GridMap.StageState.tutorial)
		{
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.enabled = true;
			DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			DM.playerTalking = false;
			DM.lineNum = 0;
			string file = "Assets/Data/blacksmith";
			file += ".txt";
			DP.LoadDialogue(file);
			GridMap.stageState = GridMap.StageState.tutorial2;
		}
	}

	public void ChooseFarm()
	{
		if (SResource.Instance.GOLD >= 150 && SResource.Instance.FOOD >= 100)
		{
			SResource.Instance.GOLD -= 150;
			SResource.Instance.FOOD -= 100;
			GridMap.kindBuild = GridMap.KindBuild.farm;
			overObject = Instantiate(gridMap.overFarm);

			main.depth = -1;
			bulidingCamera.depth = -2;
			menuCanvas.enabled = true;
			buildCanvas.enabled = false;
		}
		else
		{
			text.enabled = true;
		}

	}

	public void ChooseWindmill()
	{
		GridMap.kindBuild = GridMap.KindBuild.windmill;
		overObject = Instantiate(gridMap.overWindmill);

		main.depth = -1;
		bulidingCamera.depth = -2;
		menuCanvas.enabled = true;
		buildCanvas.enabled = false;

	}

	public void ChooseBlacksmith()
	{
		if (SResource.Instance.GOLD >= 150 && SResource.Instance.IRON >= 100)
		{
			SResource.Instance.GOLD -= 150;
			SResource.Instance.IRON -= 100;
			GridMap.kindBuild = GridMap.KindBuild.blacksmith;
			overObject = Instantiate(gridMap.overBlacksmith);
			main.depth = -1;
			bulidingCamera.depth = -2;
			menuCanvas.enabled = true;
			buildCanvas.enabled = false;
		}
		else
		{
			text.enabled = true;
		}

	}

	public void ChooseMine()
	{
		if (SResource.Instance.GOLD >= 300 && SResource.Instance.IRON >= 200)
		{
			SResource.Instance.GOLD -= 300;
			SResource.Instance.IRON -= 200;

			GridMap.kindBuild = GridMap.KindBuild.mine;
			overObject = Instantiate(gridMap.overMine);
			main.depth = -1;
			bulidingCamera.depth = -2;
			menuCanvas.enabled = true;
			buildCanvas.enabled = false;
		}
		else
			text.enabled = true;

	}
}
