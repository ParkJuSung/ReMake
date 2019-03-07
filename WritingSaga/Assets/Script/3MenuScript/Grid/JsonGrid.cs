using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class JsonGrid : MonoBehaviour {

	//public static JsonGrid Instance = null;
	public GridInfo gridInfo;
	public GameObject[] farm;
	public GameObject[] windmill;
	public GameObject[] blacksmith;
	public GameObject[] mine;

	public Button save;
	public Button load;
	public Button reset;

	static public int NumberOfBlacksmith = 0;
	static public int NumberOfFarm = 0;
	static public int NumberOfMine = 0;
	//private static JsonGrid instance =null;

	public static JsonGrid Instance;
	/*{
		get
		{
			if (instance == null)
				instance = new JsonGrid();

			return instance;
		}
	}*/

	// Use this for initialization
	void Start () {

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

		for(int i =0;i<624;i++)
		{
			gridInfo.buildingList[i] = newMyClass.buildingList[i];
		}

		for (int i = 0; i < 624; i++)
			gridInfo.buildingList[i] = GridMap.KindBuild.none;

		save = GameObject.Find("OptionCanvas/save").GetComponent<Button>();
		load = GameObject.Find("OptionCanvas/load").GetComponent<Button>();
		reset = GameObject.Find("OptionCanvas/reset").GetComponent<Button>();

		save.onClick.AddListener(() =>
		{
			Save();
		});

		load.onClick.AddListener(() =>
		{
			Load();
		});

		reset.onClick.AddListener(() =>
		{
			Init();
		});
		if(SceneSwitch.stage>=4)
		 Load();
		else if(MainScreenLoad.isLoad)
		{
			Load();
			MainScreenLoad.isLoad = false;
		}


	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Save()
	{
		GridInfo gridMap = new GridInfo(24, gridInfo.buildingList,GridMap.stageState,SceneSwitch.stage);

		GridMap grid = GameObject.Find("Plane").GetComponent<GridMap>();

		grid.optionUI = false;
		grid.optionCanvas.enabled = false;

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
		Init();

		GridMap grid = GameObject.Find("Plane").GetComponent<GridMap>();
		//GridMap grid = GameObject.Find("Terrain").GetComponent<GridMap>();
		grid.optionUI = false;
		grid.optionCanvas.enabled = false;
		
		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");
		string jsonFormFile = File.ReadAllText(fliePath);

		GridInfo newMyClass = JsonUtility.FromJson<GridInfo>(jsonFormFile);
		//GridMap.stageState = newMyClass.stageInformation;
		//	GridMap.stageState = newMyClass.stageInformation;
		//	SceneSwitch.stage = newMyClass.battleStageInformation;

		for (int i=0;i<624;i++)
		{
			if (newMyClass.buildingList[i] == GridMap.KindBuild.none)
				continue;

			if(newMyClass.buildingList[i] == GridMap.KindBuild.farm)
			{
				farm[i] = Instantiate(Resources.Load("Farm", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(0, 0, 0.5f), grid.farm.transform.rotation) as GameObject;

				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(0, 0, 1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(1, 0, 1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) +GridMap. GetGridNumber(new Vector3(-1, 0, 1))] = true;

				gridInfo.buildingList[i] = GridMap.KindBuild.farm;
			}
			else if(newMyClass.buildingList[i] == GridMap.KindBuild.mine)
			{
				mine[i] = Instantiate(Resources.Load("Crystal", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(0, 0, -0.5f), grid.mine.transform.rotation) as GameObject;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				gridInfo.buildingList[i] = GridMap.KindBuild.mine;
			}
			else if(newMyClass.buildingList[i] == GridMap.KindBuild.blacksmith)
			{
				blacksmith[i] = Instantiate(Resources.Load("Blacksmith", typeof(GameObject)), GridMap.GetVector(i) + new Vector3(-0.5f, 0, 0.2f), grid.blacksmith.transform.rotation) as GameObject;

				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, 0))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(0, 0, -1))] = true;
				grid.wall[GridMap.GetGridNumber(GridMap.GetVector(i)) + GridMap.GetGridNumber(new Vector3(-1, 0, -1))] = true;
				gridInfo.buildingList[i] = GridMap.KindBuild.blacksmith;

			}
			else if(newMyClass.buildingList[i] == GridMap.KindBuild.windmill)
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

	public void Init()
	{
		//	GridInfo gridInfo = new GridInfo();
		//	string jsonToFile = JsonUtility.ToJson(gridInfo, true);
		//Debug.Log(jsonToFile);

		//	string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");

		//	File.WriteAllText(fliePath, jsonToFile);

			GridMap grid = GameObject.Find("Plane").GetComponent<GridMap>();
		//GridMap grid = GameObject.Find("Terrain").GetComponent<GridMap>();
		for (int i = 0; i < 624; i++)
		{
			switch (gridInfo.buildingList[i].ToString())
			{
				case "farm":
					Destroy(farm[i]);
					gridInfo.buildingList[i] = GridMap.KindBuild.none;
					break;
				case "windmill":
					Destroy(windmill[i]);
					gridInfo.buildingList[i] = GridMap.KindBuild.none;
					break;
				case "blacksmith":
					Destroy(blacksmith[i]);
					gridInfo.buildingList[i] = GridMap.KindBuild.none;
					break;
				case "mine":
					Destroy(mine[i]);
					gridInfo.buildingList[i] = GridMap.KindBuild.none;
					break;
			}
		}

		grid.optionUI = false;
		grid.optionCanvas.enabled = false;
	}

	static public void CountBlacksmith()
	{
		if (NumberOfBlacksmith >= 1)
			return;
		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");
		string jsonFormFile = File.ReadAllText(fliePath);
		GridInfo newMyClass = JsonUtility.FromJson<GridInfo>(jsonFormFile);
		for (int i = 0; i < 624; i++)
		{
			if(newMyClass.buildingList[i] == GridMap.KindBuild.blacksmith)
			{
				NumberOfBlacksmith++;
			}

			if(newMyClass.buildingList[i] == GridMap.KindBuild.farm)
			{
				NumberOfFarm++;
			}
			if (newMyClass.buildingList[i] == GridMap.KindBuild.farm)
			{
				NumberOfMine++;
			}
		}


		Debug.Log("대장간 갯수" + NumberOfBlacksmith);

	}
}
