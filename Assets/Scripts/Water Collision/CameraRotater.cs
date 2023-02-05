using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
				Invoke("LoadNextScene", 3f);
				loadedNextLevel = true;
			}
			this.enabled = false;
		}
	}

	void LoadNextScene()
	{
		SceneManager.LoadSceneAsync(FindObjectOfType<WaterCollision>().nextLevel, LoadSceneMode.Single);
	}
}
