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

	// Start is called before the first frame update
	void Start()
	{
		outline.widthCurve = target.widthCurve;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		outline.positionCount = target.positionCount;
		Vector3[] positions = new Vector3[target.positionCount];

		outline.widthMultiplier = target.widthMultiplier + outlineWidth;

		target.GetPositions(positions);
		outline.SetPositions(positions);
	}
}
