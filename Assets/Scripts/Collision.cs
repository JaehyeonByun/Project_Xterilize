using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;
using System.Text;

public class Collision : MonoBehaviour
{

    public Material Contamination;
    public GameObject arrow_down; 
    private List<GameObject> contaminatedObjects = new List<GameObject>();

    private Canvas hitEffectCanvas;
    private Image hitEffectImage;

    private void Start()
    {
        // load arrow
        arrow_down = Resources.Load<GameObject>("Prefabs/Arrow_down");

        // hit effect Canvas
        GameObject canvasGameObject = new GameObject("HitEffectCanvas");
        hitEffectCanvas = canvasGameObject.AddComponent<Canvas>();
        hitEffectCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        hitEffectCanvas.sortingOrder = 100;

        // hit effect Image
        GameObject imageGameObject = new GameObject("HitEffectImage");
        imageGameObject.transform.SetParent(canvasGameObject.transform);
        hitEffectImage = imageGameObject.AddComponent<Image>();
        hitEffectImage.color = new Color(1, 0, 0, 0.3f); 
        hitEffectImage.rectTransform.anchorMin = new Vector2(0, 0);
        hitEffectImage.rectTransform.anchorMax = new Vector2(1, 1);
        hitEffectImage.rectTransform.offsetMin = Vector2.zero;
        hitEffectImage.rectTransform.offsetMax = Vector2.zero;

        hitEffectCanvas.enabled = false;
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    { 
        GameObject collidedObject = collision.gameObject;

        string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
        Debug.Log("Current Time: " + currentTime);
        GameManager.timeLog.Add(currentTime);
        GameManager.CountLog += 1;
        GameManager.WhyLog.Add(this.gameObject.name);

        if (collidedObject.CompareTag("NotConta"))
        {
            //Debug.Log("Collision With NotConta Object!");
        }
        else
        {
            if (this.gameObject.CompareTag("Hand"))
            {
                collidedObject.tag = "HandConta";

                AddOverlay addOverlay = collidedObject.AddComponent<AddOverlay>();
                addOverlay.overlayMaterial = Contamination;
                addOverlay.ApplyOverlay();

                contaminatedObjects.Add(collidedObject);

                StartCoroutine(BlinkColor(collidedObject, Contamination, 1f, 3));
            }

            else if (this.gameObject.CompareTag("Body"))
            {
                collidedObject.tag = "BodyConta";

                contaminatedObjects.Add(collidedObject);

                StartCoroutine(ShowHitEffect(1f));
            }
        }
    }

    private IEnumerator BlinkColor(GameObject obj, Material contaminationMaterial, float blinkDuration, int blinkCount)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Material originalMaterial = renderer.material;

        for (int i = 0; i < blinkCount; i++)
        {
            renderer.material = contaminationMaterial;
            yield return new WaitForSeconds(blinkDuration);
            renderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private IEnumerator ShowHitEffect(float duration)
    {
        hitEffectCanvas.enabled = true;

        yield return new WaitForSeconds(duration);

        hitEffectCanvas.enabled = false;
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

    // If press the feedback button, 
    void ShowArrowsOnContaminatedObjects()
    {
        foreach (GameObject obj in contaminatedObjects)
        {
            // Attach arrow to objects of Contalist
            Instantiate(arrow_down, obj.transform.position + Vector3.up * 2, Quaternion.identity);
        }
    }
}

