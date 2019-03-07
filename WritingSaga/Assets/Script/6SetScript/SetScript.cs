using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetScript : MonoBehaviour
{
    public GameObject ResourceMessage;

    public Image ITen;
    public Image IOne;

    public Image Max25Ten;
    public Image Max25One;

    public Image Thousand;
    public Image Hundred;
    public Image Ten;
    public Image One;

    public Image SNum;
	public Image STen;
    public Image LNum;
	public Image LTen;
    public Image ANum;
	public Image ATen;
    public Image RNum;
	public Image RTen;
    public Image VNum;
    public Image WNum;


    private int Max_UnitCount;
    private static int Current_UnitCount;

    public bool UnitLimit;
    public bool ResourceEnough;
    // Use this for initialization

    void Start () {

		SBattleManager.Instance.SaberCount = 0;
        SBattleManager.Instance.LancerCount = 0;
        SBattleManager.Instance.ArcherCount = 0;
        SBattleManager.Instance.RiderCount = 0;
        SBattleManager.Instance.VeteranCount = 0;
        SBattleManager.Instance.WeaponCount = 0;
		//SetStageInfo();
		    ResourceEnough = true;
			UnitLimit = false;
			Max_UnitCount = SResource.Instance.Human / 100;
		//ResourceMessage.SetActive(false);
		//if (SResource.Instance.Human == 0)

		//	switch (JsonGrid.NumberOfBlacksmith)
		//	{
		if (JsonGrid.NumberOfBlacksmith >= 4 || JsonGrid.NumberOfFarm >= 1)
		{
			SResource.Instance.Human = 0;
			SResource.Instance.Human += 500 * JsonGrid.NumberOfBlacksmith;
			SResource.Instance.Human += 900 * JsonGrid.NumberOfFarm;
		}

		if (JsonGrid.NumberOfBlacksmith == 3 && JsonGrid.NumberOfFarm == 1)
			SResource.Instance.Human = 2400;
		else if (JsonGrid.NumberOfBlacksmith == 3)
			SResource.Instance.Human = 1900;
		else if (JsonGrid.NumberOfBlacksmith == 2)
			SResource.Instance.Human = 1400;
		else if (JsonGrid.NumberOfBlacksmith==1)
				SResource.Instance.Human = 900;


		NumberUI.Instance.UIUpdate(null, null, Max25Ten, Max25One, Max_UnitCount);



		//	}
		//	SResource.Instance.Human = 3500;

	}

	// Update is called once per frame
	void Update () {
		if (SceneSwitch.stage == 3 && SBattleManager.Instance.CurrentWave == 0)
			SetStageInfo(3, 1, 0, 100, 2);
		else if (SceneSwitch.stage == 4 && SBattleManager.Instance.CurrentMonster <= 0)
			SetStageInfo(3, 2, 2, 100, 3);
		else if (SceneSwitch.stage == 5 && SBattleManager.Instance.CurrentMonster <= 0)
			SetStageInfo(4, 3, 3, 20, 3);
		else if (SceneSwitch.stage >= 6 && SBattleManager.Instance.CurrentMonster <= 0)
			SetStageInfo(5, 3, 4, 20, 4);
		else if (SceneSwitch.stage >= 7 && SBattleManager.Instance.CurrentMonster <= 0)
			SetStageInfo(5, 3, 4, 20, 4);
		else if(SceneSwitch.stage>=8 && SBattleManager.Instance.CurrentMonster <=0)
			SetStageInfo(SceneSwitch.stage - 1, SceneSwitch.stage - 3, SceneSwitch.stage - 2, 100,SceneSwitch.stage - 2);

		if (Max_UnitCount <= Current_UnitCount)
            UnitLimit = true;

        if (Max_UnitCount > Current_UnitCount)
            UnitLimit = false;

        if (!ResourceEnough)
            Warnning();

            Current_UnitCount = SBattleManager.Instance.SaberCount +
            SBattleManager.Instance.LancerCount +
            SBattleManager.Instance.ArcherCount +
           SBattleManager.Instance.RiderCount +
           SBattleManager.Instance.VeteranCount +
           SBattleManager.Instance.WeaponCount;

        NumberUI.Instance.UIUpdate(null, null, ITen,IOne, Current_UnitCount);
        NumberUI.Instance.UIUpdate(Thousand, Hundred, Ten, One, SResource.Instance.Human);

        NumberUI.Instance.UIUpdate(null, null, STen, SNum, SBattleManager.Instance.SaberCount);
		//NumberUI.Instance.UIUpdate(null, null, null, , SBattleManager.Instance.SaberCount);

		NumberUI.Instance.UIUpdate(null, null, LTen, LNum, SBattleManager.Instance.LancerCount);
		//NumberUI.Instance.UIUpdate(null, null, null, , SBattleManager.Instance.LancerCount);

		NumberUI.Instance.UIUpdate(null, null, ATen, ANum, SBattleManager.Instance.ArcherCount);
		//NumberUI.Instance.UIUpdate(null, null, null, , SBattleManager.Instance.ArcherCount);

		NumberUI.Instance.UIUpdate(null, null, RTen, RNum, SBattleManager.Instance.RiderCount);
		//NumberUI.Instance.UIUpdate(null, null, null, , SBattleManager.Instance.RiderCount);

		NumberUI.Instance.UIUpdate(null, null, null, VNum, SBattleManager.Instance.VeteranCount);
        NumberUI.Instance.UIUpdate(null, null, null, WNum, SBattleManager.Instance.WeaponCount);
    }

    public void IncreaseUnit()
    {
       Current_UnitCount++;
    }

    public void DecreaseUnit()
    {
        Current_UnitCount--;
    }

    public void Warnning()
    {
        ResourceMessage.SetActive(true);
        Invoke("WarningExit",1.0f);
    }

    public void WarningExit()
    {
        ResourceMessage.SetActive(false);
    }

	public void SetStageInfo(int iGoblinCount, int iSkeletonCount,int iWizardCount,int iLimitMonster, int iWave)
    {
        SBattleManager.Instance.GoblinCount = iGoblinCount;
        SBattleManager.Instance.SkeletonCount = iSkeletonCount;
        SBattleManager.Instance.CurrentWave = iWave;
        SBattleManager.Instance.MaxMonster = iLimitMonster;
		SBattleManager.Instance.WizardCount = iWizardCount;
    }


}
