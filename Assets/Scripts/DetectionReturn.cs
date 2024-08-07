using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionReturn : MonoBehaviour
{
    public GameObject EndUI;
    public GameObject DefaultUI;
    public GameObject OriginPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body" || other.tag == "LeftHand" || other.tag == "RightHand") {

            EndUI.SetActive(true);
            DefaultUI.SetActive(false);
            OriginPos.SetActive(false);
        }; 
    }
}
