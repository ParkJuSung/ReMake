using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
//using UnityEditor;
using UnityEngine.SceneManagement;
public class DialogueParser : MonoBehaviour {

	
	struct DialogueLine
	{
		public string name;
		public string content;
		public int pose;
		public string position;
		public string[] options;

		public DialogueLine(string Name, string Content, int Pose, string Position)
		{
			name = Name;
			content = Content;
			pose = Pose;
			position = Position;
			options = new string[0];
		}
	}

	 List<DialogueLine> lines;

	// Use this for initialization
	void Start () {
		string file;

		//string sceneNum = EditorApplication.currentScene;
		string sceneNum = SceneManager.GetActiveScene().buildIndex.ToString();

		if (sceneNum == "3" && GridMap.stageState == GridMap.StageState.FirstMatch)
		{
			SBattleManager.Instance.EnemyList.Clear();
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.enabled = true;
			DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			DM.playerTalking = false;
			DM.lineNum = 0;
			file = "Assets/Data/Dialogue8";
		//	file += ".txt";
			GridMap.stageState = GridMap.StageState.tutorial4;
			StopCoroutine(DM.ChangeColor(DM.arrow2));
		//	DP.LoadDialogue(file);
			SBattleManager.Instance.Win = false;
			for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
				Destroy(SBattleManager.Instance.EnemyList[i]);

			SBattleManager.Instance.EnemyList.Clear();
		}
		else if (sceneNum == "3" && GridMap.stageState == GridMap.StageState.SecondMatch)
		{
			SBattleManager.Instance.EnemyList.Clear();
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.enabled = true;
			DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			DM.playerTalking = false;
			DM.lineNum = 0;
			file = "Assets/Data/Dialogue10";
		//	file += ".txt";
			GridMap.stageState = GridMap.StageState.ThirdMatch;
		//	DP.LoadDialogue(file);
			SBattleManager.Instance.Win = false;
			for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
				Destroy(SBattleManager.Instance.EnemyList[i]);

			SBattleManager.Instance.EnemyList.Clear();
		}
		else if (sceneNum == "3" && GridMap.stageState == GridMap.StageState.ThirdMatch)
		{
			SBattleManager.Instance.EnemyList.Clear();
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.enabled = true;
			DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			DM.playerTalking = false;
			DM.lineNum = 0;
			file = "Assets/Data/Dialogue10";
		//	file += ".txt";
			GridMap.stageState = GridMap.StageState.RiderTutorial;
		//	DP.LoadDialogue(file);
			SBattleManager.Instance.Win = false;
			for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
				Destroy(SBattleManager.Instance.EnemyList[i]);

			SBattleManager.Instance.EnemyList.Clear();
		}
		else if (sceneNum == "3" && GridMap.stageState == GridMap.StageState.RiderTutorial)
		{
			SBattleManager.Instance.EnemyList.Clear();
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.enabled = true;
			DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			DM.playerTalking = false;
			DM.lineNum = 0;
			file = "Assets/Data/Dialogue13";
		//	file += ".txt";
		//	GridMap.stageState = GridMap.StageState.FourthMatch;
		//	DP.LoadDialogue(file);
			SBattleManager.Instance.Win = false;
			for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
				Destroy(SBattleManager.Instance.EnemyList[i]);

			SBattleManager.Instance.EnemyList.Clear();
		}
		else if (sceneNum == "3" && GridMap.stageState == GridMap.StageState.HeroTutorial)
		{
			SBattleManager.Instance.EnemyList.Clear();
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.enabled = true;
			DialogueManager DM = GameObject.Find("DialogueCanvas/Panel").GetComponent<DialogueManager>();
			DialogueParser DP = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
			DM.playerTalking = false;
			DM.lineNum = 0;
			file = "Assets/Data/CreateHero";
		//	file += ".txt";
		//	GridMap.stageState = GridMap.StageState.EndStage;
		//	DP.LoadDialogue(file);
			for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
				Destroy(SBattleManager.Instance.EnemyList[i]);

			SBattleManager.Instance.EnemyList.Clear();
		}

		else
			file = "Assets/Data/Start";

		SBattleManager.Instance.Win = false;
		//sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
		if (sceneNum == "6" && GridMap.StageState.FirstMatch == GridMap.stageState)
		{
			file = "Assets/Data/Dialogue";
			file += sceneNum;
		}
		else if (sceneNum == "6" && GridMap.StageState.SecondMatch == GridMap.stageState)
		{
			file = "Assets/Data/Dialogue9";
		}
		else if (sceneNum == "6" && GridMap.StageState.ThirdMatch == GridMap.stageState)
			file = "Assets/Data/Dialogue11";
		else if (sceneNum == "6" && GridMap.StageState.FourthMatch == GridMap.stageState)
			file = "Assets/Data/Dialogue14";
		else if (sceneNum == "6" && GridMap.StageState.HeroTutorial == GridMap.stageState)
		{
			file = "Assets/Data/AboutHero";
		}
		else if ((sceneNum == "5" || sceneNum == "6") && GridMap.StageState.FirstMatch != GridMap.stageState)
		{
			this.gameObject.SetActive(false);
			GameObject dialogueCanvas = GameObject.Find("DialogueCanvas");
			dialogueCanvas.SetActive(false);
			GameObject kyle = GameObject.Find("Kyle");
			kyle.SetActive(false);
		}

		if ((int)GridMap.stageState >=11)
		{
			Canvas temp = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
			temp.gameObject.SetActive(false);
			transform.gameObject.SetActive(false);
		}

			Debug.Log(sceneNum);
		file += ".txt";


		LoadDialogue(file);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadDialogue(string filename)
	{
		lines = new List<DialogueLine>();
		string line;
		StreamReader r = new StreamReader(filename);

		using (r)
		{
			do
			{
				line = r.ReadLine();
				if (line != null)
				{
					string[] lineData = line.Split(';');
					if (lineData[0] == "Player")
					{
						DialogueLine lineEntry = new DialogueLine(lineData[0], "", 0, "");
						lineEntry.options = new string[lineData.Length - 1];
						for (int i = 1; i < lineData.Length; i++)
						{
							lineEntry.options[i - 1] = lineData[i];
						}
						lines.Add(lineEntry);
					}
					else
					{
						DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2]), lineData[3]);
						lines.Add(lineEntry);
					}
				}
			}
			while (line != null);
			r.Close();
		}
	}

	public string GetPosition(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].position;
		}

		GameObject.Find("DialogueCanvas").GetComponent<Canvas>().enabled = false;
		return "";
	}

	public string GetName(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].name;
		}
		return "";
	}

	public string GetContent(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].content;
		}
		return "";
	}

	public int GetPose(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].pose;
		}
		return 0;
	}

	public string[] GetOptions(int lineNumber)
	{
		if (lineNumber < lines.Count)
		{
			return lines[lineNumber].options;
		}
		return new string[0];
	}
}
