using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private CarController[] _carScripts;
	private GameObject[] _cars;
	private Vector3[] _carsInitialTransform;

	public GameObject LevelText;
	public Text Score1;
	public Text Score2;
	private int _score1;
	private int _score2;

	public Text WinText;
	
	
	private bool _gameStart;

	public GameObject Player1;
	public GameObject Player2;
	

	private AudioSource _mainAudio;
	public AudioClip Bgm;
	public AudioClip SwapScene;

	private float _gameTimer;
	public float GameLength;
	public Text GameTimer;

	private Vector3 _player1Position;
	private Vector3 _player2Position;

	public AudioClip WinFx;
	private bool _winFxPlayed;
	
	
	// Use this for initialization
	void Start ()
	{
		_gameStart = false;
		Score1.enabled = false;
		Score2.enabled = false;
		WinText.enabled = false;
		_cars = GameObject.FindGameObjectsWithTag("Car");
		_carScripts = new CarController[_cars.Length];
		_carsInitialTransform = new Vector3[_cars.Length];
		for(var i = 0; i < _cars.Length; i++)
		{
			_carsInitialTransform[i] = _cars[i].GetComponent<Transform>().position;
		}

		_player1Position = Player1.transform.position;
		_player2Position = Player2.transform.position;
		_mainAudio = GetComponent<AudioSource>();
		_mainAudio.PlayOneShot(SwapScene);
		_gameTimer = GameLength;

	}

	
	// Update is called once per frame
	void Update ()
	{
		GameTimer.text = _gameTimer.ToString("F1");
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
			_gameStart = true;
			LevelText.SetActive(false);
			Score1.enabled = true;
			Score1.text = "0";
			Score2.enabled = true;
			Score2.text = "0";
			Player1.SetActive(true);
			Player2.SetActive(true);
			Player1.transform.position = _player1Position;
			Player2.transform.position = _player2Position;
			for (var i = 0; i < _cars.Length; i++)
			{
				_carScripts[i] = _cars[i].GetComponent<CarController>();
				_carScripts[i].enabled = true;
			}
			for (var i = 0; i < _cars.Length; i++)
			{
				_cars[i].transform.position = _carsInitialTransform[i];
			}
			WinText.enabled = false;
			_gameTimer = GameLength;
			_mainAudio.clip = Bgm;
			_mainAudio.Play();
			_mainAudio.loop = true;
			_winFxPlayed = false;
		}

		if (_gameStart)
		{
			_gameTimer -= Time.deltaTime;
		}

		if (_gameTimer <= 0)
		{
			_gameStart = false;
			_gameTimer = 0.0f;
			Player1.SetActive(false);
			Player2.SetActive(false);
			int.TryParse(Score1.text, out _score1);
			int.TryParse(Score2.text, out _score2);
			if (_score1 > _score2)
			{
				WinText.text = "Player 1 Win!";
			} 
			else if (_score1 == _score2)
			{
				WinText.text = "Draw!";
			}
			else
			{
				WinText.text = "Player 2 Win!";
			}
			WinText.enabled = true;
			if (!_winFxPlayed)
			{
				_mainAudio.loop = false;
				_mainAudio.Stop();
				_mainAudio.PlayOneShot(WinFx);
				_winFxPlayed = true;
			}
			
		}
	}
}
