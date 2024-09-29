using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MongoDemo : MonoBehaviour
{
    public Button mongoDemo;
    // Start is called before the first frame update
    void Start()
    {
        if (mongoDemo != null) {
            mongoDemo.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    void OnButtonClick()
    {
        SceneManager.LoadScene("MongoDemo");
    }
}
