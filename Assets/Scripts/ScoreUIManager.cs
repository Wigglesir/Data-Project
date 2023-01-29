using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreUIManager : MonoBehaviour
{
    public GameObject[] highScores;

    // Start is called before the first frame update
    void Start()
    {
        highScores = GameObject.FindGameObjectsWithTag("ScoreText");

        display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }

    public void Clear()
    {
        DataManager.instance.ClearScore();
        display();
    }

    public void display()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].GetComponent<TextMeshProUGUI>().text = $"Name-{DataManager.instance.highScoresList[i].h_PlayerName} {DataManager.instance.highScoresList[i].h_Score}-Points";
        }
    }
}
