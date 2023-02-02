using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    public GameObject root;
    public Camera mainCam;
    private Vector3 startTrans;
    // Start is called before the first frame update
    void Start()
    {
        startTrans = root.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        root.GetComponent<Root>().enabled = false;
        mainCam.GetComponent<RootCamera>().enabled = false;
        mainCam.GetComponent<CameraReturnToPlant>().enabled = true;
        mainCam.GetComponent<CameraReturnToPlant>().moveToward(startTrans);
    }
}
