using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Contamination Detected! " + collider.gameObject.name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
    
