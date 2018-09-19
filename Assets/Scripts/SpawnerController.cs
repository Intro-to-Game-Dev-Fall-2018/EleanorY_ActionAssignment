using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{


	public GameObject CarPrefab;
	public GameObject[] Cars;

	public Vector3 InitialPos;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < Cars.Length; i++)
		{
			if (Cars[i] == null)
			{
				Cars[i] = Instantiate(CarPrefab, InitialPos, Quaternion.identity) as GameObject;
			}
		}
	}
}
