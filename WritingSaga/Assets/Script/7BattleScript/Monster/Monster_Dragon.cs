using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Dragon : Unit
{

    public GameObject target;
    public NavMeshAgent nvAgent; // 네비

    public bool LockOn;

    Animator mAnimator = null;

    public Unit_State Current_State;
    float targetdistance;

	public AudioSource scream;

	private void Awake()
    {
		Dragon_Init();
    }

    void Start()
    {
        LockOn = false;

        nvAgent = gameObject.GetComponent<NavMeshAgent>();

        mAnimator = gameObject.GetComponent<Animator>(); //애니메이션할 객체 얻기

        mAnimator.SetBool("isBasic", false);
		mAnimator.SetBool("isDie", false);
		mAnimator.SetBool("isWalk", false);

		HPBar = Instantiate(HPBar, transform.position, Quaternion.identity);
		stateScreen = Instantiate(stateScreen, transform.position, Quaternion.identity);
		//stateText = Instantiate(stateText, transform.position, Quaternion.identity);
		HPBar.transform.parent = GameObject.Find("MonsterHPCanvas").transform;
		stateScreen.transform.parent = GameObject.Find("MonsterHPCanvas").transform;

		Current_State = Unit_State.Sceam_State;
		scream.Play();

		StartCoroutine(SetAnimator());

    }

    // Update is called once per frame
    void Update()
    {
        mAnimator.SetBool("isBasic", false);


		CheckAlive();

        if (SBattleManager.Instance.UnitList.Count <= 0 && SBattleManager.Instance.HeroList.Count <= 0)
            Current_State = Unit_State.Wining_State;

		DrawMonsterBar(HPBar, HeadUpPosition, HP, 3000);
		DrawMonsterState(stateScreen, Current_State.ToString(), HeadUpPosition);
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
				case Unit_State.Sceam_State:
					StartCoroutine(Sceam());
					break;
            }
            yield return null;
        }

    }

    void Dragon_Init()
    {
        HP = 3000;               // 병사수
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

	IEnumerator Sceam()
	{
		mAnimator.SetBool("isScream", true);
		for(int i=0;i<SBattleManager.Instance.UnitList.Count;i++)
		{
			switch(SBattleManager.Instance.UnitList[i].tag)
			{
				case "Saber":
					SBattleManager.Instance.UnitList[i].GetComponent<Unit_Saber>().Current_State = Unit_State.Fear_State;
					break;
				case "Archer":
					SBattleManager.Instance.UnitList[i].GetComponent<Unit_Archer>().Current_State = Unit_State.Fear_State;
					break;
				case "Lancer":
					SBattleManager.Instance.UnitList[i].GetComponent<Unit_Lancer>().Current_State = Unit_State.Fear_State;
					break;
				//case "Rider":
				//	SBattleManager.Instance.UnitList[i].GetComponent<Unit_Rider>().Current_State = Unit_State.Fear_State;
				//	break;
			}
		}
		yield return new WaitForSeconds(2);
		mAnimator.SetBool("isScream", false);

		Current_State = Unit_State.Find_State;
		yield break;
	}

	void Battle()
    {
        mAnimator.SetBool("isWalk", false);
        mAnimator.SetBool("isBasic", true);

        if (SBattleManager.Instance.UnitList.Count <= 0 && SBattleManager.Instance.HeroList.Count <= 0)
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
            case "Saber":
                target.GetComponent<Unit_Saber>().SetUnitHp(ATK);
                if (target.GetComponent<Unit_Saber>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Archer":
                target.GetComponent<Unit_Archer>().SetUnitHp(ATK);
                if (target.GetComponent<Unit_Archer>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Lancer":
                target.GetComponent<Unit_Lancer>().SetUnitHp(ATK);
                if (target.GetComponent<Unit_Lancer>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Rider":
                target.GetComponent<Unit_Rider>().SetUnitHp(ATK);
                if (target.GetComponent<Unit_Rider>().GetUnitHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "SpartanKing":
                target.GetComponent<Hero_SpartanKing>().SetHeroHp(ATK);
                if (target.GetComponent<Hero_SpartanKing>().GetHeroHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Stealth":
                target.GetComponent<Hero_Stealth>().SetHeroHp(ATK);
                if (target.GetComponent<Hero_Stealth>().GetHeroHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
            case "Unitychan":
                target.GetComponent<Hero_Unitychan>().SetHeroHp(ATK);
                if (target.GetComponent<Hero_Unitychan>().GetHeroHP() <= 0)
                {
                    LockOn = false;
                    Current_State = Unit_State.Find_State;
                }
                break;
        }
    }

    void FindTarget() // 적 탐색 함수
    {
        if (SBattleManager.Instance.UnitList.Count > 0)
        {
            target = SBattleManager.Instance.UnitList[0];
            for (int UnitIter = 0; UnitIter < SBattleManager.Instance.UnitList.Count; UnitIter++)
            {
                float distance = Vector3.Distance(SBattleManager.Instance.UnitList[UnitIter].transform.position, gameObject.transform.position);
                targetdistance = Vector3.Distance(target.transform.position, gameObject.transform.position);

                if (distance < targetdistance)                                                     //타겟을 바꿔야할 때
                {
                    target = SBattleManager.Instance.UnitList[UnitIter];
                    targetdistance = distance;
                }
            }
        }

        if (SBattleManager.Instance.HeroList.Count > 0)
        {
            if (target == null)
                target = SBattleManager.Instance.HeroList[0];
            for (int Herolter = 0; Herolter < SBattleManager.Instance.HeroList.Count; Herolter++)
            {
                float distance = Vector3.Distance(SBattleManager.Instance.HeroList[Herolter].transform.position, gameObject.transform.position);
                targetdistance = Vector3.Distance(target.transform.position, gameObject.transform.position);

                if (distance < targetdistance)                                                     //타겟을 바꿔야할 때
                {
                    target = SBattleManager.Instance.HeroList[Herolter];
                    targetdistance = distance;
                }
            }
        }

    }

    void MoveToTarget() // 적한테 이동함수
    {
        if (SBattleManager.Instance.UnitList.Count > 0 || SBattleManager.Instance.HeroList.Count > 0)
        {
            nvAgent.destination = target.transform.position;
            mAnimator.SetBool("Iswalk", true);

            switch (target.tag)
            {

                case "Saber":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "Archer":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "Lancer":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "Rider":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "SpartanKing":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;

                    break;
                case "Stealth":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
                case "Unitychan":
                    if (!LockOn)
                    {
                        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= RANGE)
                            LockOn = true;
                    }
                    else
                        Current_State = Unit_State.Battle_State;
                    break;
            }
        }
    }

    public void Dead()
    {
        SBattleManager.Instance.CurrentMonster++;
        SBattleManager.Instance.EnemyList.Remove(gameObject);
		mAnimator.SetBool("isDie", true);
		//SceneSwitch.stage++;
        Destroy(gameObject,1.5f);
    }

    void Win()
    {
        mAnimator.SetBool("Iswalk", false);
        LockOn = false;
        if (SBattleManager.Instance.UnitList.Count != 0 || SBattleManager.Instance.HeroList.Count != 0)
        {
            FindTarget();
            Current_State = Unit_State.Find_State;
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Fireball")
			HP -= 500;
	}
}