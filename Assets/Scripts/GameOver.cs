using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
