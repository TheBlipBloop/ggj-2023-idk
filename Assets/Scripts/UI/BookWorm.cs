using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookWorm : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            deactivateWorm();
        }
    }

    public void activateWorm(string wormMsg)
    {
        this.gameObject.SetActive(true);
        text.text = wormMsg;
    }

    public void deactivateWorm()
    {
        text.text = "";
        this.gameObject.SetActive(false);
    }
}
