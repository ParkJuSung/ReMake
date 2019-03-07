using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit_Rider : Unit {


    public GameObject target;
    public NavMeshAgent nvAgent; // 네비

	public bool LockOn;

    Animator mAnimator = null;

    private int ATKAnim;

    public Unit_State Current_State;

    float targetdistance;

	public AudioSource Horse;


	private void Awake()
    {
        Init();
    }

    void Start()
    {
        LockOn = false;

        nvAgent = gameObject.GetComponent<NavMeshAgent>();

        mAnimator = gameObject.GetComponent<Animator>(); //애니메이션할 객체 얻기

        mAnimator.SetBool("Isattack", false);
        mAnimator.SetBool("Iswalk", true);
		HPBar = Instantiate(HPBar, transform.position, Quaternion.identity);
		stateScreen = Instantiate(stateScreen, transform.position, Quaternion.identity);
		//stateText = Instantiate(stateText, transform.position, Quaternion.identity);
		HPBar.transform.parent = GameObject.Find("HPCanvas").transform;
		stateScreen.transform.parent = GameObject.Find("HPCanvas").transform;
		Current_State = Unit_State.Find_State;

        StartCoroutine(SetAnimator());

    }

    // Update is called once per frame
    void Update()
    {
        mAnimator.SetBool("Isattack", false);
        CheckAlive();

        if (SBattleManager.Instance.EnemyList.Count == 0)
            Current_State = Unit_State.Wining_State;

		DrawBar(HPBar, HeadUpPosition, HP, 1000,stateScreen, Current_State.ToString());

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
					FearMove();
					FindTarget();
                    MoveToTarget();
                    break;
                case Unit_State.Battle_State:
					FearMove();
					Battle();
                    yield return new WaitForSeconds(Agil);
                    break;
				case Unit_State.Fear_State:
					FearMove();
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
        HP = 1000;               // 병사수
        ATK = 100;              // 공격력 = 병사수
        DEF = 0;              // 방어력= 영웅의 통솔력 + 병사 방어력
        Agil = 0.92f;             // 공격속도

        RANGE = 0.8f;       // 사정거리
        SPEED = 2;       // 이동속도
        FindTarget();
    }

    void Idle()
    {

    }
    void ATKAnimation()
    {
        mAnimator.SetBool("Iswalk", false);

        ATKAnim = Random.Range(1, 3);
        switch (ATKAnim)
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
                Agil = 1.3f;
                break;
        }
    }
    void Battle()
    {
        mAnimator.SetBool("Iswalk", false);
        mAnimator.SetBool("Isattack", true);

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
            case "EnemySaber":
                target.GetComponent<Monster_Saber>().SetUnitHp(ATK);
                if (target.GetComponent<Monster_Saber>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
			case "Dragon":
				target.GetComponent<Monster_Dragon>().SetUnitHp(ATK);
				if (target.GetComponent<Monster_Dragon>().GetUnitHP() <= 0)
				{
					LockOn = false;
					Current_State = Unit_State.Find_State;
				}
				break;
		}
        Agil = Random.Range(0.90f, 0.96f);

    }

    void FindTarget() // 적 탐색 함수
    {
        if (SBattleManager.Instance.EnemyList.Count > 0)
        {
			for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
			{
				target = SBattleManager.Instance.EnemyList[i];
				if (target.tag != "Wizard")
				{
					break;
				}
			}
			if (target.tag == "Wizard")
			{
				mAnimator.SetBool("Iswalk", false);
				return;
			}
			//if (target.transform.tag == "Wizard")
			//	target = null;

			for (int EnemyIter = 0; EnemyIter < SBattleManager.Instance.EnemyList.Count; EnemyIter++)
            {
				if (SBattleManager.Instance.EnemyList[EnemyIter].tag == "Wizard")
					continue;

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
			if (target.tag == "Wizard")
				return;

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
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "EnemySaber":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
				case "Dragon":
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
		Current_State = Unit_State.Dead_State;
        SResource.Instance.SOUL = SResource.Instance.SOUL + 100;
		Destroy(HPBar.transform.gameObject);
		Destroy(gameObject);
		Destroy(stateScreen.gameObject, 0.5f);
		SBattleManager.Instance.UnitList.Remove(gameObject);
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
	void FearMove()
	{
		for(int i=0;i<SBattleManager.Instance.EnemyList.Count;i++)
		{
			if(Mathf.Abs(Vector3.Distance(transform.position,SBattleManager.Instance.EnemyList[i].transform.position))<1.5f&&
				SBattleManager.Instance.EnemyList[i].tag == "Wizard")
			{
				mAnimator.SetBool("Iswalk", true);
				Current_State = Unit_State.Fear_State;
				if (FearEffect == null)
				{
					FearEffect = (GameObject)Instantiate(FearPrefab, transform.position, Quaternion.identity);
					Destroy(FearEffect, FearEffect.GetComponent<ParticleSystem>().duration + 0.1f);
					nvAgent.destination = new Vector3(Random.Range(0, 15), transform.position.y, Random.Range(0, 10));
				}
				Debug.Log("Rider destination" + nvAgent.destination);
			}
		}
		if (Current_State == Unit_State.Fear_State)
		{
			FearEffect.transform.position = transform.position;
			if (Mathf.Abs(Vector3.Distance(transform.position, nvAgent.destination)) <= 1f)
			{
				Current_State = Unit_State.Find_State;
			}
		}
	}

	public int GetUnitHP()
    {
        return HP;
    }

    public int SetUnitHp(int atk)
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
