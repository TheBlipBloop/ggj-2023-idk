using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCollision : MonoBehaviour
{
	[SerializeField]
	protected Root root;

	[SerializeField]
	protected float maxCollisionDistance = 5f;

	[SerializeField]
	protected int collisionSegmentInterval = 5;

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

		for (int i = collisionPositionsLength - 1; i >= collisionSegmentInterval; i -= collisionSegmentInterval)
		{
			Vector2 start = collisionPositions[i];
			Vector2 end = collisionPositions[i - collisionSegmentInterval];
			float dist = (end - start).magnitude;

			if (dist > root.GetSegmentDistance() * collisionSegmentInterval * 1.5f)
			{
				continue;
			}

			RaycastHit2D hit = Physics2D.CircleCast(start, collisionThickness, end - start, dist);

			if (debugCollision)
			{
				Color debugColor = Color.black;

				if (hit.collider)
				{
					debugColor = Color.magenta;
				}

				Debug.DrawLine(start, end, debugColor);
			}
		}
	}
}
