using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
	public Root root;

	[SerializeField]
	protected SpriteRenderer drillRenderer;

	[SerializeField]
	protected Sprite[] drillSprites;

	[SerializeField]
	protected float drillFrameInterval = 0.1f;

	private float nextDrillFrameTime;

	private int drillSpriteIndex = 0;

	void Update()
	{
		if (Time.time > nextDrillFrameTime)
		{
			drillRenderer.sprite = drillSprites[drillSpriteIndex];

			drillSpriteIndex = (drillSpriteIndex + 1) % drillSprites.Length;
			nextDrillFrameTime = Time.time + drillFrameInterval;
		}
	}

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
