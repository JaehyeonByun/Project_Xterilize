using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(collision collision)
    {
        if (collision.GetComponent<Collider>().gameObject.CompareTag("player")) 
        {
            Debug.Log("Player Contaminated!");
        }
    }
}
