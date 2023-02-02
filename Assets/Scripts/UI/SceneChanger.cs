using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(changeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeScene()
    {
        SceneManager.LoadScene(text.text);
    }
}
