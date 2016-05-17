using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    public GameObject objectToMove;
    public float moveSpeed;
    public Transform[] points;
    public int pointSelection;
    public bool loop = true;

    private Transform currentPoint;
    private BackgroundParallax parallaxScript;
    private Transform cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];
        parallaxScript = (BackgroundParallax)FindObjectOfType(typeof(BackgroundParallax));
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        if (objectToMove.transform.position == currentPoint.position)
        {
            pointSelection++;
            if (loop && pointSelection == points.Length)
            {
                pointSelection = 0;
            }
            currentPoint = points[pointSelection];
        }
    }
}
