using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public Button howToPlay;
    // Start is called before the first frame update
    void Start()
    {
        if (howToPlay != null) {
            howToPlay.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    void OnButtonClick()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
