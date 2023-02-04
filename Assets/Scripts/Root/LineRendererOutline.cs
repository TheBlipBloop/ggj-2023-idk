using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererOutline : MonoBehaviour
{
	[SerializeField]
	protected LineRenderer target;

	[SerializeField]
	protected LineRenderer outline;

	[SerializeField]
	protected float outlineWidth = 0.4f;

	protected Vector3[] positionPool = new Vector3[256];
	// protected int positions

	// Start is called before the first frame update
	void Start()
	{
		outline.widthCurve = target.widthCurve;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		int positions = target.positionCount;

		outline.positionCount = positions;

		if (positions >= positionPool.Length)
		{
			ExpandPositionPool();
		}

		outline.widthMultiplier = target.widthMultiplier + outlineWidth;

		target.GetPositions(positionPool);
		outline.SetPositions(positionPool);
	}

	private void ExpandPositionPool()
	{
		Vector3[] newPositionPool = new Vector3[positionPool.Length * 2];
		for (int i = 0; i < positionPool.Length; i++)
		{
			newPositionPool[i] = positionPool[i];
		}

		positionPool = newPositionPool;
	}
}
