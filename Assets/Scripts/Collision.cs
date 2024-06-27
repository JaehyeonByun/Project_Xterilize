using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class Collision : MonoBehaviour
{
    public Material Contamination;
    public Material HitEffect;
    private List<GameObject> contaminatedObjects = new List<GameObject>();
    private Dictionary<GameObject, Coroutine> runningCoroutines = new Dictionary<GameObject, Coroutine>();
    private HashSet<GameObject> runningBlinkEffects = new HashSet<GameObject>();

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        GameObject collidedObject = collision.gameObject;

        string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
        Debug.Log("Current Time: " + currentTime);
        GameManager.timeLog.Add(currentTime);
        GameManager.CountLog += 1;
        GameManager.WhyLog.Add(this.gameObject.name);

        if (this.gameObject.CompareTag("LeftHand") || this.gameObject.CompareTag("RightHand"))
        {
            Debug.Log("Collision detected with hand.");

            if (collidedObject.CompareTag("NotConta"))
            {
                Debug.Log("Collision with NotConta object.");
            }
            else
            {
                Debug.Log("Collision detected with " + this.gameObject.name);
                contaminatedObjects.Add(collidedObject);
                StartBlinkCoroutine(collidedObject, Contamination);
            }
        }
        else if (this.gameObject.CompareTag("Body"))
        {
            if (collidedObject.CompareTag("NotConta"))
            {
                Debug.Log("Collision with NotConta object.");
            }
            else
            {
                Debug.Log("Collision detected with " + this.gameObject.name);
                contaminatedObjects.Add(collidedObject);
                StartBlinkCoroutine(collidedObject, HitEffect);
            }
        }
    }

    private void StartBlinkCoroutine(GameObject obj, Material overlayMaterial)
    {
        if (runningBlinkEffects.Contains(obj))
        {
            Debug.Log("Coroutine is already running for this object.");
            return;
        }

        Coroutine coroutine = StartCoroutine(BlinkOverlay(obj, overlayMaterial, 1f, 3));
        runningCoroutines[obj] = coroutine;
        runningBlinkEffects.Add(obj);
    }

    private IEnumerator BlinkOverlay(GameObject obj, Material overlayMaterial, float blinkDuration, int blinkCount)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer == null)
        {
            yield break;
        }

        Material[] originalMaterials = renderer.materials;

        for (int i = 0; i < blinkCount; i++)
        {
            ApplyOverlay(renderer, overlayMaterial);
            yield return new WaitForSeconds(blinkDuration);
            renderer.materials = originalMaterials;
            yield return new WaitForSeconds(blinkDuration);
        }

        renderer.materials = originalMaterials; // Ensure the object returns to its original material
        runningBlinkEffects.Remove(obj); // Remove from running blink effects set
        runningCoroutines.Remove(obj); // Remove from running coroutines dictionary
    }

    private void ApplyOverlay(Renderer renderer, Material overlayMaterial)
    {
        Material[] originalMaterials = renderer.materials;
        Material[] newMaterials = new Material[originalMaterials.Length + 1];
        originalMaterials.CopyTo(newMaterials, 0);
        newMaterials[originalMaterials.Length] = overlayMaterial;
        renderer.materials = newMaterials;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    MakeCsv(); 
        //}

        // When press the Feedback button, call function
        //if (Input.GetKeyDown(KeyCode.))
        //{
        //    ShowArrowsOnContaminatedObjects();
        //}
    }

    void MakeCsv()
    {
        string csvFilePath = "Assets/CsvData/ContaminationTime.csv";

        try
        {
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("TimeStamp,Contamination");
                var list = GameManager.timeLog;
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
