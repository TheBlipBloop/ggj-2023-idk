using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : Pickup
{
	[SerializeField]
	protected PowerupType powerup;

	protected override void CollectPickup(Root root)
	{
		// powerup = PowerupType
	}
}
