using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSpriteColor : MonoBehaviour
{
	public SpriteRenderer sprite;
	public Color endColor = Color.black;
	public float fadeSpeed = 2;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		sprite.color = Color.Lerp(sprite.color, endColor, Time.deltaTime * fadeSpeed);
	}
}
