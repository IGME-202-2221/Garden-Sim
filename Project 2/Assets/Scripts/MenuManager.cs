using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void RestartGame()
    {
        // loads the game scene
        SceneManager.LoadScene(0);
    }
}
