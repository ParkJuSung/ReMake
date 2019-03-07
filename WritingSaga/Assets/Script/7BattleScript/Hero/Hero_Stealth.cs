﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero_Stealth : Unit {
    public GameObject target;
    public NavMeshAgent nvAgent; // 네비

    private int ATKanim;
    public bool LockOn;

    public GameObject SummonPrefab;

    Animator mAnimator = null;
    float targetdistance;

    Unit_State Current_State;

	public GameObject skillSlot;
	public GameObject skillSlot2;

	private void Awake()
    {
        Init();
    }

    void Start()
    {
        LockOn = false;
        GameObject Curespell = (GameObject)Instantiate(SummonPrefab, gameObject.transform.position, Quaternion.identity);

        nvAgent = gameObject.GetComponent<NavMeshAgent>();

        mAnimator = gameObject.GetComponent<Animator>(); //애니메이션할 객체 얻기
		HPBar = Instantiate(HPBar, transform.position, Quaternion.identity);
		stateScreen = Instantiate(stateScreen, transform.position, Quaternion.identity);
		skillScreen = Instantiate(skillScreen, transform.position, Quaternion.identity);
		skillScreen2 = Instantiate(skillScreen2, transform.position, Quaternion.identity);

		mAnimator.SetBool("Isattack", false);
        mAnimator.SetBool("Isattack2", false);
        mAnimator.SetBool("Iswalk", true);

		HPBar.transform.parent = GameObject.Find("HPCanvas").transform;
		stateScreen.transform.parent = GameObject.Find("HPCanvas").transform;
		skillScreen.transform.parent = GameObject.Find("HPCanvas").transform;
		skillScreen2.transform.parent = GameObject.Find("HPCanvas").transform;

		Current_State = Unit_State.Find_State;
        Destroy(Curespell, Curespell.GetComponent<ParticleSystem>().duration + 1f);
        StartCoroutine(SetAnimator());

		if (Hero.summonIsVeteran[0])
		{
			skillSlot = GameObject.Find("SkillSlot3");
			skillSlot2 = GameObject.Find("SkillSlot4");
		}
		else if (Hero.summonIsVeteran[1])
		{
			skillSlot = GameObject.Find("SkillSlot7");
			skillSlot2 = GameObject.Find("SkillSlot8");
		}

	}

    // Update is called once per frame
    void Update()
    {
        mAnimator.SetBool("Isattack", false);
        mAnimator.SetBool("Isattack2", false);

        CheckAlive();

        if (SBattleManager.Instance.EnemyList.Count == 0)
            Current_State = Unit_State.Wining_State;
		DrawHeroBar(HPBar, HeadUpPosition, HP, 1200, stateScreen,Current_State.ToString(),
			"Rush:" + skillSlot.GetComponent<Skill>().Current_State.ToString(),
			"Heal:" + skillSlot2.GetComponent<Skill>().Current_State.ToString());

	}

	IEnumerator SetAnimator()
    {
        while (true)
        {
            switch (Current_State)
            {
                case Unit_State.Idle_State:
                    break;
                case Unit_State.Find_State:
                    FindTarget();
                    MoveToTarget();
                    break;
                case Unit_State.Battle_State:
                    Battle();
                    yield return new WaitForSeconds(Agil);
                    break;
                case Unit_State.Dead_State:
                    Dead();
                    break;
                case Unit_State.Wining_State:
                    Win();
                    break;
            }
            yield return null;

        }

    }

    void Init()
    {
        HP = 1200;               // 병사수
        ATK = 100;              // 공격력 = 병사수
        DEF = 0;              // 방어력= 영웅의 통솔력 + 병사 방어력
        Agil = 0.92f;             // 공격속도

        RANGE = 1   ;       // 사정거리
        SPEED = 2;       // 이동속도
        FindTarget();
    }

    void Idle()
    {

    }

    void Battle()
    {
        ATKAnimaton();

        if (SBattleManager.Instance.EnemyList.Count == 0)
        {
            Current_State = Unit_State.Wining_State;
            return;
        }
        else if (target == null)
        {
            Current_State = Unit_State.Find_State;
            return;
        }

        switch (target.tag)
        {
            case "Goblin":
                target.GetComponent<Monster_Goblin>().SetUnitHp(ATK);
                if (target.GetComponent<Monster_Goblin>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Skeleton":
                target.GetComponent<Monster_Skeleton>().SetUnitHp(ATK);
                if (target.GetComponent<Monster_Skeleton>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Boss":
                target.GetComponent<Monster_Boss>().SetUnitHp(ATK);
                if (target.GetComponent<Monster_Boss>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
			case "Wizard":
				target.GetComponent<Monster_Wizard>().SetUnitHp(ATK);
				if (target.GetComponent<Monster_Wizard>().GetUnitHP() <= 0)
				{
					LockOn = false;
					Current_State = Unit_State.Find_State;
				}
				break;
		}
    }

    void ATKAnimaton()
    {
        mAnimator.SetBool("Iswalk", false);

        ATKanim = Random.Range(1, 3);
        switch (ATKanim)
        {
            case 1:
                mAnimator.SetBool("Isattack2", false);
                mAnimator.SetBool("Isattack", true);
                ATK = 50;
                Agil = 0.92f;
                break;
            case 2:
                mAnimator.SetBool("Isattack", false);
                mAnimator.SetBool("Isattack2", true);
                ATK = 70;
                Agil = 1.8f;
                break;
        }
    }

    void FindTarget() // 적 탐색 함수
    {
        if (SBattleManager.Instance.EnemyList.Count > 0)
        {
            target = SBattleManager.Instance.EnemyList[0];
            for (int EnemyIter = 0; EnemyIter < SBattleManager.Instance.EnemyList.Count; EnemyIter++)
            {
                float distance = Vector3.Distance(SBattleManager.Instance.EnemyList[EnemyIter].transform.position, gameObject.transform.position);
                targetdistance = Vector3.Distance(target.transform.position, gameObject.transform.position);

                if (distance < targetdistance)                                                     //타겟을 바꿔야할 때
                {
                    target = SBattleManager.Instance.EnemyList[EnemyIter];
                    targetdistance = distance;
                }
            }
        }
    }

    void MoveToTarget() // 적한테 이동함수
    {
        if (SBattleManager.Instance.EnemyList.Count > 0)
        {
            nvAgent.destination = target.transform.position;
            mAnimator.SetBool("Iswalk", true);

            switch (target.tag)
            {

                case "Goblin":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "Skeleton":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "Boss":
                    if(!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                        else
                            Current_State = Unit_State.Battle_State;
                    }
                    break;
				case "Wizard":
					if (!LockOn)
					{
						if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
						{
							LockOn = true;
						}
					}
					else
						Current_State = Unit_State.Battle_State;
					break;
			}
        }
    }

    public void Dead()
    {
        SResource.Instance.SOUL = SResource.Instance.SOUL + 100;
        SBattleManager.Instance.HeroList.Remove(gameObject);
        Destroy(gameObject);
		Destroy(skillScreen.gameObject);
		Destroy(skillScreen2.gameObject);
		Destroy(skillScreen3.gameObject);
		Destroy(HPBar.gameObject);
    }

    void Win()
    {
        mAnimator.SetBool("Iswalk", false);
        LockOn = false;
        if (SBattleManager.Instance.EnemyList.Count != 0)
        {
            FindTarget();
            Current_State = Unit_State.Find_State;
        }
    }

    public int GetHeroHP()
    {
        return HP;
    }

    public int SetHeroHp(int atk)
    {
        HP = HP - atk;
        return HP;
    }
    void CheckAlive()
    {
        if (HP <= 0)
            Dead();
    }
}
