using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandPositionObserver : MonoBehaviour
{
    public GameObject ContaUI;
    public AudioSource Soundfeedback;


    private void OnTriggerEnter(Collider collider)
    {
        ContaUI.SetActive(false);
        if (collider.tag == "PlayerContaminationArea")
        {
            string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
            GameManager.timeLog.Add(currentTime);
            GameManager.CountLog += 1;
            GameManager.WhyLog.Add("오염 행동 발생");
            Debug.Log("Contamination Pose! " + collider.gameObject.name);
            ContaUI.SetActive(true);
            Invoke("UIFeedBackOff", 2.0f); // 추가됨
            Soundfeedback.Play();
        }
    }
    
    // Update is called once per frame
    void UIFeedBackOff() // 추가 함수 UI 안사라지면 강제 해재
    {
        ContaUI.SetActive(false);
    }
}