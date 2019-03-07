using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActiveUnit : MonoBehaviour {


	private GameObject archer;
	private GameObject lancer;
	private GameObject rider;


	// Use this for initialization
	void Start () {
		if(JsonGrid.NumberOfBlacksmith>=3 && JsonGrid.NumberOfFarm>=1)
		{
			archer = GameObject.Find("UnitButton/Archer");
			lancer = GameObject.Find("UnitButton/Lancer");
			rider = GameObject.Find("UnitButton/Rider");
			ListHierarchy(archer);
			ListHierarchy(lancer);
			ListHierarchy(rider);
		}
		else if(JsonGrid.NumberOfBlacksmith==3)
		{
			archer = GameObject.Find("UnitButton/Archer");
			lancer = GameObject.Find("UnitButton/Lancer");
			rider = GameObject.Find("DeActiveUnit/Rider");
			ListHierarchy(archer);
			ListHierarchy(lancer);
			ListHierarchy(rider);
		}
		else if(JsonGrid.NumberOfBlacksmith==2)
		{
			archer = GameObject.Find("UnitButton/Archer");
			lancer = GameObject.Find("DeActiveUnit/Lancer");
			rider = GameObject.Find("DeActiveUnit/Rider");
			ListHierarchy(archer);
			ListHierarchy(lancer);
			ListHierarchy(rider);
		}
		else
		{
			archer = GameObject.Find("DeActiveUnit/Archer");
			lancer = GameObject.Find("DeActiveUnit/Lancer");
			rider = GameObject.Find("DeActiveUnit/Rider");
			ListHierarchy(archer);
			ListHierarchy(lancer);
			ListHierarchy(rider);
		}
		archer.transform.GetComponent<Image>().enabled = true;
		lancer.transform.GetComponent<Image>().enabled = true;
		rider.transform.GetComponent<Image>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ListHierarchy(GameObject go)
	{
		//Debug.Log(UnitSlot.name);
		for (int i = 0; i < go.transform.GetChildCount(); i++)
		{
			GameObject g = go.transform.GetChild(i).gameObject;
			g.transform.GetComponent<Image>().enabled = true;
			//ListHierarchy(g);
		}
	}
}
