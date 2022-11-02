using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
}
