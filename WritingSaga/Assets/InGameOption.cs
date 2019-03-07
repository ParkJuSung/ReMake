using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameOption : MonoBehaviour {



	public Canvas optionCanvas;

	public Button soundExit;
	public Slider soundVolume;

	public GridMap grid;
	// Use this for initialization
	void Start () {
		grid = GameObject.Find("Plane").GetComponent<GridMap>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickSound()
	{
		optionCanvas.enabled = false;

		soundVolume.gameObject.SetActive(true);
		soundExit.gameObject.SetActive(true);
	}

	public void OnExitSound()
	{
		optionCanvas.enabled = true;

		soundVolume.gameObject.SetActive(false);
		soundExit.gameObject.SetActive(false);
	}

	public void ExitOption()
	{
		optionCanvas.enabled = false;
		grid.optionUI = false;

	}
	public void OnValueChanged()
	{
		SoundManager.instance.efxSource.volume = soundVolume.value;
	}
}
