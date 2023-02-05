using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateForPlay : MonoBehaviour
{
    public float timeToRotate;
    public GameObject root;
    public RootCamera rootCam;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToRotate;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
        {
            this.transform.Rotate(0, 0, -90 * Time.deltaTime / timeToRotate);
            timeLeft -= Time.deltaTime;
        }
        else
        {
            root.SetActive(true);
            rootCam.enabled = true;
            this.enabled = false;
        }
    }
}
