using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootRenderer : MonoBehaviour
{
	[SerializeField]
	protected LineRenderer rootRenderer;

	[SerializeField]
	protected float thickness = 1;

	[SerializeField]
	protected float thicknessPulseMagnitude = 0.2f;

	[SerializeField]
	protected float thicknessPulseSpeed = 5f;

	Vector3 lastAddPosition = Vector3.negativeInfinity;

	Vector3 lastColliderSpawnPosition = Vector3.negativeInfinity;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		rootRenderer.widthMultiplier = thickness + Mathf.Sin(Time.time / thicknessPulseSpeed * Mathf.PI) * thicknessPulseMagnitude;

		if (Vector2.Distance(transform.position, lastAddPosition) > 0.5f)
		{
			rootRenderer.positionCount++;
			rootRenderer.SetPosition(rootRenderer.positionCount - 1, transform.position);

			lastAddPosition = transform.position;
		}
	}
}
