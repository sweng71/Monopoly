using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartTheGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
