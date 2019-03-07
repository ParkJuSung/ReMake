using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
//using UnityEditor;

public class DialogueManager : MonoBehaviour {

	DialogueParser parser;

	public string dialogue, characterName;
	public int lineNum;
	int pose;
	string position;
	string[] options;
	public bool playerTalking;
	List<Button> buttons = new List<Button>();

	public Text dialogueBox;
	public Text nameBox;
	public GameObject choiceBox;
	//public GameObject exitBox;
	public GameObject arrow;
	public GameObject arrow2;
	// Use this for initialization
	void Start () {
		dialogue = "";
		characterName = "";
		pose = 0;
		position = "L";
		playerTalking = false;
		parser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
		lineNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && playerTalking == false)
		{
			ShowDialogue();

			lineNum++;
		}

		UpdateUI();
	}

	public void ShowDialogue()
	{
		ResetImages();
		ParseLine();
	}

	void ResetImages()
	{
		if (characterName != "")
		{
			GameObject character = GameObject.Find(characterName);
			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
			currSprite.sprite = null;
		}
	}

	void ParseLine()
	{
		if (parser.GetName(lineNum) != "Player")
		{
			playerTalking = false;
			characterName = parser.GetName(lineNum);
			dialogue = parser.GetContent(lineNum);
			pose = parser.GetPose(lineNum);
			
			position = parser.GetPosition(lineNum);
			//string sceneNum = EditorApplication.currentScene;
			string sceneNum = SceneManager.GetActiveScene().buildIndex.ToString();
			//sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
			if(lineNum == 9 && GridMap.stageState == GridMap.StageState.tutorial2 && sceneNum == "3")
			{
				GameObject[] Buildings = new GameObject[3];
				Buildings[0] = GameObject.Find("BuildingCanvas/mine");
				Buildings[1] = GameObject.Find("BuildingCanvas/windmill");
				Buildings[2] = GameObject.Find("BuildingCanvas/Farm");
				for (int i = 0; i < Buildings.Length; i++)
					Buildings[i].SetActive(false);

			}

			if ((lineNum == 5 && GridMap.stageState == GridMap.StageState.tutorial && sceneNum =="3") ||
				lineNum == 1 && GridMap.stageState == GridMap.StageState.FirstMatch && sceneNum == "6")
			{
				arrow.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow));
			}
			else if(lineNum==0)
			{
				arrow.GetComponent<Image>().enabled = false;
				StopCoroutine(ChangeColor(arrow));
			}

			if (lineNum > 1 && GridMap.stageState == GridMap.StageState.FirstMatch && sceneNum == "3")
			{
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));
			}
			else
			{
				arrow2.GetComponent<Image>().enabled = false;
				StopCoroutine(ChangeColor(arrow2));
			}
			if (//(lineNum == 1 && GridMap.stageState == GridMap.StageState.SecondMatch && sceneNum != "5") ||
				lineNum >=1 && GridMap.stageState == GridMap.StageState.SecondMatch && sceneNum == "6")
			{
				arrow2 = GameObject.Find("arrow4");
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));

			}
			else
			{
				arrow2.GetComponent<Image>().enabled = false;
				StopCoroutine(ChangeColor(arrow2));
			}//차후 유닛 추가하면 사용 예정

			if (((lineNum <=3 && lineNum>=2) && GridMap.stageState == GridMap.StageState.tutorial2 && sceneNum == "3") ||
				(lineNum == 8 && GridMap.stageState == GridMap.StageState.tutorial2 && sceneNum == "3"))
			{
				//StopCoroutine(ChangeColor(arrow2));
				//arrow2.GetComponent<Image>().enabled = false;
				arrow2 = GameObject.Find("BuildingCanvas/arrow(blacksmith)");
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));
			}
			else if(((lineNum <=5 && lineNum>=4) && GridMap.stageState == GridMap.StageState.tutorial2 && sceneNum == "3"))
			{
				StopCoroutine(ChangeColor(arrow2));
				arrow2.GetComponent<Image>().enabled = false;
				arrow2 = GameObject.Find("BuildingCanvas/arrow(farm)");
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));
			}
			else if ((lineNum == 6 && GridMap.stageState == GridMap.StageState.tutorial2 && sceneNum == "3"))
			{
				StopCoroutine(ChangeColor(arrow2));
				arrow2.GetComponent<Image>().enabled = false;
				arrow2 = GameObject.Find("BuildingCanvas/arrow(windmill)");
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));
			}
			else if ((lineNum == 7 && GridMap.stageState == GridMap.StageState.tutorial2 && sceneNum == "3"))
			{
				StopCoroutine(ChangeColor(arrow2));
				arrow2.GetComponent<Image>().enabled = false;
				arrow2 = GameObject.Find("BuildingCanvas/arrow(mine)");
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));
			}

			if ((lineNum >= 1 && GridMap.stageState == GridMap.StageState.FourthMatch && sceneNum == "6"))
			{
				arrow = GameObject.Find("arrow3");
				arrow.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow));
			}
			
			if ((lineNum >= 1 && GridMap.stageState == GridMap.StageState.ThirdMatch && sceneNum == "6"))
			{
				arrow2.GetComponent<Image>().enabled = true;
				StartCoroutine(ChangeColor(arrow2));
			}//차후 유닛증가하면 쓸 예정

			if (position == "")
			{
				playerTalking = true;
				lineNum = 0;
			}
			DisplayImages();
		}
		/*else if(parser.GetName(lineNum) == "Player")
		{
			playerTalking = true;
			characterName = "";
			dialogue = "";
			pose = 0;
			position = "";
			options = parser.GetOptions(lineNum);
			CreateButtons();
		}*/
	}

	void DisplayImages()
	{
		if (characterName != "")
		{
			GameObject character = GameObject.Find(characterName);

			SetSpritePositions(character);

			SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();
			currSprite.sprite = character.GetComponent<Character>().characterPoses[0];
			// 나중에 표정 변화 생기면 이걸로 쓸 것currSprite.sprite = character.GetComponent<Character>().characterPoses[pose];
		}
	}

	void SetSpritePositions(GameObject spriteObj)
	{
		if (position == "L")
		{
			spriteObj.transform.position = new Vector3(-6, 0);
			
		}
		else if (position == "R")
		{
			spriteObj.transform.position = new Vector3(1.71f, 0.08f,1.5f);
		}
		//spriteObj.transform.position = new Vector3(spriteObj.transform.position.x, spriteObj.transform.position.y, 0);
	}

	/*void CreateButtons()
	{
		for (int i = 0; i < options.Length; i++)
		{
			GameObject button = (GameObject)Instantiate(choiceBox);
			Button b = button.GetComponent<Button>();
			ChoiceButton cb = button.GetComponent<ChoiceButton>();
			cb.SetText(options[i].Split(':')[0]);
			cb.option = options[i].Split(':')[1];
			cb.box = this;
			b.transform.SetParent(this.transform);
			b.transform.localPosition = new Vector3(0, -25 + (i * 50));
			b.transform.localScale = new Vector3(1, 1, 1);
			buttons.Add(b);
		}
	}*/

	void UpdateUI()
	{
		if (!playerTalking)
		{
			if(lineNum>=7)
			{
				//exitBox.SetActive(true);
			}
			ClearButtons();

		}
		dialogueBox.text = dialogue;
		
		nameBox.text = characterName;
	}

	void ClearButtons()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			print("Clearing buttons");
			Button b = buttons[i];
			buttons.Remove(b);
			Destroy(b.gameObject);
		}
	}

	public IEnumerator ChangeColor(GameObject arrow)
	{
		while (true)
		{
			if (arrow.GetComponent<Image>().color == new Color(255, 255, 255))
				arrow.GetComponent<Image>().color = new Color(0, 255, 255);
			else
				arrow.GetComponent<Image>().color = new Color(255, 255, 255);

			yield return new WaitForSeconds(0.5f);
		}
	}
}
