using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPositionObserver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerContaminationArea")
        {
            Debug.Log("Contamination Pose!");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
