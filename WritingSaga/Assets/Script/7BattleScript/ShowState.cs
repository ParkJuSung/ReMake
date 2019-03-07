using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowState : MonoBehaviour {

	public Text t;
	Component g;
		
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < SBattleManager.Instance.EnemyList.Count; i++)
		{
			g = SBattleManager.Instance.EnemyList[i].GetComponent<Monster_Goblin>();
			if(g!=null)
			{
				t.text+= SBattleManager.Instance.EnemyList[i].GetComponent<Monster_Goblin>().Current_State.ToString();
			}
			if (g == null)
			{
				g = SBattleManager.Instance.EnemyList[i].GetComponent<Monster_Skeleton>();
				if (g != null)
					t.text+= SBattleManager.Instance.EnemyList[i].GetComponent<Monster_Skeleton>().Current_State.ToString();
			}
			else if (g == null)
			{
				g = SBattleManager.Instance.EnemyList[i].GetComponent<Monster_Boss>();
				if (g != null)
					t.text+= SBattleManager.Instance.EnemyList[i].GetComponent<Monster_Boss>().Current_State.ToString();
			}
		}

	}
}
