using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject PlayUI;

    public Animator animator;
    public AudioSource audioSource;

    public void OnClick()
    {
        StartUI.SetActive(false);
        PlayUI.SetActive(true);
        Invoke("pull", 10.0f);
    }
    public void pull()
    {
        animator.SetBool("Pull", true);
        audioSource.Play();
        Invoke("Res", 2.0f);
    }

    // Update is called once per frame
    public void Res()
    {
        animator.SetBool("Pull", false);
    }
}
