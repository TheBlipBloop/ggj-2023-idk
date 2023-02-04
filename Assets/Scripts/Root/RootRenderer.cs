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

	private float smoothedThickness = 1;

	[SerializeField]
	protected float thicknessPulseMagnitude = 0.2f;

	[SerializeField]
	protected float thicknessPulseSpeed = 5f;

	// Start is called before the first frame update
	void Start()
	{
		smoothedThickness = thickness;
	}

	// Update is called once per frame
	void Update()
	{
		smoothedThickness = Mathf.Lerp(smoothedThickness, thickness, Time.deltaTime);
		rootRenderer.widthMultiplier = smoothedThickness + Mathf.Sin(Time.time / thicknessPulseSpeed * Mathf.PI) * thicknessPulseMagnitude;
	}

	public void SetThickness(float newThickness)
	{
		thickness = newThickness;
	}
}
