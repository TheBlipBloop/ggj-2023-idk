using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class RootRenderer : MonoBehaviour
{
	[SerializeField]
	protected TrailRenderer rootRenderer;

	[SerializeField]
	protected float thickness = 1;

	[SerializeField]
	protected float thicknessPulseMagnitude = 0.2f;

	[SerializeField]
	protected float thicknessPulseSpeed = 5f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		rootRenderer.widthMultiplier = thickness + Mathf.Sin(Time.time / thicknessPulseSpeed * Mathf.PI) * thicknessPulseMagnitude;
	}
}
