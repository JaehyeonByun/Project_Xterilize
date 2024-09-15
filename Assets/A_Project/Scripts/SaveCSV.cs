using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveCSV : MonoBehaviour
{
    //CSV ������ ���� �׽�Ʈ �ڵ�
    private List<string> timeLog = new List<string>();

    // �ð� ������ ���� ����
    private float timeElapsed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        // 1�ʰ� ����ϸ� ���� �ð��� ����Ʈ�� �߰�
        if (timeElapsed >= 1.0f)
        {
  
            string currentTime = System.DateTime.Now.ToString("HH��mm��ss��");
            timeLog.Add(currentTime);

            // �ð� �ʱ�ȭ
            timeElapsed = 0.0f;

            // �α� ��� (������)
            Debug.Log("Current Time: " + currentTime);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            MakeCsv(); // CSV ���Ϸ� �����ϴ� �Լ� ȣ��
        }
    }
    void MakeCsv()
    {
        /*if (timeLog == null || timeLog.Count == 0)
        {
            Debug.Log("No Contamination");
            using (FileStream fs = new FileStream("Assets/ContaminationTime.csv", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode))
            {
                sw.WriteLine("TimeStamp,Contamination");
                sw.WriteLine(string.Format("No time, "));
            }
            return;
        }*/

        try
        {
            using (StreamWriter sw = new StreamWriter("Assets/ContaminationTime.csv", false, Encoding.UTF8))
            {
                sw.WriteLine("TimeStamp,Contamination");
                var list = timeLog;
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
