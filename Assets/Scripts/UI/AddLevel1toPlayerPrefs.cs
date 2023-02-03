using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLevel1toPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level 1", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
