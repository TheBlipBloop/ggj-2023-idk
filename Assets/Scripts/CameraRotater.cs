using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    public float rotateSpeed;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 90 / rotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            this.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
            time -= Time.deltaTime;
        }
    }
}
