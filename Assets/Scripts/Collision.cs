using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Unity.Burst.Intrinsics.X86.Avx;
using System.Text;

public class Collision : MonoBehaviour
{
    

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.GetComponent<Collider>().gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Contaminated!");
            
            string currentTime = System.DateTime.Now.ToString("HHΩ√mm∫–ss√ ");
            Debug.Log("Current Time: " + currentTime);
            GameManager.timeLog.Add(currentTime);
            GameManager.ContaminationLog.Add("ø¿ø∞µ» π∞√º ¡¢√À");
            GameManager.WhyLog.Add(this.gameObject.name);


        }
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    MakeCsv(); 
        //}
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
                sw.WriteLine("TimeStamp,Contamination");
                var list = GameManager.timeLog;
                for (int i = 0; i < list.Count; i++)
                {
                    var tmp = list[i];
                    sw.WriteLine(string.Format("{0}", tmp));
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
    
