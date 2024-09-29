using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AIDemo : MonoBehaviour
{
    public Button aiDemo;
    // Start is called before the first frame update
    void Start()
    {
        if (aiDemo != null) {
            aiDemo.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    void OnButtonClick()
    {
        SceneManager.LoadScene("AIDemo");
    }
}
