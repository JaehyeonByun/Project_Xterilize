using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorPull : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void pull()
    {
        animator.SetBool("Pull", true);
        audioSource.Play();
        Invoke("Res", 3.0f);
  
    }

    // Update is called once per frame
    public void Res()
    {
        animator.SetBool("Pull", false);
    }
}
