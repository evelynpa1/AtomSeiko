using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (mainMenu != null) {
            mainMenu.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    void OnButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
