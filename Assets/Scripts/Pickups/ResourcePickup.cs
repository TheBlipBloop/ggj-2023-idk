using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : Pickup
{
	[SerializeField]
	protected float nutrientValue = 1f;

	[SerializeField]
	protected float healthValue = 1f;

	protected override void CollectPickup(Root root)
	{
		root.AddNutrients(nutrientValue);
		root.Heal(healthValue);
	}
}
