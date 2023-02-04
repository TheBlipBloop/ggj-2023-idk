using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : Pickup
{
	[SerializeField]
	protected PowerupType powerup;

	[SerializeField]
	protected float powerupDuration = 15f;

	protected override void CollectPickup(Root root)
	{
		root.ApplyPowerup(powerup, powerupDuration);
	}
}
