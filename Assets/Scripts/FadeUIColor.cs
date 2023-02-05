using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIColor : MonoBehaviour
{
	public Image image;
	public Color endColor = Color.black;
	public float fadeSpeed = 2;

	// Update is called once per frame
	void Update()
	{
		image.color = Color.Lerp(image.color, endColor, Time.deltaTime * fadeSpeed);
	}
}
