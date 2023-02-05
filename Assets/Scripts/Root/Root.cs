using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class Root : MonoBehaviour
{
	[System.Serializable]
	public struct PowerupEffect
	{
		public PowerupType type;

		public float endTime;

		public PowerupEffect(PowerupType inType, float inEndTime)
		{
			type = inType;
			endTime = inEndTime;
		}
	}

	[SerializeField]
	protected Rigidbody2D body;

	[SerializeField]
	protected CircleCollider2D collider;

	[SerializeField]
	protected LineRenderer rootLineRenderer;

	[SerializeField]
	protected RootRenderer rootRenderer;

	[SerializeField]
	protected float baseMoveSpeed = 6;

	[SerializeField]
	protected float defaultThickness = 0.6f;

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

	[SerializeField]
	protected float damageCooldown = 0.75f;

	[SerializeField]
	protected float vulnerableLength = 5f;

	private float lastDamageTime = float.NegativeInfinity;

	protected LinkedList<Vector2> rootPositions = new LinkedList<Vector2>();

	[Header("Powerups")]

	[SerializeField]
	protected List<PowerupEffect> activePowerups = new List<PowerupEffect>(4);

	[SerializeField]
	protected GameObject drill;

	[SerializeField]
	protected float shrinkPowerupThickness = 0.1f;

	[SerializeField]
	protected float invincibilityDeflectDuration = 3f;

	[SerializeField]
	protected Color invincibilityPowerupRootColor = new Color(0.75f, 0, 0.75f, 1f);

	[SerializeField]
	protected Color speedPowerupRootColor = Color.red;

	protected Vector2 mousePosition;

	private Vector2 lastRecordedRootPosition;

	private float originalMoveSpeed;

	protected float HACKY_initialGrowthTimer = 1f;

	// Start is called before the first frame update
	void Start()
	{
		rootPositions.AddFirst(transform.position);
		ResetRootThickness();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}

		// hacky bad
		if (IsDead())
		{
			return;
		}

		// HACK
		if (HACKY_initialGrowthTimer >= 0)
		{
			Grow(Vector2.right);
			HACKY_initialGrowthTimer -= Time.deltaTime;
			if (rootLineRenderer.positionCount > 0)
			{
				rootLineRenderer.SetPosition(rootLineRenderer.positionCount - 1, transform.position);
			}
			return;
		}

		UpdateMousePosition();
		UpdatePowerups();
		UpdateMovement();

		if (rootLineRenderer.positionCount > 0)
		{
			rootLineRenderer.SetPosition(rootLineRenderer.positionCount - 1, transform.position);
		}

		// Ambient nutrient loss
		nutrientPool.RemoveResources(nutrientLosePerSecond * Time.deltaTime);


		// Shitty hacky I dont want to talk about it fuck you
		Vector2 direction = body.velocity.normalized;
		if (body.velocity.sqrMagnitude > 0.1)
		{
			drill.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
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

		if (Vector2.Distance(transform.position, lastRecordedRootPosition) > rootRecordPositionInterval)
		{
			rootPositions.AddFirst(transform.position);

			rootLineRenderer.positionCount++;

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

		if (rootLineRenderer.positionCount > 0 && Vector3.Distance(transform.position, mostRecentPosition) < rootRecordPositionInterval / 2f)
		{
			rootLineRenderer.positionCount--;
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

	protected float GetLengthToPointOnRoot(Vector2 point, float tolerance = 1f)
	{
		float totalLength = 0;
		float maxLength = Vector2.Distance(transform.position, point);
		float distance = 0;
		LinkedListNode<Vector2> node = rootPositions.First;

		while (node.Next != null)
		{
			distance = Vector2.Distance(node.Value, point);
			if (distance < tolerance)
			{
				return totalLength;
			}
			if (distance > maxLength)
			{
				return Mathf.Infinity; // Not on root within tolerance
			}
			totalLength += Vector2.Distance(node.Value, node.Next.Value);
			node = node.Next;
		}

		return Mathf.Infinity;// Not on root within tolerance
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

	public float GetNutrientsPct()
	{
		return nutrientPool.GetResourcesPct();
	}

	/*********************************************************************************************/
	/** Health */

	public bool Damage(float damage)
	{
		if (Time.time < lastDamageTime + damageCooldown)
		{
			return false;
		}

		healthPool.RemoveResources(damage);
		lastDamageTime = Time.time;
		return true;
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
		return healthPool.HasResources() && nutrientPool.HasResources();
	}

	public bool IsDead()
	{
		return !IsAlive();
	}

	public float GetHealthPct()
	{
		return healthPool.GetResourcesPct();
	}

	/*********************************************************************************************/
	/** Powerups */

	protected void UpdatePowerups()
	{
		for (int i = 0; i < activePowerups.Count; i++)
		{
			if (Time.time >= activePowerups[i].endTime)
			{
				CancelPowerup(activePowerups[i]);
			}
		}
	}

	public void ApplyPowerup(PowerupType powerup, float duration)
	{
		int find = FindPowerup(powerup);
		if (find != -1)
		{
			activePowerups[find] = new PowerupEffect(powerup, Time.time + duration);
			return;
		}

		activePowerups.Add(new PowerupEffect(powerup, Time.time + duration));

		OnPowerupActivated(powerup);
	}

	public void CancelPowerup(PowerupEffect powerup)
	{
		activePowerups.Remove(powerup);
		OnPowerupDeactivated(powerup.type);
	}

	protected void OnPowerupActivated(PowerupType powerup)
	{
		if (powerup == PowerupType.Drill)
		{
			drill.SetActive(true);
		}
		if (powerup == PowerupType.Speed)
		{
			originalMoveSpeed = baseMoveSpeed;
			baseMoveSpeed *= 2f;
			rootRenderer.SetColor(speedPowerupRootColor);
		}
		if (powerup == PowerupType.Shrink)
		{
			SetRootThickness(shrinkPowerupThickness);
		}
		if (powerup == PowerupType.Invincibility)
		{
			rootRenderer.SetColor(invincibilityPowerupRootColor);
		}
	}

	protected void OnPowerupDeactivated(PowerupType powerup)
	{
		rootRenderer.ResetColor();
		if (powerup == PowerupType.Drill)
		{
			drill.SetActive(false);
		}
		if (powerup == PowerupType.Speed)
		{
			baseMoveSpeed = originalMoveSpeed;
		}
		if (powerup == PowerupType.Shrink)
		{
			ResetRootThickness();
		}
	}

	protected int FindPowerup(PowerupType type)
	{
		for (int i = 0; i < activePowerups.Count; i++)
		{
			if (activePowerups[i].type == type)
			{
				return i;
			}
		}

		return -1;
	}

	public bool IsInvincible()
	{
		return FindPowerup(PowerupType.Invincibility) >= 0;
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
	/** Collision */

	// Called every frame while something is colliding with this root
	public virtual void OnCollideWith(RaycastHit2D collision)
	{
		WormMovement worm = collision.collider.GetComponent<WormMovement>();
		WormCollision wormCol = collision.collider.GetComponent<WormCollision>();
		bool invincible = IsInvincible();

		if (invincible)
		{
			worm.Deflect(collision.point, invincibilityDeflectDuration);
		}

		if (wormCol != null && !invincible)
		{
			worm.Deflect(collision.point, 1f);

			bool canDamageRootSection = GetLengthToPointOnRoot(collision.point) < vulnerableLength;
			Debug.Log(GetLengthToPointOnRoot(collision.point));
			if (canDamageRootSection)
			{
				Damage(wormCol.damage);
			}
		}
	}
	/*********************************************************************************************/
	/** Placeholder UI */

	// void OnGUI()
	// {
	// 	GUI.Label(new Rect(0, 0, 250f, 25f), "Health: " + healthPool.GetAmount());
	// 	GUI.Label(new Rect(0, 25f, 250f, 25f), "Nutrients: " + nutrientPool.GetAmount());
	// }

	/*********************************************************************************************/
	/** God-class I hardly knew her! */

	protected void ResetRootThickness()
	{
		SetRootThickness(defaultThickness);
	}

	protected void SetRootThickness(float newThickness)
	{
		collider.radius = newThickness / 2f;
		rootRenderer.SetThickness(newThickness);
	}
}
