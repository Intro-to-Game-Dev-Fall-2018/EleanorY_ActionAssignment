using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {

	public float Speed;
	public float HoopSpeed;
//	[SerializeField] float _frequency = 10f;
//	[SerializeField] float _magnitude = 0.3f;
	public float InitialX;
	private float _initialY;

	private Vector3 _pos;

	private float _timer;
	private float _animationTimer;
	[SerializeField] float _hoopTime = .3f;
	[SerializeField] float _hoopInterval = 1f;
	private Animator _animator;
	
	
	// Use this for initialization
	void Start ()
	{
		_initialY = transform.position.y;
		_animator = GetComponent<Animator>();
		_animationTimer = 1.0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		_timer += Time.deltaTime;
		_animationTimer += Time.deltaTime;
		if (_timer <= _hoopTime)
		{
			transform.position = new Vector3
			(
				transform.position.x + Time.deltaTime * Speed,
				transform.position.y + Time.deltaTime * HoopSpeed,
				transform.position.z
			);
		}
		else if (_timer > _hoopTime && _timer <= 2 * _hoopTime )
		{
			transform.position = new Vector3
			(
				transform.position.x + Time.deltaTime * Speed,
				transform.position.y - Time.deltaTime * HoopSpeed,
				transform.position.z
			);
		}
		else if (_timer > 2 * _hoopTime + _hoopInterval)
		{
			_timer = 0.00f;
		}
		if (_timer <= 2 * _hoopTime + _hoopInterval && _timer > 2 * _hoopTime)
		{
			transform.position = new Vector3
			(
				transform.position.x,
				_initialY,
				transform.position.z
			);
		}

		if (_animationTimer >= 1.0f)
		{
			_animator.SetBool("GetHurt", false);
		}

		if (Input.GetKeyDown("2"))
		{
			_animator.SetBool("Start", true);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("edge");
		if (other.CompareTag("Background"))
		{
			transform.position = new Vector3(
				InitialX,
				transform.position.y,
				transform.position.z);
		}
		if (other.CompareTag("Player"))
		{
			_animator.SetBool("GetHurt", true);
			_animationTimer = 0.0f;
		}

	}
	
}
