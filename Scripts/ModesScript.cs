using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModesScript : MonoBehaviour
{

    public void LoadLevels()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadOnline()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadBack()
    {
        SceneManager.LoadScene(0);
    }

}
