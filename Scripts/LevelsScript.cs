using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsScript : MonoBehaviour
{
    private Scene scene;


    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(4 + i);
    }

    public void LoadBack()
    {
        SceneManager.LoadScene(1);
    }
}
