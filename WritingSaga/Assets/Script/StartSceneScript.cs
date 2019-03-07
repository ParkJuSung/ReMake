using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class StartSceneScript : MonoBehaviour {


    private float currenttime;
    public bool LoadComplete;
    public GameObject PressAnyTouch;
    public GameObject Effect;

    private bool Uping;

    // Use this for initialization
    void Start () {
        Effect.SetActive(false);
        LoadComplete = false;
        SResource.Instance.SetUser();
        Invoke("Switch", 3f);
        Uping = true;

		StartCoroutine(printSound());
	}
	
	// Update is called once per frame
	void Update () {
        currenttime += Time.deltaTime;
        if(currenttime>3f)
        {
            Uping = false;
            if (currenttime >= 6f)
            {
                Uping = true;
                currenttime = 0;
            }                
        }

        if (Uping)
            Up();
        else if(!Uping)
            Down();

        /*if (Input.GetMouseButtonDown(0) && LoadComplete)
        {
			SceneChange();
			SoundManager.instance.musicSource.clip = Resources.Load("Sound/With love from Vertex Studio (7)", typeof(AudioClip)) as AudioClip;
			SoundManager.instance.musicSource.loop = true;
			SoundManager.instance.efxSource.loop = true;

			SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
		}*/
	}


    void Up()
    {
        this.transform.Translate(new Vector3(0, -1 * Time.deltaTime, 0));
    }

    void Down()
    {
        this.transform.Translate(new Vector3(0, 1 * Time.deltaTime, 0));
    }
    
    void Switch()
    {
        Effect.SetActive(true);
        LoadComplete = true;
    }
    
    void SceneChange()
    {
		JsonGrid.NumberOfBlacksmith = 0;
		JsonGrid.NumberOfFarm = 0;
		JsonGrid.NumberOfMine = 0;
		SceneManager.LoadScene("3MenuScene");
	//	SoundManager.instance.musicSource.clip = Resources.Load("Sound/With love from Vertex Studio (7)", typeof(AudioClip)) as AudioClip;
	//	SoundManager.instance.musicSource.loop = true;
	//	SoundManager.instance.efxSource.loop = true;

	//	SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
	}

	IEnumerator printSound()
	{
		yield return new WaitForSeconds(1.5f);
		SoundManager.instance.musicSource.clip = Resources.Load("Sound/Twinkle", typeof(AudioClip)) as AudioClip;
		SoundManager.instance.musicSource.loop = false;
		SoundManager.instance.efxSource.loop = false;
		SoundManager.instance.PlaySingle(SoundManager.instance.musicSource.clip);
		StopCoroutine(printSound());

	}
}
