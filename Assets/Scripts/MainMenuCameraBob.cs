using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraBob : MonoBehaviour
{
	[SerializeField]
	protected float bobMagnitude = 3f;

	[SerializeField]
	protected float bobFrequency = 10f;

	protected Vector3 startPosition;

	// Start is called before the first frame update
	void Start()
	{
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time / bobFrequency * Mathf.PI) * bobMagnitude;
	}
}
