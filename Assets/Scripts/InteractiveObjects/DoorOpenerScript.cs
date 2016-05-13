using UnityEngine;

public class DoorOpenerScript : MonoBehaviour
{
    //public float time;

    public GameObject objectToDestroy;

    //private GameObject mainCamera;
    //private GameObject target;

    //private bool isTriggered;
    //private float initalX;
    //private float initalY;

    //public void Start()
    //{
    //    this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    //    this.target = GameObject.FindGameObjectWithTag("Door");
    //    this.initalX = mainCamera.transform.position.x;
    //    this.initalY = mainCamera.transform.position.y;
    //}

    //public void Update()
    //{
    //    if (isTriggered)
    //    {
    //        if (this.time <= 0)
    //        {
    //            this.ChangeDirection(initalX, initalY);
               
    //        }

    //        this.time -= Time.deltaTime;
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if (!isTriggered)
        //{
        //    this.isTriggered = true;
        //    var x = this.target.transform.position.x;
        //    var y = this.target.transform.position.y;
        //    this.ChangeDirection(x, y);
        //    ((Camera) this.mainCamera).
        //}
        Destroy(this.GetComponent<ParticleSystem>());
        Destroy(this.gameObject);
        Destroy(this.objectToDestroy.gameObject);
    }

    //private void ChangeDirection(float x, float y)
    //{
    //    var position = this.mainCamera.transform.position;

    //    position.x = x;
    //    position.y = y;

    //    this.mainCamera.transform.position = position;
    //}
}