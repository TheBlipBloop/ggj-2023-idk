using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCollision : MonoBehaviour
{
	public GameObject nutrientDrop;

	public void Die()
	{
		Instantiate(nutrientDrop, this.transform.position, Quaternion.identity);
		GameObject.Destroy(this.gameObject);
	}
}
