using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public bool Player1;
	
	public KeyCode Up;
	public KeyCode Down;
	public float Speed;
	public float BackupDistance;
	private float _backUpAccumulate;
	
	

	private float _initialY;
	private int _count;

	public Text Count;
	private bool _getHurt;
	private float _crashPositionY;

	private float _timer;
	private float _gameTimer;
	private bool _gameStart = false;


	private AudioSource _playerAudioSource;
	public AudioClip ScoreFx;
	public AudioClip HurtFx;
	
	
	// Use this for initialization
	void Start ()
	{
		_initialY = transform.position.y;
		_count = 0;
		_timer = 2.0f;
		_playerAudioSource = GetComponent<AudioSource>();
		_playerAudioSource.clip = HurtFx;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_gameStart && Input.GetKeyUp("2"))
		{
			_gameStart = true;
		}
		
		if (_gameStart && Input.GetKeyUp("2"))
		{
			transform.position = new Vector3(
				transform.position.x,
				_initialY,
				transform.position.z
			);
			_count = 0;
			Count.text = _count.ToString();
			_timer = 2.0f;
			_getHurt = false;
		}
		
		
		if (_gameStart)
		{
			if (_timer < 2.0f)
			{
				_timer += Time.deltaTime;
			}
			else
			{
				if (!_getHurt && _timer >= 2.0f)
				{
					if (Input.GetKey(Up))
					{
						transform.position = new Vector3(
							transform.position.x,
							transform.position.y + Speed * Time.deltaTime,
							transform.position.z
						);
					} else if (Input.GetKey(Down) && transform.position.y >= _initialY)
					{
						transform.position = new Vector3(
							transform.position.x,
							transform.position.y - Speed * Time.deltaTime,
							transform.position.z
						);
					}
				}
				else
				{
					Backwards();
				}
			}
		}
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("Background"))
		{
			_playerAudioSource.PlayOneShot(ScoreFx);
			transform.position = new Vector3(
				transform.position.x,
				_initialY,
				transform.position.z
			);
			_count++;
			Count.text = _count.ToString();
			_timer = 0.0f;
		}
		if (other.CompareTag("Frog"))
		{
			if (Player1)
			{
				_crashPositionY = transform.position.y;
				_getHurt = true;
				_backUpAccumulate += BackupDistance;
			}
			else
			{
				transform.position = new Vector3(
					transform.position.x,
					_initialY,
					transform.position.z
				);
				_timer = 1.5f;
			}
			_playerAudioSource.PlayOneShot(HurtFx);
		}

	}

	private void Backwards()
	{
		if (transform.position.y > _initialY)
		{
			if (transform.position.y > _crashPositionY - _backUpAccumulate)
			{
				transform.position = new Vector3(
					transform.position.x,
					transform.position.y - Speed * Time.deltaTime,
					transform.position.z
				);
			} else 
			{
				_getHurt = false;
				_backUpAccumulate = 0;
				_timer = 1.5f;
			}
		}
		else
		{
			transform.position = new Vector3(
				transform.position.x,
				_initialY,
				transform.position.z);
			_getHurt = false;
			_backUpAccumulate = 0;
			_timer = 1.5f;
		}
	}
}
