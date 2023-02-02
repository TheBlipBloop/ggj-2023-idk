using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Root : MonoBehaviour
{
	[SerializeField]
	protected Rigidbody2D body;

	[SerializeField]
	protected LineRenderer rootRenderer;

	[SerializeField]
	protected float baseMoveSpeed = 6;

	[SerializeField]
	protected float rootRecordPositionInterval = 0.25f;

	[Header("Nutrients")]

	[SerializeField]
	protected float nutrientUseWhileMoving = 1.0f;

	[SerializeField]
	protected float nutrientsRetractScale = 0.75f;

	[SerializeField]
	protected RootResourcePool nutrientPool;

	[Header("Health")]

	[SerializeField]
	protected RootResourcePool healthPool;

	protected LinkedList<Vector2> rootPositions = new LinkedList<Vector2>();

	protected Vector2 mousePosition;

	private Vector2 lastRecordedRootPosition;

	// Start is called before the first frame update
	void Start()
	{
		rootPositions.AddFirst(transform.position);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMousePosition();
		UpdateMovement();

		if (rootRenderer.positionCount > 0)
		{
			rootRenderer.SetPosition(rootRenderer.positionCount - 1, transform.position);
		}
	}

	protected void UpdateMovement()
	{
		if (Input.GetKey(KeyCode.Mouse0) && nutrientPool.HasResources())
		{
			Grow(GetDirectionToMouse());
			nutrientPool.RemoveResources(nutrientUseWhileMoving * Time.deltaTime);
			return;
		}
		if (Input.GetKey(KeyCode.Mouse1) && GetRootLength() > 0)
		{
			UnGrow();
			nutrientPool.AddResources(nutrientUseWhileMoving * nutrientsRetractScale * Time.deltaTime);
			return;
		}

		body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, Time.fixedDeltaTime * 8f);
	}


	protected void Grow(Vector2 direction)
	{
		body.velocity = direction * baseMoveSpeed;

		if (Vector2.Distance(transform.position, lastRecordedRootPosition) > rootRecordPositionInterval)
		{
			rootPositions.AddFirst(transform.position);

			rootRenderer.positionCount++;

			lastRecordedRootPosition = transform.position;
		}
	}

	protected void UnGrow()
	{
		if (rootPositions.Count == 0)
		{
			return;
		}

		Vector3 mostRecentPosition = rootPositions.First.Value;

		body.velocity = (mostRecentPosition - transform.position).normalized * baseMoveSpeed;

		if (rootRenderer.positionCount > 0 && Vector3.Distance(transform.position, mostRecentPosition) < rootRecordPositionInterval / 2f)
		{
			rootRenderer.positionCount--;
			rootPositions.RemoveFirst();
		}
	}

	public float GetRootLength()
	{
		float totalLength = 0;
		LinkedListNode<Vector2> node = rootPositions.First;

		while (node.Next != null)
		{
			totalLength += Vector2.Distance(node.Value, node.Next.Value);
			node = node.Next;
		}

		return totalLength;
	}

	private void UpdateMousePosition()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	protected float GetDistanceToMouse()
	{
		return (mousePosition - (Vector2)transform.position).magnitude;
	}

	protected Vector3 GetDirectionToMouse()
	{
		return ((Vector3)mousePosition - transform.position).normalized;
	}

	/*********************************************************************************************/
	/** Nutrients */

	public float GetNutrients()
	{
		return nutrientPool.GetResources();
	}

	/*********************************************************************************************/
	/** Health */

	public void Damage(float damage)
	{
		healthPool.RemoveResources(damage);
	}

	public void Heal(float health)
	{
		healthPool.AddResources(health);
	}

	public float GetHealth()
	{
		return healthPool.GetResources();
	}

	public bool IsAlive()
	{
		return healthPool.HasResources();
	}

	public bool IsDead()
	{
		return !IsAlive();
	}
}
