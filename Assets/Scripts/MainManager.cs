using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestNameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_Best;
    private string m_BestName;

    private bool m_GameOver = false;
    


    // Start is called before the first frame update

    private void Awake()
    {
        if (!GameObject.Find("DataManager"))
        {
            
            SceneManager.LoadScene("menu");
            
        }
        
    }
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        m_Best = DataManager.instance.highScore;
        m_BestName = DataManager.instance.playerHighName;
        
        bestScoreText.text = $"Best: {m_Best}";
        bestNameText.text = $"{m_BestName}";

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

            
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("menu");
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points >= m_Best)
        {
            m_Best = m_Points;
            bestScoreText.text = $"Best: {m_Best}";
            bestNameText.text = $"{DataManager.instance.playerName}";

            DataManager.instance.highScore = m_Best;
            DataManager.instance.playerHighName = DataManager.instance.playerName;
            
            
            

        }

        //if (m_Points > DataManager.instance.highScore)
        //{
        //    DataManager.instance.AddNewScore(DataManager.instance.playerName, m_Points);
        //}
        
        
    }

    public void GameOver()
    {
        DataManager.instance.AddNewScore(DataManager.instance.playerName, m_Points);
        DataManager.instance.SaveScores();
        m_GameOver = true;
        GameOverText.SetActive(true);
        
    }
}
