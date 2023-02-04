using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
    public List<Transform> points;
    public float speed;
    private int curTargIndex;
    private Vector3 currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        curTargIndex = 1;
        Transform currentTarget = points[1];
        currentDirection = Vector3.Normalize(currentTarget.position-this.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(currentDirection * speed * Time.deltaTime, Space.World);
        this.transform.Translate(new Vector3(0,0, -this.transform.position.z), Space.World);
        if(Mathf.Abs(this.transform.position.x-points[curTargIndex].position.x) < 0.25)
        {
            if(Mathf.Abs(this.transform.position.x - points[curTargIndex].position.x) < 0.25)
            {
                curTargIndex++;
                if(curTargIndex >= points.Count)
                {
                    curTargIndex = 0;
                }
                Transform currentTarget = points[curTargIndex];
                currentDirection = Vector3.Normalize(currentTarget.position - this.transform.position);
                Vector3 worldX = new Vector3(1, 0, 0);
                float theta = Mathf.Acos(Vector3.Dot(currentDirection, worldX)) * Mathf.Rad2Deg;
                if(Vector3.Cross(currentDirection, worldX).z > 0)
                {
                    theta = -theta;
                }
                this.transform.Rotate(new Vector3(-this.transform.rotation.x, -this.transform.rotation.y, -this.transform.rotation.z));
                this.transform.Rotate(new Vector3(0, 0, theta));
            }
        }
    }
}
