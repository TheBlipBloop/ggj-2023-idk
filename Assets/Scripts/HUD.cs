using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeReference]
	protected Root player;

	[SerializeField]
	protected GameObject deathHUD;

	[SerializeField]
	protected Image healthIndicator;

	[SerializeField]
	protected Image nutrientIndicator;


	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<Root>();
	}

	// Update is called once per frame
	void Update()
	{
		healthIndicator.fillAmount = player.GetHealthPct();
		nutrientIndicator.fillAmount = player.GetNutrientsPct();

		if (player.IsDead())
		{
			deathHUD.SetActive(true);
		}
	}
}
