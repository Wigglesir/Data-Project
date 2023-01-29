using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    private bool nameInputChanged = false;

    
    

    // Start is called before the first frame update
    void Start()
    {
        //nameInputField.ActivateInputField();

    }


    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void Scores()
    {
        SceneManager.LoadScene("score");
    }

    public void SubmitInput()
    {
        
        if (!nameInputChanged)
        {
            DataManager.instance.playerName = "Player01";
        }
        else
        {
            DataManager.instance.playerName = nameInputField.text;
        }

        StartGame();   
    }

    public void NameInputChange()
    {
        nameInputChanged = true;
        Debug.Log(nameInputChanged);
    }

    public void QuitGame()
    {

        Debug.Log("Inside quit game");
        DataManager.instance.SaveScores();

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
        }

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif


    }

}
