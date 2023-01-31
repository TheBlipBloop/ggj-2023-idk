using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMovement : MonoBehaviour
{
	[SerializeField]
	protected Rigidbody2D body;

	[SerializeField]
	protected float distanceToMouseMoveThreshold = 1f;

	[SerializeField]
	protected float moveSpeed;

	protected Vector2 mousePosition;

	protected bool moving;

	void Awake()
	{
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMousePosition();
		moving = Input.GetKey(KeyCode.Mouse0);
	}

	void FixedUpdate()
	{
		if (!moving)
		{
			body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, Time.fixedDeltaTime * 8f);
			return;
		}

		if (GetDistanceToMouse() < distanceToMouseMoveThreshold)
		{
			Move(moveSpeed * (GetDistanceToMouse() / distanceToMouseMoveThreshold));
		}
		else
		{
			Move(moveSpeed);
		}
	}

	private void UpdateMousePosition()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	protected virtual void Move(float speed)
	{
		body.velocity = GetDirectionToMouse() * speed;
	}

	protected float GetDistanceToMouse()
	{
		return (mousePosition - (Vector2)transform.position).magnitude;
	}

	protected Vector3 GetDirectionToMouse()
	{
		return ((Vector3)mousePosition - transform.position).normalized;
	}
}
