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
        this.transform.Translate(currentDirection * speed * Time.deltaTime);
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
            }
        }
    }
}
