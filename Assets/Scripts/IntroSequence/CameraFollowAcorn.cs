using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAcorn : MonoBehaviour
{
    public Camera mainCam;
    public float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.gameObject.transform.Translate(new Vector3(fallSpeed*Time.deltaTime, 0, 0), Space.World);
    }
}
