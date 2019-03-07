using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetUnit : SetScript
{
	public GameObject increasePrefab;
	public GameObject increaseEffect;

	public GameObject decreasePrefab;
	public GameObject decreaseEffect;
	void Update () {
    }

	public void SaberIncrease()
	{
		if (SceneSwitch.stage < 8)
		{
			if (SBattleManager.Instance.SaberCount < 25 && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(-0.147f, -0.377f, 10.089f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.SaberCount++;
			}
			Debug.Log(SBattleManager.Instance.SaberCount);
		}
		else if (SceneSwitch.stage >= 8)
		{
			if (SBattleManager.Instance.SaberCount < JsonGrid.NumberOfBlacksmith*7 && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(-0.147f, -0.377f, 10.089f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.SaberCount++;
			}
		}
	}
    public void SaberDecrease()
    {
        if (SBattleManager.Instance.SaberCount > 0 )
        {
        SResource.Instance.Human += 100;
			decreaseEffect = (GameObject)Instantiate(decreasePrefab, new Vector3(-0.147f, -0.581f, 10.0f), Quaternion.identity);
			//decreaseEffect = (GameObject)Instantiate(decreasePrefab, transform.position, Quaternion.identity);
			Destroy(decreaseEffect, decreaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
		DecreaseUnit();
        SBattleManager.Instance.SaberCount--;
        }
        Debug.Log(SBattleManager.Instance.SaberCount);
    }



	public void LancerIncrease()
	{
		if (SceneSwitch.stage < 8)
		{
			if (SBattleManager.Instance.LancerCount < 5 && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(0.805f, -0.383f, 10.0f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.LancerCount++;
				Debug.Log(SBattleManager.Instance.LancerCount);
			}
		}
		else if (SceneSwitch.stage >= 8)
		{
			if (SBattleManager.Instance.LancerCount < (JsonGrid.NumberOfBlacksmith * 2) && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(0.805f, -0.383f, 10.0f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.LancerCount++;
				Debug.Log(SBattleManager.Instance.LancerCount);
			}
		}
	}
    public void LancerDecrease()
    {
        if (SBattleManager.Instance.LancerCount > 0)
        {
            SResource.Instance.Human += 100;
			decreaseEffect = (GameObject)Instantiate(decreasePrefab, new Vector3(0.805f, -0.591f, 9.8f), Quaternion.identity);
			Destroy(decreaseEffect, decreaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
			DecreaseUnit();
            SBattleManager.Instance.LancerCount--;
            Debug.Log(SBattleManager.Instance.LancerCount);
        }
    }

    public void ArcherIncrease()
    {
		if (SceneSwitch.stage < 8)
		{
			if (SBattleManager.Instance.ArcherCount < 5 && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(-0.157f, -0.855f, 10.0f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.ArcherCount++;
				Debug.Log(SBattleManager.Instance.ArcherCount);
			}
		}
		else if(SceneSwitch.stage >= 8)
		{
			if (SBattleManager.Instance.ArcherCount < (JsonGrid.NumberOfBlacksmith*2) && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(-0.157f, -0.855f, 10.0f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.ArcherCount++;
				Debug.Log(SBattleManager.Instance.ArcherCount);
			}
		}
    }

    public void ArcherDecrease()
    {
        if (SBattleManager.Instance.ArcherCount > 0)
        {
            SResource.Instance.Human += 100;
			decreaseEffect = (GameObject)Instantiate(decreasePrefab, new Vector3(0, -1.05f, 10), Quaternion.identity);
			Destroy(decreaseEffect, decreaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
			DecreaseUnit();
            SBattleManager.Instance.ArcherCount--;
            Debug.Log(SBattleManager.Instance.ArcherCount);
        }
    }

	public void RiderIncrease()
	{
		if (SceneSwitch.stage < 8)
		{
			if (SBattleManager.Instance.RiderCount < 5 && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(0.814f, -0.866f, 10.0f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.RiderCount++;
				Debug.Log(SBattleManager.Instance.RiderCount);
			}
		}
		else if (SceneSwitch.stage >= 8)
		{
			if (SBattleManager.Instance.RiderCount < (JsonGrid.NumberOfFarm * 2) && SResource.Instance.Human >= 100)
			{
				SResource.Instance.Human -= 100;
				increaseEffect = (GameObject)Instantiate(increasePrefab, new Vector3(0.814f, -0.866f, 10.0f), Quaternion.identity);
				Destroy(increaseEffect, increaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
				IncreaseUnit();
				SBattleManager.Instance.RiderCount++;
				Debug.Log(SBattleManager.Instance.RiderCount);
			}
		}
	}
    public void RiderDecrease()
    {
        if (SBattleManager.Instance.RiderCount > 0)
        {
            SResource.Instance.Human += 100;
			decreaseEffect = (GameObject)Instantiate(decreasePrefab, new Vector3(0.814f, -1.1f, 9.8f), Quaternion.identity);
			Destroy(decreaseEffect, decreaseEffect.GetComponent<ParticleSystem>().duration + 0.1f);
			DecreaseUnit();
            SBattleManager.Instance.RiderCount--;
            Debug.Log(SBattleManager.Instance.RiderCount);
        }
    }

    public void VeteranIncrease()
    {
        if (SBattleManager.Instance.VeteranCount < 5 && SResource.Instance.Human >= 100)
        {
            SResource.Instance.Human -= 100;
            IncreaseUnit();
            SBattleManager.Instance.VeteranCount++;
            Debug.Log(SBattleManager.Instance.VeteranCount);
        }
    }

    public void VeteranDecrease()
    {
        if (SBattleManager.Instance.VeteranCount > 0)
        {
            SResource.Instance.Human += 100;
            DecreaseUnit();
            SBattleManager.Instance.VeteranCount--;
            Debug.Log(SBattleManager.Instance.VeteranCount);
        }
    }

    public void WeaponIncrease()
    {
        if(SBattleManager.Instance.WeaponCount<5 && SResource.Instance.Human >= 100)
        {
            SResource.Instance.Human -= 100;
            IncreaseUnit();
            SBattleManager.Instance.WeaponCount++;
            Debug.Log(SBattleManager.Instance.WeaponCount);
        }
    }

    public void WeaponDecrease()
    {
        if(SBattleManager.Instance.WeaponCount>0)
        {
            SResource.Instance.Human += 100;
            DecreaseUnit();
            SBattleManager.Instance.WeaponCount--;
            Debug.Log(SBattleManager.Instance.WeaponCount);
        }
    }
}
