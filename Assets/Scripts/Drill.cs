using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{


	private void OnCollisionEnter2D(Collision2D collider)
	{
		Drillable drillable = collider.gameObject.GetComponent<Drillable>();

		if (drillable)
		{
			drillable.Drill();
		}
	}

}
