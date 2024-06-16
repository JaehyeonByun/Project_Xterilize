using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Tutorial,
        Operation,
        GameEnd,
    }

    public GameState gameState;
    public static List<string> timeLog = new List<string>();
    public static List<string> ContaminationLog = new List<string>();
    public static List<string> WhyLog = new List<string>();

    // Update is called once per frame
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial Scene");
        gameState = GameState.Tutorial;
    }

    public void Operation()
    {
        SceneManager.LoadScene("Operation Scene");
        gameState = GameState.Operation;
    }

    public void GameExit()
    {
        UnityEngine.Application.Quit();
        gameState = GameState.GameEnd;
    }

    public async void MakeCsv(List<string> timeLog)
    {
        Debug.Log("save");
        FileStream fs = new FileStream("Assets/ContaminationTime.csv", FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

        var list = timeLog;
        for (int i = 0; i < list.Count; i++)
        {
            var tmp = list[i];

            sw.WriteLine(string.Format("{0}", tmp));
        }

        sw.Close();

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (gameState == GameState.Tutorial)
        {

        }

        else if (gameState == GameState.Operation)
        {

        }
  
    }
}

