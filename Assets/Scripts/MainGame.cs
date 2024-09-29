using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    public Button mainGame;
    // Start is called before the first frame update
    void Start()
    {
        if (mainGame != null) {
            mainGame.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    void OnButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
