using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionReturn : MonoBehaviour
{
    public GameObject EndUI;
    public GameObject OriginPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftHand" || other.tag == "RightHand") {

            EndUI.SetActive(true);
            OriginPos.SetActive(false);
        }; 
    }
}
