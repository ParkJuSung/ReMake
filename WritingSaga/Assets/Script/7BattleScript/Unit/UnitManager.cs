using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 유닛 생산 담당

public class UnitManager : Unit {
    //public Unit unitlist = new Unit();
    // 일반 유닛
        int Saber_Count;
        int Lancer_Count;
        int Archer_Count;
        int Rider_Count;
        int Veteran_Count;
        int Weapon_Count;

    public GameObject SaberPrefab;
    public GameObject LancerPrefab;
    public GameObject ArcherPrefab;
    public GameObject RiderPrefab;
    public GameObject VeteranPrefab;
    public GameObject WeaponPrefab;

    public GameObject StealthPrefab;
    public GameObject SpartanKingPrefab;
    public GameObject UnitychanPrefab;

    //몬스터
    int Goblin_Count;
    int Skeleton_Count;
	int Wizard_Count;

	int Boss_Count;

    public GameObject GoblinPrefab;
    public GameObject SkeletonPrefab;
	public GameObject WizardPrefab;
	public GameObject BossPrefab;
	public GameObject DragonPrefab;


    public GameObject Reward;
	int beforStage;

	public bool start = false;

	public Image readyNumber;
	public int number;


	// Use this for initialization
	void Start ()
    {
		Reward.SetActive(false);
        Saber_Count = SBattleManager.Instance.SaberCount;
        Lancer_Count = SBattleManager.Instance.LancerCount;
        Archer_Count = SBattleManager.Instance.ArcherCount;
        Rider_Count = SBattleManager.Instance.RiderCount;
        Veteran_Count = SBattleManager.Instance.VeteranCount;
        Weapon_Count = SBattleManager.Instance.WeaponCount;
        Goblin_Count = SBattleManager.Instance.GoblinCount;
        Skeleton_Count = SBattleManager.Instance.SkeletonCount;
		Wizard_Count = SBattleManager.Instance.WizardCount;
		number = 3;
		StartCoroutine(ReadySatate());
		//EnemySummon();
       //UnitSummon();
	   
		beforStage = SceneSwitch.stage;

	}


	// Update is called once per frame
	void Update () {
		if (SBattleManager.Instance.Win && (GridMap.stageState != GridMap.StageState.EndStage && GridMap.stageState != GridMap.StageState.Ending
			|| GridMap.stageState == GridMap.StageState.InfinityMode))
		{
			Reward.SetActive(true);
			//SBattleManager.Instance.Win = false;
			SBattleManager.Instance.CurrentMonster = 0;
		}
        EntryBoss();
        BossSummon();
		DragonSummon();



    }
   
