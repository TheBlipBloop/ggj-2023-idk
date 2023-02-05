using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterCollision : MonoBehaviour
{
	public string nextLevel = "Root 1";

	public GameObject fadeToBlackPrefab;

	public Root root;
	public Camera mainCam;
	public int curLevel;
	private Vector3 startTrans;
	// Start is called before the first frame update
	void Start()
	{
		root = FindObjectOfType<Root>();
		mainCam = Camera.main;

		if (root)
		{
			startTrans = root.transform.position;
		}
	}

	// Update is called once per frame
	void Update()
	{
		// awful hack for intro sequence
		if (root == null)
		{
			root = FindObjectOfType<Root>();
		}
		else
		{
			startTrans = root.transform.position;
		}

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.GetComponent<Root>() && collider.gameObject.GetComponent<Root>().IsAlive())
		{
			root.enabled = false;
			mainCam.GetComponent<RootCamera>().enabled = false;
			Instantiate(fadeToBlackPrefab);
			mainCam.GetComponent<CameraReturnToPlant>().enabled = true;
			mainCam.GetComponent<CameraReturnToPlant>().moveToward(startTrans);
			Invoke("LoadNextScene", 3f);
			// PlayerPrefs.SetInt("Level " + (curLevel + 1), 1); // This line of code crashes unity I have no idea how or why
		}
	}
	void LoadNextScene()
	{
		SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Single);
	}
}
