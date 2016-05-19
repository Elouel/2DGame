using UnityEngine;
using System.Collections;

public class DoorOpener : TrailerCamera
{
    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (finishedTrailer == true)
        {
            Destroy(this.GetComponent<ParticleSystem>());
            Destroy(this.gameObject);
            Destroy(this.focusObject.gameObject);
        }
    }
}
