using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
public class ConquestManager : MonoBehaviour {

	public Button stageOne;
	public Button stageTwo;

	// Use this for initialization
	void Start () {
		stageTwo.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneSwitch.stage==2)
		{
			stageOne.gameObject.SetActive(false);
			stageTwo.gameObject.SetActive(true);
		}
	}

    void GongSaJung()
    {
        SceneManager.LoadScene("4ShopScene");
    }
    void Open()
    {
        SceneManager.LoadScene("6SetScene");
    }
}
