using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnToPlant : MonoBehaviour
{
    public float secondsToReturn;
    private float returnTimer;
    private Vector3 target;
    private float distanceX;
    private float distanceY;
    // Start is called before the first frame update
    void Start()
    {
        returnTimer = secondsToReturn;
    }

    // Update is called once per frame
    void Update()
    {
        if(returnTimer > 0)
        {
            if (target != null)
            {
                transform.Translate(new Vector3(distanceX * Time.deltaTime, distanceY * Time.deltaTime, 0));
                returnTimer -= Time.deltaTime;
            }
        }
    }

    public void moveToward(Vector3 targ)
    {
        target = targ;
        distanceX = (target.x-this.transform.position.x)/secondsToReturn;
        distanceY = (target.y-this.transform.position.y )/secondsToReturn;
        Debug.Log(distanceX);
        Debug.Log(distanceY);
    }
}
