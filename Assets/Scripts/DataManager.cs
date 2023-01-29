using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;
    public List<HighScoreData> highScoresList;
    public HighScoreData[] highScoresArray;


    public string playerName;
    public string playerHighName;
    public int highScore;

    private string path = "D:/Unity Projects/Data-Project";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        highScoresList = new List<HighScoreData>();
        DataManager.instance.LoadScores();
        

        //get highScore from somewhere;
    }

   
    

    public void SaveScores()
    {
        //Debug.Log("count before tyring to save " + highScoresList.Count);

        //string json = JsonUtility.ToJson(highScoresList);
        //File.WriteAllText(Application.persistentDataPath + "/blocksave.json", json);
        //Debug.Log(json);
        //Debug.Log("count AFTER tyring to save " + highScoresList.Count);

        string json = JsonConvert.SerializeObject(highScoresList);
        

        File.WriteAllText(path + "/dataproject.json", json);

    }

    public void AddNewScore(string name, int score)
    {
        highScoresList.Add(new HighScoreData(name, score));
        highScoresList.Sort((x, y) => y.h_Score.CompareTo(x.h_Score));

        if (score > highScore)
        {
            highScore = score;
            playerHighName = name;
        }

        Debug.Log("from addnewscore " + highScoresList.Count);
        if (highScoresList.Count > 10)
        {
            highScoresList.RemoveAt(10);
        }
        //SaveScores();
    }

    public void LoadScores()
    {
        if (File.Exists(path + "/dataproject.json"))
        {
            string json = File.ReadAllText(path + "/dataproject.json");

            highScoresList.Clear();
            highScoresList = JsonConvert.DeserializeObject<List<HighScoreData>>(json);
            

            highScoresList.Sort((x, y) => y.h_Score.CompareTo(x.h_Score));

            highScoresArray = highScoresList.ToArray();

            
            highScore = highScoresList[0].h_Score;
            playerHighName = highScoresList[0].h_PlayerName;


        }

        if (highScoresList.Count < 10)
        {
            for (int i = highScoresList.Count; i < 10; i++)
            {
                highScoresList.Add(new HighScoreData("Default", 0));
            }
        }



        //string path = path + "/blocksave.json";

        //if (File.Exists(path + "/dataproject.json"))
        //{
        //    string json = File.ReadAllText(path);
        //    highScoresList = JsonUtility.FromJson<List<HighScoreData>>(json);
        //    if (highScoresList.Count < 1)
        //    {
        //        AddNewScore("default", 0);
        //    }
        //    highScoresArray = highScoresList.ToArray();

        //    Debug.Log("count from load " + highScoresArray.Length);
        //    highScore = highScoresArray[0].h_Score;
        //    playerHighName = highScoresArray[0].h_PlayerName;


        //}
    }

    public void ClearScore()
    {
        highScoresList.Clear();
        for (int i = 0; i < 10; i++)
        {
            highScoresList.Add(new HighScoreData("Default", 0));
        }




    }


    [System.Serializable]
    public class HighScoreData
    {
        public string h_PlayerName;
        public int h_Score;

        public HighScoreData(string name, int score)
        {
            h_PlayerName = name;
            h_Score = score;
        }
    }

}





