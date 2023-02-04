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
	protected float nutrientLosePerSecond = 0.3f;

	[SerializeField]
	protected float additionalNutrientUseWhileMoving = 0.1f;

	[SerializeField]
	protected float nutrientsRetractScale = 0.75f;

	[SerializeField]
	protected RootResourcePool nutrientPool;

	[Header("Health")]

	[SerializeField]
	protected RootResourcePool healthPool;

	protected LinkedList<Vector2> rootPositions = new LinkedList<Vector2>();

	[Header("Powerups")]

	[SerializeField]
	protected PowerupType activePowerup = PowerupType.None;

	[SerializeField]
	protected GameObject drill;

	protected float powerupEndTime = float.NegativeInfinity;

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

		nutrientPool.RemoveResources(nutrientLosePerSecond * Time.deltaTime);

		if (activePowerup != PowerupType.None && Time.time >= powerupEndTime)
		{
			OnPowerupDeactivated(activePowerup);
		}
	}

	protected void UpdateMovement()
	{
		if (Input.GetKey(KeyCode.Mouse0) && nutrientPool.HasResources())
		{
			Grow(GetDirectionToMouse());
			nutrientPool.RemoveResources(additionalNutrientUseWhileMoving * Time.deltaTime);
			return;
		}
		if (Input.GetKey(KeyCode.Mouse1) && GetRootLength() > 0)
		{
			UnGrow();
			nutrientPool.AddResources(additionalNutrientUseWhileMoving * Time.deltaTime);
			return;
		}

		body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, Time.fixedDeltaTime * 8f);
	}


	protected void Grow(Vector2 direction)
	{
		body.velocity = direction * baseMoveSpeed * Map.GetSpeedScalar(transform.position);
		body.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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

		body.velocity = (mostRecentPosition - transform.position).normalized * baseMoveSpeed * Map.GetSpeedScalar(transform.position);

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

	public void AddNutrients(float amount)
	{
		nutrientPool.AddResources(amount);
	}

	public float GetNutrients()
	{
		return nutrientPool.GetAmount();
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
		return healthPool.GetAmount();
	}

	public bool IsAlive()
	{
		return healthPool.HasResources();
	}

	public bool IsDead()
	{
		return !IsAlive();
	}

	/*********************************************************************************************/
	/** Powerups */

	public void ApplyPowerup(PowerupType powerup, float duration)
	{
		activePowerup = powerup;
		powerupEndTime = Time.time + duration;

		OnPowerupActivated(powerup);
	}

	public void CanclePowerup()
	{
		PowerupType lastPowerup = activePowerup;
		activePowerup = PowerupType.None;
		powerupEndTime = float.NegativeInfinity;
		OnPowerupDeactivated(lastPowerup);
	}

	public void OnPowerupActivated(PowerupType powerup)
	{
		if (powerup == PowerupType.Drill)
		{
			drill.SetActive(true);
		}
	}

	public void OnPowerupDeactivated(PowerupType powerup)
	{
		if (powerup == PowerupType.Drill)
		{
			drill.SetActive(false);
		}
	}


	/*********************************************************************************************/
	/** Root Positions */

	public float GetSegmentDistance()
	{
		return rootRecordPositionInterval;
	}

	public int GetRootPositionsNoAlloc(ref Vector2[] positions, float maxDistance = -1)
	{
		LinkedListNode<Vector2> node = rootPositions.First;
		Vector2 referencePosition = transform.position;
		int index = 0;

		while (node.Next != null && index < positions.Length)
		{
			if (maxDistance > 0 && (node.Value - referencePosition).sqrMagnitude > maxDistance * maxDistance)
			{
				node = node.Next;
				continue;
			}

			positions[index] = node.Value;
			index++;
			node = node.Next;
		}

		return index;
	}

	/*********************************************************************************************/
	/** Health */

	// Called every frame while something is colliding with this root
	public virtual void OnCollideWith(RaycastHit2D collision)
	{
	}
	/*********************************************************************************************/
	/** Placeholder UI */

	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 250f, 25f), "Health: " + healthPool.GetAmount());
		GUI.Label(new Rect(0, 25f, 250f, 25f), "Nutrients: " + nutrientPool.GetAmount());
	}

}
