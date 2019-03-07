using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeroActive : MonoBehaviour {

	GameObject HeroSlot;

	public GameObject particlePrefab;
	public GameObject particleEffect;
	// Use this for initialization
	void Start () {
		if (JsonGrid.NumberOfMine >= 1)
		{
			HeroSlot = GameObject.Find("HeroManager/Active");
			Debug.Log(HeroSlot);
			ListHierarchy(HeroSlot);
			particleEffect = (GameObject)Instantiate(particlePrefab, new Vector3(-1.186f, 1.076f, 3.1f), particlePrefab.transform.rotation);
			Destroy(particleEffect, particleEffect.GetComponent<ParticleSystem>().duration - 3.5f);
		}
		else
		{
			HeroSlot = GameObject.Find("HeroManager/DeActive");
			Debug.Log(HeroSlot);
			ListHierarchy(HeroSlot);
			//particleEffect = (GameObject)Instantiate(particlePrefab, new Vector3(-3.8f, 2.4f, 3.1f), particlePrefab.transform.rotation);
			//Destroy(particleEffect, particleEffect.GetComponent<ParticleSystem>().duration - 3.5f);
		}
		//HeroSlot.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ListHierarchy(GameObject go)
	{
		Debug.Log(HeroSlot.name);
		for (int i = 0; i < go.transform.GetChildCount(); i++)
		{
			GameObject g = go.transform.GetChild(i).gameObject;
			g.transform.GetComponent<Image>().enabled = true;
			//ListHierarchy(g);
		}
	}
}
