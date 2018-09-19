using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private CarController[] carScripts;
	private GameObject[] cars;

	public GameObject LevelText;
	public GameObject Score_1;
	public GameObject Score_2;
	
	// Use this for initialization
	void Start ()
	{
		cars = GameObject.FindGameObjectsWithTag("Car");
		carScripts = new CarController[cars.Length];
	
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("1"))
		{
		
			if (SceneManager.GetActiveScene().buildIndex + 1 < 
			    SceneManager.sceneCountInBuildSettings)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex + 1);
			} else
			{
				SceneManager.LoadScene(0);
			}
				
		}
		if (Input.GetKeyUp("2"))
		{
			LevelText.SetActive(false);
			Score_1.SetActive(true);
			Score_2.SetActive(true);
			for(int i = 0; i < cars.Length; i++)
			{
				carScripts[i] = cars[i].GetComponent<CarController>();
				carScripts[i].enabled = true;
			}
		}
	}
}
