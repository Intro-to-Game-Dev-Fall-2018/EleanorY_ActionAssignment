using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private CarController[] carScripts;
	private GameObject[] cars;
	private Vector3[] carsInitialTransform;

	public GameObject LevelText;
	public GameObject Score_1;
	public GameObject Score_2;
	private bool GameStart;
	

	private AudioSource MainAudio;
	public AudioClip Bgm;
	public AudioClip SwapScene;
	
	
	// Use this for initialization
	void Start ()
	{
		GameStart = false;
		cars = GameObject.FindGameObjectsWithTag("Car");
		carScripts = new CarController[cars.Length];
		carsInitialTransform = new Vector3[cars.Length];
		for(int i = 0; i < cars.Length; i++)
		{
			carsInitialTransform[i] = cars[i].GetComponent<Transform>().position;
		}
		MainAudio = GetComponent<AudioSource>();
		MainAudio.clip = SwapScene;
		MainAudio.Play();

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
			GameStart = true;
			LevelText.SetActive(false);
			Score_1.SetActive(true);
			Score_2.SetActive(true);
			for(int i = 0; i < cars.Length; i++)
			{
				carScripts[i] = cars[i].GetComponent<CarController>();
				carScripts[i].enabled = true;
			}
			MainAudio.clip = Bgm;
			MainAudio.Play();
			MainAudio.loop = true;
		}
		if (GameStart && Input.GetKeyUp("2"))
		{
			for(int i = 0; i < cars.Length; i++)
			{
				cars[i].transform.position = carsInitialTransform[i];
			}
			MainAudio.clip = Bgm;
			MainAudio.Play();
			MainAudio.loop = true;
		}
	}
}
