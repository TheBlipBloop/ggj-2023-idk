using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
	public Root root;

	private void OnCollisionEnter2D(Collision2D collider)
	{
		Drillable drillable = collider.gameObject.GetComponent<Drillable>();

		if (drillable)
		{
			root.Damage(15);
			drillable.Drill();
		}

		WormCollision worm = collider.gameObject.GetComponent<WormCollision>();

		if (worm)
        {
			worm.Die();
		}
	}

}
