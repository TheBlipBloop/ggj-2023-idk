using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

	private void OnCollisionEnter2D(Collision2D collider)
	{
		Root root = collider.gameObject.GetComponent<Root>();
		if (root)
		{
			CollectPickup(root);
			Destroy(gameObject);
		}
	}

	protected virtual void CollectPickup(Root root)
	{
		// Implement me in a subclass!
	}
}
