using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraBob : MonoBehaviour
{
	[SerializeField]
	protected float bobMagnitude = 3f;

	[SerializeField]
	protected float bobFrequency = 10f;

	[SerializeField]
	protected Vector3 targetPosition = new Vector3(0, 0, -10);

	protected Vector3 startPosition;

	// Start is called before the first frame update
	void Start()
	{
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		targetPosition = startPosition + Vector3.up * Mathf.Sin(Time.time / bobFrequency * Mathf.PI) * bobMagnitude;
		targetPosition.z = -10f;
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 3f);
	}
}
