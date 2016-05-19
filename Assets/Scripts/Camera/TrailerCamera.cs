using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailerCamera : MonoBehaviour
{
    float currentLerpTime;
    private Transform cam;
    private CameraController camScript;
    private Vector3 targetPosition;

    protected bool finishedTrailer = false;

    public float lerpTime;
    public float secondsToFocus;
    public GameObject focusObject;
    private Vector3 startPosition;
    private bool isLerping;
    public List<MonoBehaviour> scriptsToEnable;

    protected void Start()
    {
        cam = Camera.main.transform;
        camScript = cam.GetComponent<CameraController>();
        Debug.Log(camScript);
    }

    protected void Update()
    {
        if (isLerping)
        {
            if (startPosition == Vector3.zero)
            {
                startPosition = cam.position;
                CalculateTargetPosition(); 
            }
            StartCoroutine(Transition());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isLerping = true;
        camScript.enabled = false;
        //StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;
        //perc = perc * perc * (3f - 2f * perc);
        //t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f)
        //t = t*t
        //t = t*t * (3f - 2f*t)
        //t = t*t*t * (t * (6f*t - 15f) + 10f)
        cam.position = Vector3.Lerp(startPosition, targetPosition, perc);
        if (perc >= 1)
        {
            isLerping = false;
            foreach (var c in scriptsToEnable)
            {
                c.enabled = true;
            }
            yield return new WaitForSeconds(secondsToFocus);
            foreach (var c in scriptsToEnable)
            {
                c.enabled = false;
            }
            camScript.enabled = true;
            finishedTrailer = true;
        }
        yield return 0;
    }

    public void CalculateTargetPosition()
    {
        var targetX = Mathf.Clamp(focusObject.transform.position.x, camScript.minXAndY.x, camScript.maxXAndY.x);
        var targetY = Mathf.Clamp(focusObject.transform.position.y, camScript.minXAndY.y, camScript.maxXAndY.y);
        targetPosition = new Vector3(targetX, targetY, cam.position.z);
    }
}
