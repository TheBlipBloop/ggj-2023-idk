using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
	[SerializeReference]
	protected Root player;

	[SerializeField]
	protected GameObject deathHUD;


	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<Root>();
	}

	// Update is called once per frame
	void Update()
	{
		if (player.IsDead())
		{
			deathHUD.SetActive(true);
		}
	}
}
