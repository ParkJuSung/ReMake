using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Skill : SkillManager {

    public enum Skill_State
    {
        Filling_State,
        Full_State,
        Ban_State,
		Casting_State,
    }

    public GameObject Effect;
    public Image BanObject;
    private bool Banned;

    private int SkillCost;

    public Skill_List SkillIter;
    public Skill_State Current_State;

    float CurrentTime;

    public Sprite Skill_Rush;
    public Sprite Skill_Windish;
    public Sprite Skill_Match;
    public Sprite Skill_FireExplosion;
    public Sprite Skill_Heal;
    public Sprite Skill_Thunder;
    public Sprite Skill_Avatar;
    public Sprite Skill_Summon;
    public Sprite Skill_Brave;
    public Sprite Skill_Cure;
    public Sprite Skill_Tame;


    public GameObject HealPrefab;
    public GameObject BravePrefab;
    public GameObject AvatarPrefab;
    public GameObject TamePrefab;
    public GameObject FirePrefab;
    public GameObject ExFirePrefab;
	public GameObject RushPrefab;

    public GameObject FireEffect;
    public GameObject ExFireEffect;
    public GameObject BraveEffect;
    public GameObject HealEffect;
    public GameObject AvatarEffect;
	public GameObject RushEffect;
    public GameObject TameEffect;

    public GameObject Target;

    public bool Fire;
    public bool Brave;
    public bool Heal;
    public bool Avatar;
	public bool Rush;
    public bool Tame;

	public AudioSource rushSound;
	public AudioSource tameSound;
	public AudioSource avatarSound;
	public AudioSource braveSound;
	public AudioSource healSound;
	// Use this for initialization
	void Start () {

		SkillIter = SkillList[iterator];
        iterator++;
        Effect.SetActive(false);
        Current_State = Skill_State.Filling_State;
        SetSkillSlot();
        Banned = false;
        CurrentTime =0f;
		rushSound.Stop();
		tameSound.Stop();
		avatarSound.Stop();
		braveSound.Stop();
		healSound.Stop(); ;
	}

    // Update is called once per frame
    void Update()
    {

        CheckState();

		if(Avatar)
		{
			for (int i = 0; i < SBattleManager.Instance.HeroList.Count; i++)
			{
				if (SBattleManager.Instance.HeroList[i].tag == "Unitychan")
				{
					AvatarEffect.transform.position = SBattleManager.Instance.HeroList[i].transform.position;
				}
			}
		}

		if(Rush)
		{
			for (int i = 0; i < SBattleManager.Instance.HeroList.Count; i++)
			{
				if (SBattleManager.Instance.HeroList[i].tag == "Stealth")
					RushEffect.transform.position = SBattleManager.Instance.HeroList[i].transform.position;
			}
		}

        if (Input.GetButtonDown("Fire1"))
        {
            if (Fire)
            {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo,100))
                {
                    Vector3 hitPosition = hitInfo.point;

                  //  Vector3 dir = hitPosition - transform.position;

                    ExFireEffect = (GameObject)Instantiate(ExFirePrefab, hitPosition, Quaternion.identity);
                    Destroy(ExFireEffect, ExFireEffect.GetComponent<ParticleSystem>().duration + 0.5f);

                    StartCoroutine(ExFire(hitPosition));
                 //   Debug.Log(dir.x);
                //    Debug.Log(dir.z);

                    Fire = false;
					InitCoolTime();

				}
			}
        }
    }

    void SetSkillSlot()
    {
        switch (SkillIter)
        {
            case Skill_List.Skill_Rush:
                this.GetComponent<Image>().sprite = Skill_Rush;
                break;
            case Skill_List.Skill_Windish:
                this.GetComponent<Image>().sprite = Skill_Windish;
                break;
            case Skill_List.Skill_Match:
                this.GetComponent<Image>().sprite = Skill_Match;
                break;
            case Skill_List.Skill_FireExplosion:
                this.GetComponent<Image>().sprite = Skill_FireExplosion;
                break;
            case Skill_List.Skill_Heal: // 유닛 한종류 힐
                this.GetComponent<Image>().sprite = Skill_Heal;
                break;
            case Skill_List.Skill_Thunder:
                this.GetComponent<Image>().sprite = Skill_Thunder;
                break;
            case Skill_List.Skill_Avatar: //유니티강화
                this.GetComponent<Image>().sprite = Skill_Avatar;
                break;
            case Skill_List.Skill_Summon:
                this.GetComponent<Image>().sprite = Skill_Summon;
                break;
            case Skill_List.Skill_Brave: //유닛강화
                this.GetComponent<Image>().sprite = Skill_Brave;
                break;
            case Skill_List.Skill_Cure:
                this.GetComponent<Image>().sprite = Skill_Cure;
                break;
            case Skill_List.Skill_Tame: //유닛강화
                this.GetComponent<Image>().sprite = Skill_Tame;
                break;
        }
    }

    public void Active()
    {
        if(Current_State == Skill_State.Full_State && SkillCost <= SBattleManager.Instance.currentgage)
        {
            Debug.Log(SkillIter);

            switch (SkillIter)
            {
                case Skill_List.Skill_Rush:
					Rush = true;
					rushSound.Play();
					for (int i = 0; i < SBattleManager.Instance.HeroList.Count; i++)
					{
						if (SBattleManager.Instance.HeroList[i].tag == "Stealth")
						{
							RushEffect = (GameObject)Instantiate(RushPrefab, SBattleManager.Instance.HeroList[i].transform.position, Quaternion.identity);
							SBattleManager.Instance.HeroList[i].GetComponent<Hero_Stealth>().nvAgent.speed = 4;
						}
						StartCoroutine(Rushfun(SBattleManager.Instance.HeroList[i]));
					}

					InitCoolTime();
					break;
                case Skill_List.Skill_Windish:
                    break;
                case Skill_List.Skill_Match:
                    break;
                case Skill_List.Skill_FireExplosion:
                    Fire = true;
					Current_State = Skill_State.Casting_State;
					Debug.Log(Fire);
                    break;
                case Skill_List.Skill_Heal:
                    Heal = true;
					healSound.Play();
                    if (SBattleManager.Instance.UnitList.Count > 0)
                        Target = SBattleManager.Instance.UnitList[0];


                    HealTarget();

                    for (int i = 0; i < SBattleManager.Instance.UnitList.Count; i++)
                    {
                        if (Target.tag == SBattleManager.Instance.UnitList[i].tag)
                        {
                            HealEffect = (GameObject)Instantiate(HealPrefab, SBattleManager.Instance.UnitList[i].transform.position, Quaternion.identity);
							if(Target.tag == "Archer")
								SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP = 200;
							else if(Target.tag =="Saber")
								SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP = 400;
							else if(Target.tag =="Rider")
								SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP = 600;
							else if(Target.tag =="Lancer")
								SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP = 500;

							Debug.Log(SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP);
                            Destroy(HealEffect, 1.5f);
                        }
                    }
					InitCoolTime();

					break;
            case Skill_List.Skill_Thunder:
                break;
            case Skill_List.Skill_Avatar:
					if (SBattleManager.Instance.HeroList.Count <= 0)
						return;

					Avatar = true;
					

                    for (int i = 0; i < SBattleManager.Instance.HeroList.Count; i++)
                    {
                        if (SBattleManager.Instance.HeroList[i].tag == "Unitychan")
                        {
                            AvatarEffect = (GameObject)Instantiate(AvatarPrefab, SBattleManager.Instance.HeroList[i].transform.position, Quaternion.identity);
                            SBattleManager.Instance.HeroList[i].GetComponent<Hero_Unitychan>().ATK += 20;
                            SBattleManager.Instance.HeroList[i].GetComponent<Hero_Unitychan>().HP += 200;
                            SBattleManager.Instance.HeroList[i].GetComponent<Hero_Unitychan>().transform.localScale = new Vector3(1, 1, 1);
                            StartCoroutine(Avatarfun(SBattleManager.Instance.HeroList[i]));
							avatarSound.Play();
						}
					}
					InitCoolTime();
					break;
            case Skill_List.Skill_Summon:
                break;
            case Skill_List.Skill_Brave:
					braveSound.Play();
                    Brave = true;

                    if (SBattleManager.Instance.UnitList.Count <= 0)
                        return;
                    for(int i =0;i<SBattleManager.Instance.UnitList.Count;i++)
                    {
                        BraveEffect= (GameObject)Instantiate(BravePrefab, SBattleManager.Instance.UnitList[i].transform.position + new Vector3(0,1,0), Quaternion.identity);

                        SBattleManager.Instance.UnitList[i].GetComponent<Unit>().ATK += 50;
                        SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP -= 30;

						StartCoroutine(Bravefun());
						Destroy(BraveEffect, BraveEffect.GetComponent<ParticleSystem>().duration + 0.1f);

					}
					InitCoolTime();
					break;
            case Skill_List.Skill_Cure:
                    break;
            case Skill_List.Skill_Tame:
					tameSound.Play();
					Tame = true;
                    if (SBattleManager.Instance.UnitList.Count <= 0)
                        return;
                    for (int i = 0; i < SBattleManager.Instance.UnitList.Count; i++)
                    {
                        TameEffect = (GameObject)Instantiate(TamePrefab, SBattleManager.Instance.UnitList[i].transform.position, Quaternion.identity);
                        SBattleManager.Instance.UnitList[i].GetComponent<Unit>().ATK += 20;
                        Debug.Log(SBattleManager.Instance.UnitList[i].GetComponent<Unit>().ATK);

						StartCoroutine(Tamefun());
						Destroy(TameEffect, TameEffect.GetComponent<ParticleSystem>().duration + 0.1f);
                    }
					InitCoolTime();
					break;
            }
			//InitCoolTime();

		}
	}
    IEnumerator Avatarfun(GameObject unity)
    {
			yield return new WaitForSeconds(3f);
			unity.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			Avatar = false;
			Destroy(AvatarEffect);
			StopCoroutine("Avatarfun");
			yield break;
    }

	IEnumerator Rushfun(GameObject stealth)
	{
		yield return new WaitForSeconds(3f);
		stealth.GetComponent<Hero_Stealth>().nvAgent.speed = 2;
		Rush = false;
		Destroy(RushEffect);
		StopCoroutine("Rushfun");
		yield break;
	}

	IEnumerator Tamefun()
	{
		yield return new WaitForSeconds(2.5f);
		for (int i = 0; i < SBattleManager.Instance.UnitList.Count; i++)
		{
			SBattleManager.Instance.UnitList[i].GetComponent<Unit>().ATK -= 20;
		}
		StopCoroutine(Tamefun());
		yield break;
	}

	IEnumerator Bravefun()
	{
		yield return new WaitForSeconds(2.5f);

		for (int i = 0; i < SBattleManager.Instance.UnitList.Count; i++)
		{
			SBattleManager.Instance.UnitList[i].GetComponent<Unit>().ATK -= 50;
		}

		StopCoroutine(Bravefun());
		yield break;
	}

    void InitCoolTime()
    {
        CurrentTime = 3f;

		transform.GetComponent<Image>().color =  new Color(0.33f, 0.033f, 0.33f);
		
		//transform.GetComponent<Button>().colors = cb;
        Current_State = Skill_State.Filling_State;
    }

    void CheckState()
    {
        if (SkillCost >= SBattleManager.Instance.currentgage)
            Effect.SetActive(true);

        if (Current_State == Skill_State.Filling_State)
            BanObject.fillAmount = (6f-CurrentTime) / 6f;

        if (0 >= CurrentTime && Current_State != Skill_State.Casting_State)
        {
            CurrentTime = 0;
            Current_State = Skill_State.Full_State;
			transform.GetComponent<Image>().color = new Color(1, 1, 1); ;
			BanObject.fillAmount = 0;
        }
        else
			if(0< CurrentTime)
				CurrentTime -= Time.deltaTime;
    }
    void CheckHero()
    {

    }

    void HealTarget()
    {

       // for (int i = 0; i < SBattleManager.Instance.UnitList.Count; i++)
       // {
			// if (Target.GetComponent<Unit>().HP > SBattleManager.Instance.UnitList[i].GetComponent<Unit>().HP)
			 Target = SBattleManager.Instance.UnitList[Random.Range(0,SBattleManager.Instance.UnitList.Count)];

		// Target = SBattleManager.Instance.UnitList[i];
		// }

	}

    IEnumerator ExFire(Vector3 dir)
    {
        yield return new WaitForSeconds(0.5f);
        FireEffect = Instantiate(FirePrefab, dir, Quaternion.identity);
        Destroy(FireEffect, FireEffect.GetComponent<ParticleSystem>().duration );
        for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
        {
			if (Mathf.Abs(SBattleManager.Instance.EnemyList[i].GetComponent<Unit>().transform.position.x - dir.x) <= 0.7f ||
                Mathf.Abs(SBattleManager.Instance.EnemyList[i].GetComponent<Unit>().transform.position.z - dir.z)<= 0.7f ||
				Mathf.Abs(SBattleManager.Instance.EnemyList[i].GetComponent<Unit>().transform.position.z - dir.z) <= 0.7f)
            {
                SBattleManager.Instance.EnemyList[i].GetComponent<Unit>().HP -= 500;
				//if (SBattleManager.Instance.EnemyList[i].GetComponent<Unit>().HP <= 0)
				//{
				//	Destroy(SBattleManager.Instance.EnemyList[i].gameObject);
				//	SBattleManager.Instance.EnemyList.Remove(SBattleManager.Instance.EnemyList[i]);
				//}
            }
        }
        StopCoroutine("ExFire");
    }



}
