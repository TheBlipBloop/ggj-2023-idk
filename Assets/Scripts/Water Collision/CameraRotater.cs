using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
	public float rotateSpeed;
	public GameObject levelSelectButton;
	private float time;
	private bool loadedNextLevel = false;
	// Start is called before the first frame update
	void Start()
	{
		time = 90 / rotateSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		if (time > 0)
		{
			this.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
			time -= Time.deltaTime;
		}
		else
		{
			if (!loadedNextLevel)
			{
				loadedNextLevel = true;
			}
			this.enabled = false;
		}
	}
}
