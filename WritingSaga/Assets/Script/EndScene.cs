using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Gameover2", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = true;
		SoundManager.instance.efxSource.loop = true;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
