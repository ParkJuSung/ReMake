using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridInfo{

	public GridMap.KindBuild[] buildingList = new GridMap.KindBuild[624];
	public GridMap.StageState stageInformation;
	public int battleStageInformation;
	public int gold;
	public int Iron;
	public int food;
	public GridInfo(int N, GridMap.KindBuild[] kindBuild,GridMap.StageState stage,int battleStage)
	{
		for (int i = 0; i < 624; i++)
		{
			buildingList[i] = kindBuild[i];

		}

		stageInformation = stage;
		battleStageInformation = battleStage;
		gold = SResource.Instance.GOLD;
		Iron = SResource.Instance.IRON;
		food = SResource.Instance.FOOD;

	}

	public GridInfo(int N,GridMap.KindBuild kindBuild)
	{
		for(int i=0;i<624;i++)
		{
			buildingList[i] = kindBuild;

		}
	}

	public GridInfo()
	{
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
