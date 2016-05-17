using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xMargin = 1f;      // Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f;      // Distance in the y axis the player can move before the camera follows.
    public float xSmooth = 2f;      // How smoothly the camera catches up with it's target movement in the x axis.
    public float ySmooth = 2f;      // How smoothly the camera catches up with it's target movement in the y axis.
    public GameObject maxCameraXY;
    public GameObject minCameraXY;

    private Vector2 maxXAndY;        // The maximum x and y coordinates the camera can have.
    private Vector2 minXAndY;        // The minimum x and y coordinates the camera can have.
    


    private Transform player;       // Reference to the player's transform.

    public void Awake()
    {
        // Setting up the reference.
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        var cameraBounds = Camera.main.OrthographicBounds();
        var camXOffset = (Mathf.Abs(cameraBounds.max.x) - Mathf.Abs(cameraBounds.min.x)) / 2;
        var camYOffset = (Mathf.Abs(cameraBounds.max.y) - Mathf.Abs(cameraBounds.min.y)) / 2;
        maxXAndY = new Vector2(maxCameraXY.transform.position.x - camXOffset, maxCameraXY.transform.position.y - camYOffset);
        Debug.Log(maxXAndY);
        minXAndY = new Vector2(minCameraXY.transform.position.x - camXOffset, minCameraXY.transform.position.y - camYOffset);
        Debug.Log(minXAndY);
    }

    public void LateUpdate()
    {
        this.TrackPlayer();
    }

    private bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    private bool CheckYMargin()
    {
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    private void TrackPlayer()
    {
        // By default the target x and y coordinates of the camera are it's current x and y coordinates.
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // If the player has moved beyond the x margin...
        if (CheckXMargin())
        {
            // ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }
        // If the player has moved beyond the y margin...
        if (CheckYMargin())
        {
            // ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }

        // The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
