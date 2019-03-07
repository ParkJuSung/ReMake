using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
using System.IO;

public class SceneSwitch : MonoBehaviour {
	public static int stage=3;

	public void FirstMoveMenu()
	{
		SceneManager.LoadScene("3MenuScene"); //MenuScene 이동
		JsonGrid.NumberOfBlacksmith = 0;
		SResource.Instance.GOLD = 150;
		SResource.Instance.IRON = 100;


		//	SoundManager.instance.musicSource.clip = Resources.Load("Sound/With love from Vertex Studio (7)", typeof(AudioClip)) as AudioClip;
		//	SoundManager.instance.musicSource.loop = true;
		//	SoundManager.instance.efxSource.loop = true;

		//	SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}
	public void MenuScene()
    {
		SResource.Instance.isIncomeGet = false;
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Dungeon5", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
		SceneManager.LoadScene("3MenuScene"); //MenuScene 이동
		SBattleManager.Instance.UnitList.Clear();
		SBattleManager.Instance.HeroList.Clear();
		//SoundManager.instance.musicSource.clip = Resources.Load("Sound/With love from Vertex Studio (7)", typeof(AudioClip)) as AudioClip;
		//SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);

	}

    public void BattleScene()
    {
		if (stage == 1)
		{
			SceneManager.LoadScene("7BattleScene"); // BattleScene 이동
		}
		else if(stage ==2)
		{
			SceneManager.LoadScene("7BattleScene-2");
		}
		//SoundManager.instance.musicSource.clip = Resources.Load("Sound/00_TER_16_009", typeof(AudioClip)) as AudioClip;
		//SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}

	public void BattleScene2()
	{
		SceneManager.LoadScene("7BattleScene-2"); // BattleScene 이동
	}
	
	public void BattleScene3()
	{
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Battle4", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
		SceneManager.LoadScene("7BattleScene-3");
	}

	public void HeroScene()
    {
        SceneManager.LoadScene("4HeroScene"); // HeroScene 이동
    }

    public void ShopScene()
    {
        SceneManager.LoadScene("4ShopScene"); // ShopScene 이동
    }

    public void WorldScene()
    {
        SceneManager.LoadScene("4WorldScene"); //WorldScene 이동
    }

    public void ConQuestScene()
    {
        SceneManager.LoadScene("5ConquestScene"); //Conquest 이동
    }

    public void SetScene()
    {
		JsonGrid.Instance.Save();
		SceneManager.LoadScene("6SetScene");
		if(GridMap.stageState == GridMap.StageState.RiderTutorial)
			GridMap.stageState = GridMap.StageState.FourthMatch;

		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Dungeon1", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}

	public void Surrender()
	{
		for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			//Debug.Log(SBattleManager.Instance.EnemyList[i].gameObject.name);
			Destroy(SBattleManager.Instance.EnemyList[i].gameObject);
		for(int i=0;i<SBattleManager.Instance.UnitList.Count;i++)
			Destroy(SBattleManager.Instance.UnitList[i].gameObject);
		for(int i=0;i<SBattleManager.Instance.HeroList.Count;i++)
			Destroy(SBattleManager.Instance.HeroList[i].gameObject);

		SBattleManager.Instance.EnemyList.Clear();
		SBattleManager.Instance.HeroList.Clear();
		SBattleManager.Instance.UnitList.Clear();
		SceneManager.LoadScene("3MenuScene"); //MenuScene 이동
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Battle4", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void StartLoad()
	{
		MainScreenLoad.isLoad = false;
		SBattleManager.Instance.Win = false;
		SBattleManager.Instance.CurrentMonster = 0;
		SBattleManager.Instance.GoblinCount = 0;
		SBattleManager.Instance.SkeletonCount = 0;
		SBattleManager.Instance.CurrentWave = 0;
		SBattleManager.Instance.MaxMonster = 0;
		SBattleManager.Instance.WizardCount = 0;
		SBattleManager.Instance.EnemyList.Clear();
		SBattleManager.Instance.UnitList.Clear();
		SBattleManager.Instance.HeroList.Clear();
		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");
		string jsonFormFile = File.ReadAllText(fliePath);
		GridInfo newMyClass = JsonUtility.FromJson<GridInfo>(jsonFormFile);

		SceneSwitch.stage = newMyClass.battleStageInformation;
		GridMap.stageState = newMyClass.stageInformation;
		Debug.Log("대장간 갯수 :" + JsonGrid.NumberOfBlacksmith);
		Debug.Log("농장 갯수" + JsonGrid.NumberOfFarm);
		Debug.Log("광산 갯수" + JsonGrid.NumberOfMine);
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Dungeon5", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		JsonGrid.CountBlacksmith();
		SResource.Instance.GOLD = newMyClass.gold;
		SResource.Instance.FOOD = newMyClass.food;
		SResource.Instance.IRON = newMyClass.Iron;
		MainScreenLoad.isLoad = true;
		if (GridMap.stageState == GridMap.StageState.Ending)
			GridMap.stageState = GridMap.StageState.InfinityMode;
		SceneManager.LoadScene("3MenuScene");

		//JsonGrid.Instance.Load();
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}

	public void EndingScene()
	{
		SceneManager.LoadScene("8EndingScreen");
		GridMap.stageState = GridMap.StageState.Ending;
	}

	public void StartScene()
	{
		SceneManager.LoadScene("1StartScene");
	}
}
