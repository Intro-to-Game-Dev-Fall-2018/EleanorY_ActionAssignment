using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CarController : MonoBehaviour
{
	public float speed;
	private bool gameStart;

	public float InitialX;
	
	
	// Use this for initialization

	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(
			transform.position.x  + speed * Time.deltaTime,
			transform.position.y,
			transform.position.z
		);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Background"))
		{
//			Destroy(gameObject);
			transform.position = new Vector3(
				InitialX,
				transform.position.y,
				transform.position.z);
		}
	}
}
