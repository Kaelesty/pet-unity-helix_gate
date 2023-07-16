using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void startLevel_0()
    {
        SceneManager.LoadScene("Level_0");
    }

    public void startLevel_1()
    {
        SceneManager.LoadScene("Level_1");
    }
}
