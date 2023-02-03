using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levelButtons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject button in levelButtons)
        {
            Transform textObj = button.transform.GetChild(0);
            TextMeshProUGUI text = textObj.gameObject.GetComponent<TextMeshProUGUI>();
            if(!PlayerPrefs.HasKey(text.text))
            {
                Button buttonAct = button.GetComponent<Button>();
                buttonAct.SetEnabled(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
