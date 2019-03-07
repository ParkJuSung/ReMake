using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
//using UnityEditor;
using UnityEngine.SceneManagement;

public class GridMap : MonoBehaviour {

	public enum KindBuild
	{
		none,farm,windmill, blacksmith,mine
	}

	public enum StageState
	{
		normal,tutorial,tutorial2, FirstMatch, tutorial4,SecondMatch,ThirdMatch, RiderTutorial, FourthMatch,HeroTutorial,EndStage,Ending,InfinityMode
	}

	public GameObject farm;
	public GameObject overFarm;
	public GameObject windmill;
	public GameObject overWindmill;
	public GameObject blacksmith;
	public GameObject overBlacksmith;
	public GameObject mine;
	public GameObject overMine;
	public GameObject clickedObject;

	public static KindBuild kindBuild = KindBuild.none;
	public static StageState stageState;
	public CameraControl cameraControl;
	private static int N = 25;
	public bool[] wall = new bool[624];
	public Button moveButton;
	public Button roationButton;

	public  bool optionUI;
	public Canvas optionCanvas;

	// Use this for initialization
	void Start()
	{

		if (stageState == StageState.normal)
			stageState = StageState.tutorial;

		Debug.Log(stageState);
		for (int i = 0; i < 25; i++)
		{
			Debug.DrawLine(new Vector3(i, -2, 0), new Vector3(i, -2, N), new Color(255, 0, 0), Mathf.Infinity);
			Debug.DrawLine(new Vector3(0, -2, i), new Vector3(N, -2, i), new Color(255, 0, 0), Mathf.Infinity);
		}
		optionUI = false;
		cameraControl = GameObject.Find("Camera/Main Camera (1)").GetComponent<CameraControl>();

		kindBuild = KindBuild.none;
		if (stageState == StageState.RiderTutorial)
		{
			GameObject[] Buildings = new GameObject[3];
			Buildings[0] = GameObject.Find("BuildingCanvas/mine");
			Buildings[1] = GameObject.Find("BuildingCanvas/windmill");
			Buildings[2] = GameObject.Find("BuildingCanvas/blacksmith");
			for (int i = 0; i < Buildings.Length; i++)
				Buildings[i].SetActive(false);
		}
		else if (stageState == StageState.FirstMatch || stageState == StageState.tutorial4
		  || stageState == StageState.SecondMatch || stageState ==StageState.ThirdMatch)
		{
			GameObject[] Buildings = new GameObject[3];
			Buildings[0] = GameObject.Find("BuildingCanvas/mine");
			Buildings[1] = GameObject.Find("BuildingCanvas/windmill");
			Buildings[2] = GameObject.Find("BuildingCanvas/Farm");
			for (int i = 0; i < Buildings.Length; i++)
				Buildings[i].SetActive(false);
		}
		else if(stageState == StageState.HeroTutorial)
		{
			GameObject[] Buildings = new GameObject[3];
			Buildings[0] = GameObject.Find("BuildingCanvas/blacksmith");
			Buildings[1] = GameObject.Find("BuildingCanvas/windmill");
			Buildings[2] = GameObject.Find("BuildingCanvas/Farm");
			for (int i = 0; i < Buildings.Length; i++)
				Buildings[i].SetActive(false);
		}
		Debug.Log(stageState);
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!optionUI)
			{
				optionCanvas.enabled = true;
				//optionCanvas.gameObject.SetActive(true);
				optionUI = true;
			}
			else
			{
				optionCanvas.enabled = false;
				//optionCanvas.gameObject.SetActive(false);
				optionUI = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			if (SceneManager.GetActiveScene().buildIndex == 7)
			{
				if(stageState != StageState.FirstMatch && stageState != StageState.SecondMatch)
					stageState = stageState + 1;
				if (SceneSwitch.stage == 6)
					SBattleManager.Instance.CurrentMonster = 500;

					for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
					Destroy(SBattleManager.Instance.EnemyList[i].gameObject);
				for (int i = 0; i < SBattleManager.Instance.UnitList.Count; i++)
					Destroy(SBattleManager.Instance.UnitList[i].gameObject);
				for (int i = 0; i < SBattleManager.Instance.HeroList.Count; i++)
					Destroy(SBattleManager.Instance.HeroList[i].gameObject);

				SBattleManager.Instance.UnitList.Clear();
				SBattleManager.Instance.EnemyList.Clear();
				SBattleManager.Instance.HeroList.Clear();
				SBattleManager.Instance.CurrentWave = 0;
				if (SceneSwitch.stage != 6)
					SBattleManager.Instance.Win = true;
				//SBattleManager.Instance.CurrentMonster = 0;

			}
		}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			SResource.Instance.GOLD = 500;
			SResource.Instance.IRON = 500;
			SResource.Instance.FOOD = 500;
		}

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Ray ray = Camera.main.ViewportPointToRay(Input.mousePosition);

			RaycastHit hit;
			int mask = 1 << 8;
			if (Physics.Raycast(ray, out hit, 100, mask))
			{
				int tempNumber = GetGridNumber(hit.point);
				switch (kindBuild)
				{
					case KindBuild.windmill:
						if (!(GetGridNumber(hit.point) % N == N - 1))
							if (!(GetGridNumber(hit.point) / N == N - 1))
							{
								if (!wall[GetGridNumber(hit.point)] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))]
									&& !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))])
								{
									JsonGrid.Instance.windmill[tempNumber] = Instantiate(windmill, GetVector(GetGridNumber(hit.point)) + new Vector3(0.5f, 0, 0.5f), windmill.transform.rotation);
									JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.windmill;
									wall[GetGridNumber(hit.point)] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] = true;
									Destroy(cameraControl.overObject.gameObject);
									kindBuild = KindBuild.none;
									//JsonGrid.Instance.Save();
								}
							}
						break;
					case KindBuild.farm:
						if (!(GetGridNumber(hit.point) % N == 0))
							if (!(GetGridNumber(hit.point) % N == N - 1))
								if (!(GetGridNumber(hit.point) / N == N - 1))
									if (!wall[GetGridNumber(hit.point)] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] &&
										!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] &&
										!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] &&
										!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] &&
										!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))] &&
										!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 1))] &&
										!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, -1))])
									{
										//Debug.Log(!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-2, 0, 1))]);
										if (SceneSwitch.stage <= 7)
										{
											Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
											DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
											DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
											JsonGrid.Instance.farm[tempNumber] = Instantiate(farm, GetVector(GetGridNumber(hit.point)) + new Vector3(0, 0, 0.5f), farm.transform.rotation);
											wall[GetGridNumber(hit.point)] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 1))] = true;
											Destroy(cameraControl.overObject.gameObject);
											kindBuild = KindBuild.none;
											JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.farm;
											//JsonGrid.Instance.Save();
											if (stageState == StageState.RiderTutorial)
											{
												temp.enabled = true;
												DM.playerTalking = false;
												DM.lineNum = 0;
												string file = "Assets/Data/StartBattle";
												file += ".txt";
												//stageState = StageState.tutorial3;
												/*StopCoroutine(DM.ChangeColor(DM.arrow2));
												DM.arrow2.GetComponent<Image>().enabled = false;
												DM.arrow2 = GameObject.Find("DialogueCanvas/arrow2");
												DM.arrow2.GetComponent<Image>().enabled = true;
												StartCoroutine(DM.ChangeColor(DM.arrow2));*/
												DP.LoadDialogue(file);
											}
										}
										else if(SceneSwitch.stage >7)
										{
											JsonGrid.Instance.farm[tempNumber] = Instantiate(farm, GetVector(GetGridNumber(hit.point)) + new Vector3(0, 0, 0.5f), farm.transform.rotation);
											wall[GetGridNumber(hit.point)] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] = true;
											wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 1))] = true;
											Destroy(cameraControl.overObject.gameObject);
											kindBuild = KindBuild.none;
											JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.farm;
										}
										JsonGrid.NumberOfFarm++;
									}
						break;
					case KindBuild.blacksmith:
						if (!(GetGridNumber(hit.point) % N == 0))
							if (!(GetGridNumber(hit.point) / N == 0))
								if (!wall[GetGridNumber(hit.point)] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))]
									&& !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))]
									&& !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, -1))])
								{
									JsonGrid.Instance.blacksmith[tempNumber] = Instantiate(blacksmith, GetVector(GetGridNumber(hit.point)) + new Vector3(-0.5f, 0, 0.2f), blacksmith.transform.rotation);
									JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.blacksmith;
									JsonGrid.NumberOfBlacksmith++;
									//wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))] = true;
									//wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
									//wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, -1))] = true;
									//if (!(GetGridNumber(hit.point) / N == N-1))
									//{
									wall[GetGridNumber(hit.point)] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, -1))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))] = true;
									//}
									Destroy(cameraControl.overObject.gameObject);
									kindBuild = KindBuild.none;
									Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
									DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
									DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();

									if (stageState == StageState.tutorial2)
									{
										temp.enabled = true;
										;
										DM.playerTalking = false;
										DM.lineNum = 0;
										string file = "Assets/Data/NextBattle";
										file += ".txt";
										stageState = StageState.FirstMatch;
										StopCoroutine(DM.ChangeColor(DM.arrow2));
										DM.arrow2.GetComponent<Image>().enabled = false;
										DM.arrow2 = GameObject.Find("DialogueCanvas/arrow2");
										DM.arrow2.GetComponent<Image>().enabled = true;
										StartCoroutine(DM.ChangeColor(DM.arrow2));
										DP.LoadDialogue(file);
									}
									else if (stageState == StageState.tutorial4)
									{
										//Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
										temp.enabled = true;
										//DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
										//DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
										DM.playerTalking = false;
										DM.lineNum = 0;
										string file = "Assets/Data/StartBattle";
										file += ".txt";
										stageState = StageState.SecondMatch;
										DP.LoadDialogue(file);
									}
									else if (stageState == StageState.ThirdMatch)
									{
										//Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
										temp.enabled = true;
										//DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
										//DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
										DM.playerTalking = false;
										DM.lineNum = 0;
										string file = "Assets/Data/StartBattle";
										file += ".txt";
										//stageState = StageState.tutorial5;
										DP.LoadDialogue(file);
									}
									/*else if(stageState == StageState.RiderTutorial)
									{
										temp.enabled = true;
										DM.playerTalking = false;
										DM.lineNum = 0;
										string file = "Assets/Data/Dialogue12";
										file += ".txt";
										//stageState = StageState.tutorial5;
										DP.LoadDialogue(file);
									}*/
									Debug.Log("대장간 갯수 2 : " + JsonGrid.NumberOfBlacksmith);
									//JsonGrid.Instance.Save();
								}

						break;
					case KindBuild.mine:
						if (!wall[GetGridNumber(hit.point)])
						{
							JsonGrid.Instance.mine[tempNumber] = Instantiate(mine, GetVector(GetGridNumber(hit.point)) + new Vector3(0, 0, -0.5f), mine.transform.rotation);
							JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.mine;
							wall[GetGridNumber(hit.point)] = true;
							Destroy(cameraControl.overObject.gameObject);
							kindBuild = KindBuild.none;
							JsonGrid.NumberOfMine++;
							Debug.Log("광산 갯수: " + JsonGrid.NumberOfMine);

							//JsonGrid.Instance.Save();
						}
						break;
					default:
						break;
				}
			}
		}
		if (SBattleManager.Instance.Win)
		{

			//string sceneNum = EditorApplication.currentScene;
		//	string sceneNum = SceneManager.GetActiveScene().buildIndex.ToString();

	//		sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
			//if (sceneNum == "3" && stageState == StageState.FirstMatch)
			//{
			//	SBattleManager.Instance.EnemyList.Clear();
			//	Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			//	temp.enabled = true;
			//	DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			//	DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			//	DM.playerTalking = false;
			//	DM.lineNum = 0;
			//	string file = "Assets/Data/Dialogue8";
			//	file += ".txt";
			//	GridMap.stageState = GridMap.StageState.tutorial4;
			//	StopCoroutine(DM.ChangeColor(DM.arrow2));
			//	DP.LoadDialogue(file);
			//	SBattleManager.Instance.Win = false;
			//	for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			//		Destroy(SBattleManager.Instance.EnemyList[i]);

			//	SBattleManager.Instance.EnemyList.Clear();
			//}
			//else if(sceneNum == "3" && stageState == StageState.SecondMatch)
			//{
			//	SBattleManager.Instance.EnemyList.Clear();
			//	Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			//	temp.enabled = true;
			//	DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			//	DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			//	DM.playerTalking = false;
			//	DM.lineNum = 0;
			//	string file = "Assets/Data/Dialogue10";
			//	file += ".txt";
			//	GridMap.stageState = GridMap.StageState.ThirdMatch;
			//	DP.LoadDialogue(file);
			//	SBattleManager.Instance.Win = false;
			//	for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			//		Destroy(SBattleManager.Instance.EnemyList[i]);

			//	SBattleManager.Instance.EnemyList.Clear();
			//}
			//else if (sceneNum == "3" && stageState == StageState.ThirdMatch)
			//{
			//	SBattleManager.Instance.EnemyList.Clear();
			//	Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			//	temp.enabled = true;
			//	DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			//	DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			//	DM.playerTalking = false;
			//	DM.lineNum = 0;
			//	string file = "Assets/Data/Dialogue10";
			//	file += ".txt";
			//	GridMap.stageState = GridMap.StageState.RiderTutorial;
			//	DP.LoadDialogue(file);
			//	SBattleManager.Instance.Win = false;
			//	for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			//		Destroy(SBattleManager.Instance.EnemyList[i]);

			//	SBattleManager.Instance.EnemyList.Clear();
			//}
			//else if(sceneNum == "3" && stageState == StageState.RiderTutorial)
			//{
			//	SBattleManager.Instance.EnemyList.Clear();
			//	Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			//	temp.enabled = true;
			//	DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			//	DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			//	DM.playerTalking = false;
			//	DM.lineNum = 0;
			//	string file = "Assets/Data/Dialogue13";
			//	file += ".txt";
			//	GridMap.stageState = GridMap.StageState.FourthMatch;
			//	DP.LoadDialogue(file);
			//	SBattleManager.Instance.Win = false;
			//	for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			//		Destroy(SBattleManager.Instance.EnemyList[i]);

			//	SBattleManager.Instance.EnemyList.Clear();
			//}
			//else if(sceneNum == "3" && stageState == StageState.HeroTutorial)
			//{
			//	SBattleManager.Instance.EnemyList.Clear();
			//	Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			//	temp.enabled = true;
			//	DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			//	DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			//	DM.playerTalking = false;
			//	DM.lineNum = 0;
			//	string file = "Assets/Data/CreateHero";
			//	file += ".txt";
			//	GridMap.stageState = GridMap.StageState.HeroTutorial;
			//	DP.LoadDialogue(file);
			//	SBattleManager.Instance.Win = false;
			//	for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			//		Destroy(SBattleManager.Instance.EnemyList[i]);

			//	SBattleManager.Instance.EnemyList.Clear();
			//}
		}
	}

	void OnMouseDown()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Ray ray = Camera.main.ViewportPointToRay(Input.mousePosition);

		RaycastHit hit;
		int mask = 1 << 8;
		if (Physics.Raycast(ray, out hit, 100,mask))
		{
			int tempNumber = GetGridNumber(hit.point);
			switch (kindBuild)
			{
				case KindBuild.windmill:
					if (!(GetGridNumber(hit.point) % N == N - 1))
						if (!(GetGridNumber(hit.point) / N == N - 1))
						{
							if (!wall[GetGridNumber(hit.point)] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))]
								&& !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))])
							{
								JsonGrid.Instance.windmill[tempNumber] = Instantiate(windmill, GetVector(GetGridNumber(hit.point)) + new Vector3(0.5f, 0, 0.5f), windmill.transform.rotation);
								JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.windmill;
								wall[GetGridNumber(hit.point)] = true;
								wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] = true;
								wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] = true;
								wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] = true;
								Destroy(cameraControl.overObject.gameObject);
								kindBuild = KindBuild.none;
								//JsonGrid.Instance.Save();
							}
						}
					break;
				case KindBuild.farm:
					if (!(GetGridNumber(hit.point) % N == 0))
						if (!(GetGridNumber(hit.point) % N == N - 1))
							if (!(GetGridNumber(hit.point) / N == N - 1))
								if (!wall[GetGridNumber(hit.point)] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] &&
									!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] &&
									!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] &&
									!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] &&
									!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))] &&
									!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 1))] &&
									!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, -1))])
								{
									//Debug.Log(!wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-2, 0, 1))]);
									Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
									DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
									DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
									JsonGrid.Instance.farm[tempNumber] = Instantiate(farm, GetVector(GetGridNumber(hit.point)) + new Vector3(0, 0, 0.5f), farm.transform.rotation);
									wall[GetGridNumber(hit.point)] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 0))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, 1))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(1, 0, 1))] = true;
									wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 1))] = true;
									Destroy(cameraControl.overObject.gameObject);
									kindBuild = KindBuild.none;
									JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.farm;
									//JsonGrid.Instance.Save();
									if (stageState == StageState.RiderTutorial)
									{
										temp.enabled = true;
										DM.playerTalking = false;
										DM.lineNum = 0;
										string file = "Assets/Data/StartBattle";
										file += ".txt";
										//stageState = StageState.tutorial3;
										/*StopCoroutine(DM.ChangeColor(DM.arrow2));
										DM.arrow2.GetComponent<Image>().enabled = false;
										DM.arrow2 = GameObject.Find("DialogueCanvas/arrow2");
										DM.arrow2.GetComponent<Image>().enabled = true;
										StartCoroutine(DM.ChangeColor(DM.arrow2));*/
										DP.LoadDialogue(file);
									}
									JsonGrid.NumberOfFarm++;
								}
					break;
				case KindBuild.blacksmith:
					if (!(GetGridNumber(hit.point) % N == 0))
						if (!(GetGridNumber(hit.point) / N == 0))
							if (!wall[GetGridNumber(hit.point)] && !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))]
								&& !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))]
								&& !wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, -1))])
							{
								JsonGrid.Instance.blacksmith[tempNumber] = Instantiate(blacksmith, GetVector(GetGridNumber(hit.point)) + new Vector3(-0.5f, 0, 0.2f), blacksmith.transform.rotation);
								JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.blacksmith;
								//wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))] = true;
								//wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
								//wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, -1))] = true;
								//if (!(GetGridNumber(hit.point) / N == N-1))
								//{
								wall[GetGridNumber(hit.point)] = true;
								wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, 0))] = true;
								wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(0, 0, -1))] = true;
								wall[GetGridNumber(hit.point) + GetGridNumber(new Vector3(-1, 0, -1))] = true;
								//}
								Destroy(cameraControl.overObject.gameObject);
								kindBuild = KindBuild.none;
								Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
								DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
								DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();

								if (stageState == StageState.tutorial2)
								{
									temp.enabled = true;
									;
									DM.playerTalking = false;
									DM.lineNum = 0;
									string file = "Assets/Data/NextBattle";
									file += ".txt";
									stageState = StageState.FirstMatch;
									StopCoroutine(DM.ChangeColor(DM.arrow2));
									DM.arrow2.GetComponent<Image>().enabled = false;
									DM.arrow2 = GameObject.Find("DialogueCanvas/arrow2");
									DM.arrow2.GetComponent<Image>().enabled = true;
									StartCoroutine(DM.ChangeColor(DM.arrow2));
									DP.LoadDialogue(file);
								}
								else if (stageState == StageState.tutorial4)
								{
									//Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
									temp.enabled = true;
									//DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
									//DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
									DM.playerTalking = false;
									DM.lineNum = 0;
									string file = "Assets/Data/StartBattle";
									file += ".txt";
									stageState = StageState.SecondMatch;
									DP.LoadDialogue(file);
								}
								else if (stageState == StageState.ThirdMatch)
								{
									//Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
									temp.enabled = true;
									//DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
									//DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
									DM.playerTalking = false;
									DM.lineNum = 0;
									string file = "Assets/Data/StartBattle";
									file += ".txt";
									//stageState = StageState.tutorial5;
									DP.LoadDialogue(file);
								}
								/*else if(stageState == StageState.RiderTutorial)
								{
									temp.enabled = true;
									DM.playerTalking = false;
									DM.lineNum = 0;
									string file = "Assets/Data/Dialogue12";
									file += ".txt";
									//stageState = StageState.tutorial5;
									DP.LoadDialogue(file);
								}*/
								JsonGrid.NumberOfBlacksmith++;
								Debug.Log("대장간 갯수 2 : " + JsonGrid.NumberOfBlacksmith);
								//JsonGrid.Instance.Save();
							}

					break;
				case KindBuild.mine:
					if (!wall[GetGridNumber(hit.point)])
					{
						JsonGrid.Instance.mine[tempNumber] = Instantiate(mine, GetVector(GetGridNumber(hit.point)) + new Vector3(0, 0, -0.5f), mine.transform.rotation);
						JsonGrid.Instance.gridInfo.buildingList[tempNumber] = KindBuild.mine;
						wall[GetGridNumber(hit.point)] = true;
						Destroy(cameraControl.overObject.gameObject);
						kindBuild = KindBuild.none;
						JsonGrid.NumberOfMine++;
						Debug.Log("광산 갯수  : " + JsonGrid.NumberOfMine);

						//JsonGrid.Instance.Save();
					}
					break;
				default:
					break;
			}


			/*int[] neighobrNumber =  GetNeighborNumber(hit.point);
            Debug.Log("찍은 곳" + GetGridNumber(hit.point));
            foreach (int c in neighobrNumber)
            {
                Debug.Log(c);
            }*/
			//	FindInGrid(GetGridNumber(hit.point), 0);

			}
		}

	public static int GetGridNumber(Vector3 point)
	{
		return (int)point.x + (int)point.z * N;
	}

	public static Vector3 GetVector(int GridNumber)
	{
		return new Vector3(GridNumber % N + 0.5f, -2.5f + 0.5f, GridNumber / N + 0.5f);
	}

	static int[] GetNeighborNumber(Vector3 point)
	{
		int tempGridNumber = GetGridNumber(point);
		List<int> neighbor = new List<int>() { N, N + 1, 1, -N + 1, -N, -N - 1, -1, N - 1 };

		/*if(tempGridNumber % N ==0 && tempGridNumber / N ==0)
            neighbor.RemoveAll(c => c == -1 || c == N - 1 || c == -N - 1 || c == -N || c == -N + 1); // 왼쪽 끝아래
        else if (tempGridNumber % N == N - 1 && tempGridNumber / N == 0)
            neighbor.RemoveAll(c => c == 1 || c == -N + 1 || c == N + 1 || c == -N || c == -N - 1); // 오른쪽 끝아래
        else if (tempGridNumber % N == 0 && tempGridNumber / N == N-1)
            neighbor.RemoveAll(c => c == -1 || c == N - 1 || c == N || c == N + 1 || c == -N - 1); //왼쪽 끝위
        else if(tempGridNumber %  N == N-1 && tempGridNumber / N == N-1)
            neighbor.RemoveAll(c => c == 1 || c == N - 1 || c == N || c == N + 1 || c == -N + 1); //오른쪽 끝위*/

		if (tempGridNumber % N == 0)
			neighbor.RemoveAll(c => c == -1 || c == N - 1 || c == -N - 1); //왼쪽
		if (tempGridNumber / N == 0)
			neighbor.RemoveAll(c => c == -N - 1 || c == -N || c == -N + 1); // 맨아래
		if (tempGridNumber % N == N - 1)
			neighbor.RemoveAll(c => c == 1 || c == N + 1 || c == -N + 1); // 오른쪽
		if (tempGridNumber / N == N - 1)
			neighbor.RemoveAll(c => c == N - 1 || c == N || c == N + 1); // 맨위

		for (int i = 0; i < neighbor.Count; i++)
			neighbor[i] = tempGridNumber + neighbor[i];

		return neighbor.ToArray();
	}

	public void OptionButton()
	{
		if (!optionUI)
		{
			optionCanvas.enabled = true;
			optionUI = true;
		}
		else
		{
			optionCanvas.enabled = false;
			optionUI = false;
		}
	}
	/*static bool FindInGrid(int sourceGrid, int destinationGrid)
	{
		Queue<int> grids = new Queue<int>();
		List<int> visited = new List<int>();
		int current = -1;
		grids.Enqueue(sourceGrid);
		Debug.Log("찍은 곳" + sourceGrid);

		while (grids.Count > 0)
		{
			current = grids.Dequeue();
			int[] neighobrNumber = GetNeighborNumber(GetVector(current));
			foreach (var c in neighobrNumber)
			{
				if (visited.Contains(c) || wall[c])
					continue;

				visited.Add(c);
				grids.Enqueue(c);

				Debug.Log(c);
				if (c == destinationGrid)
				{
					Debug.Log("찾음");
					return true;
				}


			}
		}
		Debug.Log("못찾음");
		return false;
	}*/


}
