using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCamera : MonoBehaviour
{
	[SerializeField]
	protected Root root;

	[SerializeField]
	protected float speed = 0.8f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 targetPosition = new Vector3(root.transform.position.x, root.transform.position.y, -10);

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
	}
}
