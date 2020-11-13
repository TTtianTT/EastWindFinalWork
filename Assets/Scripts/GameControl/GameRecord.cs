using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRecord : MonoBehaviour
{
    public void OnRebackGame()
    {
        SceneManager.LoadScene(0);
    }
}
