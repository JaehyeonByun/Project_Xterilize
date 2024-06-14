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
    private List<string> timeLog = new List<string>();

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.GetComponent<Collider>().gameObject.CompareTag("player"))
        {
            Debug.Log("Player Contaminated!");
            string currentTime = System.DateTime.Now.ToString("HH��mm��ss��");
            timeLog.Add(currentTime);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MakeCsv(); // CSV ���Ϸ� �����ϴ� �Լ� ȣ��
        }
    }
    void MakeCsv()
    {
        string csvFilePath = "Assets/CsvData/ContaminationTime.csv";
        if (timeLog == null || timeLog.Count == 0)
        {
            Debug.Log("No Contamination");
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Time,Why");
                sw.WriteLine(string.Format("No time"));
            }
            return;
        }

        try
        {
            
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("TimeStamp,Contamination");
                var list = timeLog;
                for (int i = 0; i < list.Count; i++)
                {
                    var tmp = list[i];
                    sw.WriteLine(string.Format("{0}", tmp));
                }
            }
            Debug.Log("CSV file saved successfully.");
        }
        catch (Exception ex)
        {
            Debug.Log("An error occurred while writing to the CSV file: " + ex.Message);
        }

    }
}
    
