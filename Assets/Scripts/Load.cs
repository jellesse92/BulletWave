using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Load : MonoBehaviour {
    int sceneIndex;

    public void ChangeSceneIndex(int index)
    {
        sceneIndex = index;
    }
    public void LoadScene()
    {
        if(sceneIndex == 0)
            Application.Quit();
        else
            SceneManager.LoadScene(sceneIndex);
    }
}
