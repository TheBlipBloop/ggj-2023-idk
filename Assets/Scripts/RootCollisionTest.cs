using UnityEngine;

public class RootCollisionTest : MonoBehaviour
{
	public RootCollision rootCollision;

	// Start is called before the first frame update
	void Start()
	{
		rootCollision.onCollisionStay += OnTouchRoot;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += transform.up * Time.deltaTime;
	}

	protected void OnTouchRoot(Root root, RootCollision rootCollision, RaycastHit2D collision)
	{
		if (collision.collider.gameObject != gameObject)
		{
			return;
		}

		Debug.Log("Touched Root!");
		rootCollision.onCollisionStay -= OnTouchRoot;
		Destroy(gameObject);
	}
}
