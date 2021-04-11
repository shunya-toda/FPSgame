using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    private GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText = GameObject.Find("GameOver");
    }

    
    public void GameOver()
    {
        gameOverText.GetComponent<Text>().text="GameOver";
    }
}
