using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornFall : MonoBehaviour
{
	public Transform fallDest;
	public float rotateSpeed;
	public float fallSpeed;
	public GameObject root;
	public CameraFollowAcorn acornFollow;
	public CameraRotateForPlay camFinalRotate;
	public float startTime = 4;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Time.timeSinceLevelLoad < startTime)
		{
			return;
		}

		if (Mathf.Abs(this.transform.position.x - fallDest.position.x) > 0.25)
		{
			this.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
			this.transform.Translate(new Vector3(fallSpeed * Time.deltaTime, 0, 0), Space.World);
		}
		else
		{
			acornFollow.enabled = false;
			camFinalRotate.enabled = true;
			GameObject.Destroy(this);
		}
	}
}