    void UnitSummon()
    {
		SBattleManager.Instance.UnitList.Clear();

        for (int i = 0; i < Rider_Count; i++)
        {
			if (SceneSwitch.stage == 1)
				SBattleManager.Instance.UnitList.Add(Instantiate(RiderPrefab, new Vector3(i + 1, 0, -8), Quaternion.identity));
			else if (SceneSwitch.stage == 2)
				SBattleManager.Instance.UnitList.Add(Instantiate(RiderPrefab, new Vector3(-14 + i, 0, -14), Quaternion.identity));
			else
				SBattleManager.Instance.UnitList.Add(Instantiate(RiderPrefab, new Vector3(i+2, 0, 0),Quaternion.identity));
		}

        for (int i= 0; i< Saber_Count; i++)
        {
			if (SceneSwitch.stage == 1)
				SBattleManager.Instance.UnitList.Add(Instantiate(SaberPrefab, new Vector3(i + 1, 0, -8), Quaternion.identity));
			else if (SceneSwitch.stage == 2)
				SBattleManager.Instance.UnitList.Add(Instantiate(SaberPrefab, new Vector3(-14 + i, 0, -14), Quaternion.identity));
			else
				SBattleManager.Instance.UnitList.Add(Instantiate(SaberPrefab, new Vector3(i+2, -1.987987f, 0), Quaternion.identity));
		}

        for (int i = 0; i < Lancer_Count; i++)
        {
			if (SceneSwitch.stage == 1)
				SBattleManager.Instance.UnitList.Add(Instantiate(LancerPrefab, new Vector3(i + 1, 0, -8), Quaternion.identity));
			else if (SceneSwitch.stage == 2)
				SBattleManager.Instance.UnitList.Add(Instantiate(LancerPrefab, new Vector3(-14 + i, 0, -14), Quaternion.identity));
			else
				SBattleManager.Instance.UnitList.Add(Instantiate(LancerPrefab, new Vector3(i+2, 0, 0), Quaternion.identity));
		}

        for (int i = 0; i < Archer_Count; i++)
        {
			if (SceneSwitch.stage == 1)
				SBattleManager.Instance.UnitList.Add(Instantiate(ArcherPrefab, new Vector3(i + 1, 0, -8), Quaternion.identity));
			else if (SceneSwitch.stage == 2)
				SBattleManager.Instance.UnitList.Add(Instantiate(ArcherPrefab, new Vector3(-14 + i, 0, -14), Quaternion.identity));
			else
				SBattleManager.Instance.UnitList.Add(Instantiate(ArcherPrefab, new Vector3(i+2, 0, 0), Quaternion.identity));
		}
       
        for (int i = 0; i < Veteran_Count; i++)
        {
			if (SceneSwitch.stage == 1)
				SBattleManager.Instance.UnitList.Add(Instantiate(VeteranPrefab, new Vector3(i + 1, 0, -8), Quaternion.identity));
			else if (SceneSwitch.stage == 2)
				SBattleManager.Instance.UnitList.Add(Instantiate(VeteranPrefab, new Vector3(-14 + i, 0, -14), Quaternion.identity));
			else
				SBattleManager.Instance.UnitList.Add(Instantiate(VeteranPrefab, new Vector3(i+2, 0, 0), Quaternion.identity));
		}
        for (int i = 0; i < Weapon_Count; i++)
        {
			if (SceneSwitch.stage == 1)
				SBattleManager.Instance.UnitList.Add(Instantiate(WeaponPrefab, new Vector3(i + 1, 0, -8), Quaternion.identity));
			else if (SceneSwitch.stage == 2)
				SBattleManager.Instance.UnitList.Add(Instantiate(WeaponPrefab, new Vector3(-14 + i, 0, -14), Quaternion.identity));
			else
				SBattleManager.Instance.UnitList.Add(Instantiate(WeaponPrefab, new Vector3(i+2, 0, 0), Quaternion.identity));
		}
        
    }

    void EnemySummon()
    {
		Debug.Log(SBattleManager.Instance.Win);
        if (!SBattleManager.Instance.Win)
        {
            if (SBattleManager.Instance.CurrentWave > 0)
            {
                for (int i = 0; i < Skeleton_Count; i++)
                {
					if (SceneSwitch.stage == 1)
						SBattleManager.Instance.EnemyList.Add(Instantiate(SkeletonPrefab, new Vector3(i + 2.25f, 0, 7.5f), new Quaternion(0, 180, 0, 0)));
					else if (SceneSwitch.stage == 2)
						SBattleManager.Instance.EnemyList.Add(Instantiate(SkeletonPrefab, new Vector3(-18 + i, 0, 12), new Quaternion(0, 180, 0, 0)));
					else
						SBattleManager.Instance.EnemyList.Add(Instantiate(SkeletonPrefab, new Vector3(i+2, 0, 23), new Quaternion(0, 180, 0, 0)));
				}

                for (int i = 0; i < Goblin_Count; i++)
                {
					if (SceneSwitch.stage == 1)
						SBattleManager.Instance.EnemyList.Add(Instantiate(GoblinPrefab, new Vector3(i + 2.25f, 0, 7.5f), new Quaternion(0, 180, 0, 0)));
					else if(SceneSwitch.stage == 2)
						SBattleManager.Instance.EnemyList.Add(Instantiate(GoblinPrefab, new Vector3(i, 0, 12), new Quaternion(0, 180, 0, 0)));
					else
						SBattleManager.Instance.EnemyList.Add(Instantiate(GoblinPrefab, new Vector3(i+2, 0, 21), new Quaternion(0, 180, 0, 0)));
				}

				for(int i = 0;i<Wizard_Count;i++)
				{
					SBattleManager.Instance.EnemyList.Add(Instantiate(WizardPrefab, new Vector3(i + 5, 0, 23), new Quaternion(0, 180, 0, 0)));
				}

				SBattleManager.Instance.CurrentWave--;
                Invoke("EnemySummon", 5f);
            }
        }

    }

