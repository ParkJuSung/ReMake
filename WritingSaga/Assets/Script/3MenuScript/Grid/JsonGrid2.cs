using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class JsonGrid2 : MonoBehaviour {

	//public static JsonGrid Instance = null;
	public GridInfo gridInfo;
	public GameObject[] farm;
	public GameObject[] windmill;
	public GameObject[] blacksmith;
	public GameObject[] mine;

	//private static JsonGrid instance =null;

	public static JsonGrid2 Instance;
	/*{
		get
		{
			if (instance == null)
				instance = new JsonGrid();

			return instance;
		}
	}*/

	// Use this for initialization
	void Start()
	{

		if (Instance == null)
		{
			//DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		//else if (Instance != this)
		//	Destroy(gameObject);

		farm = new GameObject[624];
		windmill = new GameObject[624];
		blacksmith = new GameObject[624];
		mine = new GameObject[624];

		gridInfo = new GridInfo();
		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");
		string jsonFormFile = File.ReadAllText(fliePath);
		GridInfo newMyClass = JsonUtility.FromJson<GridInfo>(jsonFormFile);

		for (int i = 0; i < 624; i++)
		{
			gridInfo.buildingList[i] = newMyClass.buildingList[i];
		}

		for (int i = 0; i < 624; i++)
			gridInfo.buildingList[i] = GridMap.KindBuild.none;

		Load();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Save()
	{
		GridInfo gridMap = new GridInfo(24, gridInfo.buildingList, GridMap.stageState, SceneSwitch.stage);

		GridMap grid = GameObject.Find("Plane").GetComponent<GridMap>();

//		grid.optionUI = false;
//		grid.optionCanvas.enabled = false;

		string jsonToFile = JsonUtility.ToJson(gridMap, true);
		Debug.Log(jsonToFile);

		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");

		File.WriteAllText(fliePath, jsonToFile);

		/*gridMap = new GridInfo(10, gridInfo.buildingList);

		jsonToFile = JsonUtility.ToJson(gridMap, true);
		Debug.Log(jsonToFile);

		fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");

		File.WriteAllText(fliePath, jsonToFile);*/
	}


	public void Load()
	{
		GridMap grid = GameObject.Find("Plane").GetComponent<GridMap>();



		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");
		string jsonFormFile = File.ReadAllText(fliePath);

		GridInfo newMyClass = JsonUtility.FromJson<GridInfo>(jsonFormFile);

		for (int i = 0; i < 624; i++)
		{
			if (newMyClass.buildingList[i] == GridMap.KindBuild.none)
				continue;

			if (newMyClass.buildingList[i] == GridMap.KindBuild.farm)
			{
				farm[i] = Instantiate(Resources.Load("Farm", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(0, 0, 0.5f), grid.farm.transform.rotation) as GameObject;

				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(0, 0, 1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(1, 0, 1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, 1))] = true;

				gridInfo.buildingList[i] = GridMap.KindBuild.farm;
			}
			else if (newMyClass.buildingList[i] == GridMap.KindBuild.mine)
			{
				mine[i] = Instantiate(Resources.Load("Crystal", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(0, 0, -0.5f), grid.mine.transform.rotation) as GameObject;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				gridInfo.buildingList[i] = GridMap.KindBuild.mine;
			}
			else if (newMyClass.buildingList[i] == GridMap.KindBuild.blacksmith)
			{
				blacksmith[i] = Instantiate(Resources.Load("Blacksmith", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(-0.5f, 0, 0.2f), grid.blacksmith.transform.rotation) as GameObject;

				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(0, 0, -1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, -1))] = true;
				gridInfo.buildingList[i] = GridMap.KindBuild.blacksmith;
			}
			else if (newMyClass.buildingList[i] == GridMap.KindBuild.windmill)
			{
				windmill[i] = Instantiate(Resources.Load("Windmill", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(0.5f, 0, 0.5f), grid.windmill.transform.rotation) as GameObject;

				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(0, 0, 1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(1, 0, 1))] = true;
				gridInfo.buildingList[i] = GridMap.KindBuild.windmill;
			}
		}
	}
}
