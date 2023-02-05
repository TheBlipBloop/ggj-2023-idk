using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
	public string nextLevel = "Root 1";

	public GameObject root;
	public Camera mainCam;
	public int curLevel;
	private Vector3 startTrans;
	// Start is called before the first frame update
	void Start()
	{
		root = FindObjectOfType<Root>().gameObject;
		mainCam = Camera.main;

		startTrans = root.transform.position;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.GetComponent<Root>() && collider.gameObject.GetComponent<Root>().IsAlive())
		{
			root.GetComponent<Root>().enabled = false;
			mainCam.GetComponent<RootCamera>().enabled = false;
			mainCam.GetComponent<CameraReturnToPlant>().enabled = true;
			mainCam.GetComponent<CameraReturnToPlant>().moveToward(startTrans);
			// PlayerPrefs.SetInt("Level " + (curLevel + 1), 1); // This line of code crashes unity I have no idea how or why
		}
	}
}
