// #define JankyCollisionDetection

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Root : MonoBehaviour
{

#if JankyCollisionDetection
	[System.Serializable]
	protected struct RootCollisionNode
	{
		public Vector2 position;
		public Vector2 direction;

		public RootCollisionNode(Vector2 position, Vector2 direction)
		{
			this.position = position;
			this.direction = direction;
		}
	}
#endif

	[SerializeField]
	protected Rigidbody2D body;

	[SerializeField]
	protected LineRenderer rootRenderer;

	[SerializeField]
	protected float baseMoveSpeed = 6;

	[SerializeField]
	protected float rootRecordPositionInterval = 0.25f;

	protected LinkedList<Vector2> rootPositions = new LinkedList<Vector2>();

#if JankyCollisionDetection
	protected List<RootCollisionNode> collisionPoints = new List<RootCollisionNode>();
#endif

	protected Vector2 mousePosition;

	private Vector2 lastRecordedRootPosition;

	// Start is called before the first frame update
	void Start()
	{
		rootPositions.AddFirst(transform.position);
#if JankyCollisionDetection
		collisionPoints.Add(new RootCollisionNode(transform.position, transform.right));
#endif
	}

	// Update is called once per frame
	void Update()
	{
		UpdateMousePosition();
		UpdateMovement();
	}

	protected void UpdateMovement()
	{
#if JankyCollisionDetection
		RootCollisionNode debugg = collisionPoints[collisionPoints.Count - 1];

		Debug.DrawRay(debugg.position, debugg.direction, Color.green);
		Debug.DrawRay(debugg.position, transform.position - (Vector3)debugg.position, Color.red);
		Debug.Log(Vector2.Angle(((Vector2)transform.position - debugg.position).normalized, debugg.direction));
#endif
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Grow(GetDirectionToMouse());

#if JankyCollisionDetection
			RootCollisionNode lastCollisionNode = collisionPoints[collisionPoints.Count - 1];

			Debug.DrawRay(lastCollisionNode.position, lastCollisionNode.direction, Color.green);
			Debug.DrawRay(lastCollisionNode.position, (Vector3)lastCollisionNode.position - transform.position, Color.green);

			if (Vector2.Distance(transform.position, lastCollisionNode.position) > 3f || Vector2.Angle(((Vector2)transform.position - lastCollisionNode.position).normalized, lastCollisionNode.direction) > 6f)
			{
				collisionPoints.Add(new RootCollisionNode(transform.position, GetDirectionToMouse()));
			}
#endif
			return;
		}
		if (Input.GetKey(KeyCode.Mouse1))
		{
			UnGrow();
			return;
		}
		body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, Time.fixedDeltaTime * 8f);

		LinkedListNode<Vector2> node = rootPositions.First;
		while (node.Next.Next != null)
		{
			Debug.DrawLine(node.Value, node.Next.Value, Color.yellow);
			node = node.Next;
		}
	}


	protected void Grow(Vector2 direction)
	{
		body.velocity = direction * baseMoveSpeed;

		if (Vector2.Distance(transform.position, lastRecordedRootPosition) > rootRecordPositionInterval)
		{
			rootPositions.AddFirst(transform.position);

			rootRenderer.positionCount++;
			rootRenderer.SetPosition(rootRenderer.positionCount - 1, transform.position);

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

		if (Vector3.Distance(transform.position, mostRecentPosition) < rootRecordPositionInterval / 2f)
		{
			rootRenderer.positionCount--;
			rootPositions.RemoveFirst();
		}
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
}
