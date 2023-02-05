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

	protected Color originalColor;

	protected Color targetColor;

	protected Color smoothedColor;

	// Start is called before the first frame update
	void Start()
	{
		smoothedThickness = thickness;
		originalColor = rootRenderer.endColor;

		targetColor = originalColor;
		smoothedColor = targetColor;
	}

	// Update is called once per frame
	void Update()
	{
		smoothedThickness = Mathf.Lerp(smoothedThickness, thickness, Time.deltaTime);
		smoothedColor = Color.Lerp(smoothedColor, targetColor, Time.deltaTime * 3f);

		rootRenderer.widthMultiplier = smoothedThickness + Mathf.Sin(Time.time / thicknessPulseSpeed * Mathf.PI) * thicknessPulseMagnitude;
		rootRenderer.endColor = smoothedColor;
	}

	public void SetThickness(float newThickness)
	{
		thickness = newThickness;
	}

	public void SetColor(Color newColor)
	{
		targetColor = newColor;
	}

	public void ResetColor()
	{
		targetColor = originalColor;
	}
}
