using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
	[System.Serializable]
	protected enum Mode
	{
		None,
		Growing,
		Retracting
	}

	[SerializeField]
	protected float moveDistance = 0.5f;

	[SerializeField]
	protected float moveInterval = 0.1f;

	[SerializeField]
	protected Mode mode;

	[SerializeField]
	protected GameObject segmentPrefab;

	private Vector2 mousePosition;

	private List<GameObject> segments = new List<GameObject>();

	private float lastMoveTime = 0;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		mode = Mode.None;
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		bool canMove = Time.time - lastMoveTime > moveInterval;

		if (Input.GetKey(KeyCode.Mouse0) && canMove)
		{
			mode = Mode.Growing;
			lastMoveTime = Time.time;
		}
		if (Input.GetKey(KeyCode.Mouse1) && canMove)
		{
			mode = Mode.Retracting;
			lastMoveTime = Time.time;
		}

		Move(mode);
	}

	protected virtual void Move(Mode mode)
	{
		if (mode == Mode.Growing)
		{
			transform.up = GetDirectionToMouse();

			GameObject newSegment = Instantiate(segmentPrefab, transform.position, Quaternion.identity);
			newSegment.transform.up = transform.up;

			transform.position += transform.up * moveDistance;
			segments.Add(newSegment);
		}

		if (mode == Mode.Retracting)
		{
			transform.position = segments[segments.Count - 1].transform.position;
			Destroy(segments[segments.Count - 1]);
			segments.RemoveAt(segments.Count - 1);
		}
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
