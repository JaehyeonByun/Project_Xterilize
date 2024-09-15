using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveCSV : MonoBehaviour
{
    //CSV 저장을 위한 테스트 코드
    private List<string> timeLog = new List<string>();

    // 시간 측정을 위한 변수
    private float timeElapsed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        // 1초가 경과하면 현재 시각을 리스트에 추가
        if (timeElapsed >= 1.0f)
        {
  
            string currentTime = System.DateTime.Now.ToString("HH시mm분ss초");
            timeLog.Add(currentTime);

            // 시간 초기화
            timeElapsed = 0.0f;

            // 로그 출력 (디버깅용)
            Debug.Log("Current Time: " + currentTime);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            MakeCsv(); // CSV 파일로 저장하는 함수 호출
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
