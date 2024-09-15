using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;

public class ReadLog : MonoBehaviour
{
    public TMP_Text myText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ReadCsv();
        }
    }
    // Start is called before the first frame update
    void ReadCsv()
    {
        string allText = "";
        foreach (string item in GameManager.timeLog)
        {
            allText += item + "\n"; // �� �׸� �ڿ� �� �ٲ� ���ڸ� �߰��մϴ�.
        }
        myText.text = allText;
    }
}
