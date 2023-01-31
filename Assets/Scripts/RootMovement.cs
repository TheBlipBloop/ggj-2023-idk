using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMovement : MonoBehaviour
{
	// [SerializeField]
	// protected Rigidbody body;

	[SerializeField]
	protected float moveSpeedStart = 6f;

	[SerializeField]
	protected float distanceToMouseMoveThreshold = 1f;

	protected float moveSpeed;

	protected Vector2 mousePosition;

	void Awake()
	{
		moveSpeed = moveSpeedStart;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMousePosition();

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
		transform.up = GetDirectionToMouse();
		transform.position += transform.up * speed * Time.deltaTime;
	}

	protected float GetDistanceToMouse()
	{
		return (mousePosition - (Vector2)transform.position).magnitude;
	}

	protected Vector3 GetDirectionToMouse()
	{
		return (Vector3)mousePosition - transform.position;
	}
}
