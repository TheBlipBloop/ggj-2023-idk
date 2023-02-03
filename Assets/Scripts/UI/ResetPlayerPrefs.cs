using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPrefs : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(resetPrefs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level 1", 1);
    }
}
