using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	[SerializeField]

	protected float nutrientValue = 1f;

	private void OnCollisionEnter2D(Collision2D collider)
	{
		Root root = collider.gameObject.GetComponent<Root>();
		if (root)
		{
			root.AddNutrients(nutrientValue);
			Destroy(gameObject);
		}
	}
}
