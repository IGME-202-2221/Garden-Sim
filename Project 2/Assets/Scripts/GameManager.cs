using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if there are no agents (bees and butterflies) left on screen, transition to restart screen
        if (AgentManager.Instance.agentsList.Count <= 0)
        {
            // loads the restart scene
            SceneManager.LoadScene(1);
        }
    }
}
