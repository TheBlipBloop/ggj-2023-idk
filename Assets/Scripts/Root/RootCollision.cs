using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCollision : MonoBehaviour
{
	[SerializeField]
	protected Root root;

	[SerializeField]
	protected LayerMask collisionLayers;

	[SerializeField]
	protected float maxCollisionDistance = 5f;

	[SerializeField]
	protected int collisionSegmentInterval = 5;

	[SerializeField]
	protected int maxCollisionEventsPerFrame = 3;

	[SerializeField]
	protected int maxCollisionSegments = 256;

	[SerializeField]
	protected float collisionThickness = 0.5f;

	[SerializeField]
	protected bool debugCollision = true;

	protected Vector2[] collisionPositions;

	protected int collisionPositionsLength = 0;


	void Awake()
	{
		collisionPositions = new Vector2[maxCollisionSegments];
	}

	// Update is called once per frame
	void Update()
	{
		collisionPositionsLength = root.GetRootPositionsNoAlloc(ref collisionPositions, maxCollisionDistance);

		int collisionEventsGenerated = 0;
		for (int i = collisionPositionsLength - 1; i >= collisionSegmentInterval; i -= collisionSegmentInterval)
		{
			Vector2 start = collisionPositions[i];
			Vector2 end = collisionPositions[i - collisionSegmentInterval];
			float dist = (end - start).magnitude;

			if (dist > root.GetSegmentDistance() * collisionSegmentInterval * 1.5f)
			{
				continue;
			}

			RaycastHit2D hit = Physics2D.CircleCast(start, collisionThickness, end - start, dist, collisionLayers.value);
			if (hit.collider)
			{
				root.OnCollideWith(hit);
				collisionEventsGenerated++;
			}

			if (collisionEventsGenerated >= maxCollisionEventsPerFrame)
			{
				break;
			}

			if (debugCollision)
			{
				Color debugColor = Color.yellow;

				if (hit.collider)
				{
					debugColor = Color.magenta;
				}

				Debug.DrawLine(start, end, debugColor);
			}
		}
	}
}
