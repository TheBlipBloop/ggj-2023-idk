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

	[SerializeField]
	protected float nutrientLowPct = 0.2f;

	[SerializeField]
	protected Image nutrientLowIndicator;

	protected Color nutrientLowIndicatorStartColor;


	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<Root>();
		nutrientLowIndicatorStartColor = nutrientLowIndicator.color;
		nutrientLowIndicator.enabled = true;
		nutrientLowIndicator.color = Color.clear;
	}

	// Update is called once per frame
	void Update()
	{
		// awful hack for intro sequence
		if (player == null)
		{
			player = FindObjectOfType<Root>();
			return;
		}

		healthIndicator.fillAmount = player.GetHealthPct();
		nutrientIndicator.fillAmount = player.GetNutrientsPct();

		if (player.GetNutrientsPct() < nutrientLowPct)
		{
			nutrientLowIndicator.color = Color.Lerp(nutrientLowIndicator.color, nutrientLowIndicatorStartColor, Time.deltaTime);
		}
		else
		{
			Color colorInvis = new Color(nutrientLowIndicatorStartColor.r, nutrientLowIndicatorStartColor.g, nutrientLowIndicatorStartColor.b, 0f);
			nutrientLowIndicator.color = Color.Lerp(nutrientLowIndicator.color, colorInvis, Time.deltaTime * 3f);

		}

		if (player.IsDead())
		{
			deathHUD.SetActive(true);
		}
	}
}
