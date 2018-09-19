using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public KeyCode Up;
	public KeyCode Down;
	public float speed;
	public float backupDistance;
	private float BackUpAccumulate;

	private float initialY;
	private int count;

	public Text scoreText;
	private bool getHurt;
	private float crashPositionY;

	private float timer;
	private bool GameStart = false;


	private Animator PlayerAnimator;
	
	
	
	// Use this for initialization
	void Start ()
	{
		initialY = -4.9f;
		count = 0;
		timer = 2.0f;
		PlayerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(timer);
		if (Input.GetKeyUp("2"))
		{
			GameStart = true;
			PlayerAnimator.SetTrigger("Start");

		}
		if (GameStart == true)
		{
			if (timer < 2.0f)
			{
				timer += Time.deltaTime;
			}
			else
			{
				if (!getHurt && timer >= 2.0f)
				{
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
			crashPositionY = transform.position.y;
			getHurt = true;
			PlayerAnimator.SetBool("GetHurt", true);
			BackUpAccumulate += backupDistance;
		}

	}

	private void Backwards()
	{
		if (transform.position.y > initialY)
		{
			if (transform.position.y > crashPositionY - BackUpAccumulate)
			{
				transform.position = new Vector3(
					transform.position.x,
					transform.position.y - speed * Time.deltaTime,
					transform.position.z
				);
			} else 
			{
				getHurt = false;
				PlayerAnimator.SetBool("GetHurt", false);
				BackUpAccumulate = 0;
				timer = 1.5f;
			}
		}
		else
		{
			transform.position = new Vector3(
				transform.position.x,
				initialY,
				transform.position.z);
			getHurt = false;
			PlayerAnimator.SetBool("GetHurt", false);
			BackUpAccumulate = 0;
			timer = 1.5f;
		}
	}
}
