using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MainUIManager : MonoBehaviour
{
    private string playerName;
    private string playerBestName;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI playerBestNameText;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("DataManager"))
        {
            playerName = DataManager.instance.playerName;
            playerBestName = DataManager.instance.playerHighName;
        }
        else
        {
            playerName = "id10t";
        }
        
        playerNameText.text = playerName;
        playerBestNameText.text = playerBestName;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
