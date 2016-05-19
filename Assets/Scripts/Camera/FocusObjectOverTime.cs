using UnityEngine;
using System.Collections;

public class FocusObjectOverTime : MonoBehaviour
{
    private Transform cam;

    public float msToGoToObject;
    public float msToKeepFocus;
    public GameObject focusObject;
    private MonoBehaviour camScript;
    private Vector3 velocity;
    private Vector3 targetVector;

    void Start()
    {
        cam = Camera.main.transform;
        camScript = cam.GetComponent("CameraController") as MonoBehaviour;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        camScript.enabled = false;
        StartCoroutine(Transition());
        targetVector = new Vector3(focusObject.transform.position.x, focusObject.transform.position.y, -10);
        Debug.Log(msToGoToObject);
        //cam.position = Vector3.Lerp(cam.position, targetVector, msToGoToObject);
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            //t += Time.deltaTime * (Time.timeScale / transitionDuration);


            cam.position = Vector3.Lerp(cam.position, targetVector, msToGoToObject);
            yield return 0;
        }

    }
}
