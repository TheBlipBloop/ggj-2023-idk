using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCollision : MonoBehaviour
{
	public GameObject nutrientDrop;

	[SerializeField]
	protected TrailRenderer wormRenderer;

	[SerializeField]
	protected BoxCollider2D wormCollider;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// wormCollider.transform.position = wormRenderer.bounds.center;
		// wormCollider.size = wormRenderer.bounds.size;
	}

	public void Die()
    {
		Instantiate(nutrientDrop, this.transform.position, Quaternion.identity);
		GameObject.Destroy(this.gameObject);
	}
}
