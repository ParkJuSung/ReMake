using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 유닛 공통 기능담당

public class Unit : MonoBehaviour
{
    public enum Unit_State
    {
        Idle_State,
        Find_State,
        Battle_State,
        Dead_State,
        Wining_State,
        Skill_State,
		Fear_State,
		Sceam_State,
		Archer_Find,
		Casting_State,
	}

    public bool firstAttack = true; // 만나자마자 처음떄리기
    public int HP;               // 병사수
    public int ATK;              // 공격력 = 병사수
    public int DEF;              // 방어력= 영웅의 통솔력 + 병사 방어력
    public float Agil;             // 공격속도

    public float RANGE;       // 사정거리
    public float SPEED;       // 이동속도

	public Slider HPBar;

	public Button stateScreen;
	public Button skillScreen;
	public Button skillScreen2;
	public Button skillScreen3;

	public GameObject HeadUpPosition;
	public Transform fill ;

	public Text stateText;
	public Text skillStateText;
	public Text skillStateText2;
	public Text skillStateText3;

	public GameObject FearPrefab;
	public GameObject FearEffect = null;
	public void DrawBar(Slider ber, GameObject HeadUpPosition,int barValue, int MAX, Button stateScreen,string state)
	{
		//ber

		fill = ber.transform.FindChild("Fill Area/Fill");
		ber.value = (float)barValue / MAX;
			fill.GetComponent<Image>().color = new Color(0, 255, 0);

		ber.transform.position = HeadUpPosition.transform.position;
		stateScreen.transform.position = HeadUpPosition.transform.position + new Vector3(0, 0.4f, 0);
		stateScreen.transform.FindChild("Text").GetComponent<Text>().text = state;
		//stateText.transform.position = new Vector3(HeadUpPosition.transform.position.x, HeadUpPosition.transform.position.y + 2, HeadUpPosition.transform.position.z);

		//stateText.transform.position = ber.transform.position;
	}

	public void DrawMonsterBar(Slider ber, GameObject HeadUpPosition, int barValue, int MAX)
	{
		fill = ber.transform.FindChild("Fill Area/Fill");
		ber.value = (float)barValue / MAX;
			fill.GetComponent<Image>().color = new Color(255, 0, 0);
		ber.transform.position = HeadUpPosition.transform.position;
	}

	public void DrawMonsterState(Button stateScreen, string state, GameObject HeadUpPosition)
	{
		stateScreen.transform.position = HeadUpPosition.transform.position + new Vector3(0, 0.4f, 0);
		stateScreen.transform.FindChild("Text").GetComponent<Text>().text = state;
	}

	public void DrawHeroBar(Slider ber, GameObject HeadUpPosition, int barValue, int MAX, Button stateScreen, string state, string skillState, string skillState2)
	{
		fill = ber.transform.FindChild("Fill Area/Fill");
		ber.value = (float)barValue / MAX;
		fill.GetComponent<Image>().color = new Color(0, 0, 255);

		ber.transform.position = HeadUpPosition.transform.position;

		stateScreen.transform.position = HeadUpPosition.transform.position + new Vector3(0, 0.4f, 0);
		stateScreen.transform.FindChild("Text").GetComponent<Text>().text = state;

		skillScreen.transform.position = HeadUpPosition.transform.position + new Vector3(0, 0.8f, 0);
		skillScreen.transform.FindChild("Text").GetComponent<Text>().text = skillState;

		skillScreen2.transform.position = HeadUpPosition.transform.position + new Vector3(0, 1.2f, 0);
		skillScreen2.transform.FindChild("Text").GetComponent<Text>().text = skillState2;

	//	skillScreen3.transform.position = HeadUpPosition.transform.position + new Vector3(0, 1.6f, 0);
	//	skillScreen3.transform.FindChild("Text").GetComponent<Text>().text = usingSkill;
	}

	public void IsCasting(ref Unit_State Current_State, UnityEngine.AI.NavMeshAgent nvAgent,ref GameObject target)
	{
		if (Current_State == Unit_State.Dead_State)
			return;

		if (GameObject.Find("SkillSlot5").GetComponent<Skill>().Current_State != Skill.Skill_State.Casting_State)
		{
			Current_State = Unit_State.Find_State;
			return;
		}
		if (target.name != "refuge" && target.name != "refuge1")
		{
			if (Random.Range(0, 2) == 0)
				target = GameObject.Find("refuge");
			else
				target = GameObject.Find("refuge1");

			nvAgent.destination = target.transform.position;
		}
		
	}
}
