using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Restart()//Reload Scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Load the current scene
    }
}
