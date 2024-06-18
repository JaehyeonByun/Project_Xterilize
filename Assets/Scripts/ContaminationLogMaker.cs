using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ContaminationLogMaker : MonoBehaviour
{
    public GameObject Conta;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MakeCsv();
        }
    }
    void MakeCsv()
    {
        
        string csvFilePath = "Assets/CsvData/ContaminationTime.csv";
        /*if (timeLog == null || timeLog.Count == 0)
        {
            Debug.Log("No Contamination");
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Time,Why");
                sw.WriteLine(string.Format("No time"));
            }
            return;
        }*/
        try
        {
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("시간,원인,발생");
                var Timelist = GameManager.timeLog;
                var Contalist = GameManager.ContaminationLog;
                var Whylist = GameManager.WhyLog;
                for (int i = 0; i < Timelist.Count; i++)
                {
                    var tmp1 = Timelist[i];
                    var tmp2 = Contalist[i];
                    var tmp3 = Whylist[i];
                    sw.WriteLine(string.Format("{0},{1},{2}", tmp1,tmp2,tmp3));
                }
            }
            Debug.Log("CSV file saved");
        }
        catch (Exception ex)
        {
            Debug.Log("An error occurred while writing to the CSV file: " + ex.Message);
        }


    }
}
    