    void BossSummon()
    {
        if (!SBattleManager.Instance.IsBossSummon)
        {
            if (SBattleManager.Instance.EntrtyBoss)
            {
                SBattleManager.Instance.EnemyList.Add(Instantiate(BossPrefab, new Vector3(4, 0, 7.5f), new Quaternion(0, 180, 0, 0)));
                SBattleManager.Instance.IsBossSummon = true;
            }
        }
    }

	void DragonSummon()
	{
		if (!SBattleManager.Instance.IsDragonSummon)
		{
			if (SBattleManager.Instance.EntrtyDragon)
			{
				SBattleManager.Instance.EnemyList.Add(Instantiate(DragonPrefab, new Vector3(4, 0, 7.5f), new Quaternion(0, 180, 0, 0)));
				SBattleManager.Instance.IsDragonSummon = true;
			}
		}
	}
    public void EntryBoss()
    {
		if (start)
		{
			if (SceneSwitch.stage == 3 && SBattleManager.Instance.EnemyList.Count <= 0)
			{
				SBattleManager.Instance.Win = true;
				for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
					Destroy(SBattleManager.Instance.EnemyList[i]);
				SResource.Instance.GOLD += 150;
				SResource.Instance.IRON += 100;
				SResource.Instance.FOOD += 20;
				SBattleManager.Instance.EnemyList.Clear();
				SceneSwitch.stage++;
				return;
			}
			else if (beforStage != 3 && SceneSwitch.stage == 4 && SBattleManager.Instance.EnemyList.Count <= 0)
			{
				SBattleManager.Instance.Win = true;
				for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
					Destroy(SBattleManager.Instance.EnemyList[i]);
				SResource.Instance.GOLD += 150;
				SResource.Instance.IRON += 100;
				SResource.Instance.FOOD += 20;
				SBattleManager.Instance.EnemyList.Clear();
				SceneSwitch.stage++;
				return;

			}
			else if (beforStage != 4 && SceneSwitch.stage == 5 && SBattleManager.Instance.EnemyList.Count <= 0 &&  SBattleManager.Instance.CurrentWave <= 0)
			{
				SBattleManager.Instance.Win = true;
				for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
					Destroy(SBattleManager.Instance.EnemyList[i]);
				SResource.Instance.GOLD += 150;
				SResource.Instance.FOOD += 60;
				SBattleManager.Instance.EnemyList.Clear();
				GridMap.stageState = GridMap.StageState.RiderTutorial;
				SceneSwitch.stage++;
				return;
			}
			//else if (beforStage != 5 && SceneSwitch.stage == 6 && SBattleManager.Instance.EnemyList.Count <= 0 &&  SBattleManager.Instance.CurrentWave <= 0)//SBattleManager.Instance.CurrentMonster > SBattleManager.Instance.MaxMonster)
			else if(beforStage !=5 && SceneSwitch.stage == 6 && SBattleManager.Instance.CurrentMonster > SBattleManager.Instance.MaxMonster)
			{
				SBattleManager.Instance.EntrtyDragon = true;
				//SBattleManager.Instance.CurrentMonster = 0;
				if(SBattleManager.Instance.EnemyList.Count <= 0 && SBattleManager.Instance.CurrentWave <= 0)
				{
					SBattleManager.Instance.Win = true;
					for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
						Destroy(SBattleManager.Instance.EnemyList[i]);
					SResource.Instance.GOLD += 300;
					SResource.Instance.IRON += 200;
					SBattleManager.Instance.EnemyList.Clear();
					GridMap.stageState = GridMap.StageState.HeroTutorial;
					SceneSwitch.stage++;
					return;
				}

				/*SBattleManager.Instance.Win = true;
				for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
					Destroy(SBattleManager.Instance.EnemyList[i]);
				SResource.Instance.GOLD += 300;
				SResource.Instance.IRON += 200;
				SBattleManager.Instance.EnemyList.Clear();
				GridMap.stageState = GridMap.StageState.HeroTutorial;
				SceneSwitch.stage++;*/
			}
			else if (SceneSwitch.stage == 7 && SBattleManager.Instance.CurrentMonster > SBattleManager.Instance.MaxMonster)
			{

				SBattleManager.Instance.EntrtyBoss = true;
				SBattleManager.Instance.CurrentMonster = 0;
				SResource.Instance.GOLD += 300;
				SResource.Instance.IRON += 100;
				SResource.Instance.FOOD += 100;
				//SBattleManager.Instance.Win = true;
				GridMap.stageState = GridMap.StageState.EndStage;
				SceneSwitch.stage++;
				return;
			}
			else
			{
				SBattleManager.Instance.EntrtyBoss = false;

			}

			
			if ((!SResource.Instance.isIncomeGet) && GridMap.stageState == GridMap.StageState.InfinityMode && SBattleManager.Instance.EnemyList.Count <= 0 && SBattleManager.Instance.CurrentWave == 0)
			{
				//SBattleManager.Instance.EntrtyBoss = true;
				//for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
				//	Destroy(SBattleManager.Instance.EnemyList[i]);
				SResource.Instance.isIncomeGet = true;
				SBattleManager.Instance.CurrentMonster = 0;
				SResource.Instance.GOLD += SceneSwitch.stage * 15;
				SResource.Instance.IRON += SceneSwitch.stage * 5;
				SResource.Instance.FOOD += SceneSwitch.stage * 10;
				SBattleManager.Instance.Win = true;
				SBattleManager.Instance.CurrentMonster = 0;
				//SBattleManager.Instance.EnemyList.Clear();
				//GridMap.stageState = GridMap.StageState.EndStage;
				SceneSwitch.stage++;
			}

			if(GridMap.stageState == GridMap.StageState.InfinityMode && SBattleManager.Instance.UnitList.Count<=0)
			{
				SceneManager.LoadScene("9GameOver");
			}
		}
    }

