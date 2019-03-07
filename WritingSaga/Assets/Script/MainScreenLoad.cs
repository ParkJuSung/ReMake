using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainScreenLoad : MonoBehaviour {

	public Slider backgorundMusic;
	public Button start;
	public Button load;
	public Button option;
	public Button exit;
	public Button optionExit;
	public static bool isLoad = false;
	// Use this for initialization
	void Start () {
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Dungeon5", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void StartLoad()
	{
		SceneManager.LoadScene("3MenuScene");
		string fliePath = Path.Combine(Application.dataPath, "Resources/JsonMap.json");
		string jsonFormFile = File.ReadAllText(fliePath);
		GridInfo newMyClass = JsonUtility.FromJson<GridInfo>(jsonFormFile);

		SceneSwitch.stage = newMyClass.battleStageInformation;
		GridMap.stageState = newMyClass.stageInformation;
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Dungeon5", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		JsonGrid.CountBlacksmith();
		SResource.Instance.GOLD = newMyClass.gold;
		SResource.Instance.FOOD = newMyClass.food;
		SResource.Instance.IRON = newMyClass.Iron;
		isLoad = true;
		//JsonGrid.Instance.Load();
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}

	public void Option()
	{
		start.gameObject.SetActive(false);
		load.gameObject.SetActive(false);
		option.gameObject.SetActive(false);
		exit.gameObject.SetActive(false);
		backgorundMusic.gameObject.SetActive(true);
		optionExit.gameObject.SetActive(true);
	}

	public void OptionExit()
	{
		start.gameObject.SetActive(true);
		load.gameObject.SetActive(true);
		option.gameObject.SetActive(true);
		exit.gameObject.SetActive(true);
		backgorundMusic.gameObject.SetActive(false);
		optionExit.gameObject.SetActive(false);
	}

	public void OnValueChanged()
	{
		SoundManager.instance.efxSource.volume = backgorundMusic.value;
	}
	public void Exit()
	{
		Application.Quit();
	}
}
