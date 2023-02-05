using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
	public List<Button> levelButtons;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		foreach (Button button in levelButtons)
		{
			if (!PlayerPrefs.HasKey(button.gameObject.name))
			{
				button.interactable = false;
			}
		}
	}
}