	IEnumerator StartSummon()
	{
		yield return new WaitForSeconds(3f);
		//EnemySummon();
		//UnitSummon();
		//start = true;
		//SBattleManager.Instance.Win = false;
		StopCoroutine(StartSummon());
	}

	IEnumerator ReadySatate()
	{
		while (true)
		{
			switch (number)
			{
				case 3:
					readyNumber.GetComponent<Image>().sprite = Resources.Load("Number3", typeof(Sprite)) as Sprite;
					readyNumber.GetComponent<AudioSource>().Play();
					number--;
					break;
				case 2:
					readyNumber.GetComponent<Image>().sprite = Resources.Load("Number2", typeof(Sprite)) as Sprite;
					readyNumber.GetComponent<AudioSource>().Play();
					number--;
					break;
				case 1:
					readyNumber.GetComponent<Image>().sprite = Resources.Load("Number1", typeof(Sprite)) as Sprite;
					readyNumber.GetComponent<AudioSource>().Play();
					number--;
					break;
				default:
					readyNumber.GetComponent<Image>().enabled = false;
					EnemySummon();
					UnitSummon();
					start = true;
					SBattleManager.Instance.Win = false;
					StopCoroutine(ReadySatate());
					yield break;
			}
			yield return new WaitForSeconds(1f);
		}


	}
}
