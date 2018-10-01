using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour {

public KeyCode Up;
	public KeyCode Down;
	public float speed;

	private float initialY;
	private int count;

	public Text scoreText;
	private float crashPositionY;

	private float timer;
	private bool GameStart = false;


	private Animator PlayerAnimator;
	private AudioSource PlayerAudioSource;
	public AudioClip ScoreSE;
	public AudioClip HurtSE;
	
	
	// Use this for initialization
	void Start ()
	{
		initialY = -4.9f;
		count = 0;
		timer = 2.0f;
		PlayerAnimator = GetComponent<Animator>();
		PlayerAudioSource = GetComponent<AudioSource>();
		PlayerAudioSource.clip = HurtSE;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameStart && Input.GetKeyUp("2"))
		{
			GameStart = true;
			PlayerAnimator.SetTrigger("Start");
		}
		
		if (GameStart && Input.GetKeyUp("2"))
		{
			transform.position = new Vector3(
				transform.position.x,
				initialY,
				transform.position.z
			);
			count = 0;
			scoreText.text = count.ToString();
			timer = 2.0f;
			PlayerAnimator.SetBool("GetHurt", false);
		}
		
		if (GameStart)
		{
			if (timer < 2.0f)
			{
				timer += Time.deltaTime;
			}
			else
			{
				if (timer >= 2.0f)
				{
					PlayerAnimator.SetBool("GetHurt", false);
					if (Input.GetKey(Up))
					{
						transform.position = new Vector3(
							transform.position.x,
							transform.position.y + speed * Time.deltaTime,
							transform.position.z
						);
					} else if (Input.GetKey(Down) && transform.position.y >= initialY)
					{
						transform.position = new Vector3(
							transform.position.x,
							transform.position.y - speed * Time.deltaTime,
							transform.position.z
						);
					}
				}
			}
			
		}
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("Background"))
		{
			PlayerAudioSource.clip = ScoreSE;
			PlayerAudioSource.Play();
			transform.position = new Vector3(
				transform.position.x,
				initialY,
				transform.position.z
			);
			count++;
			scoreText.text = count.ToString();
			timer = 0.0f;
		}
		if (other.CompareTag("Car"))
		{
			transform.position = new Vector3(
				transform.position.x,
				initialY,
				transform.position.z
			);
			timer = 1.5f;
			PlayerAudioSource.clip = HurtSE;
			PlayerAudioSource.Play();
			PlayerAnimator.SetBool("GetHurt", true);
		}

	}
}

