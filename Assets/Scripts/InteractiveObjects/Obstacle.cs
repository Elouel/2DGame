using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject player;

    private MoveScript script;

    void Start()
    {
        this.script = (MoveScript)player.GetComponent(typeof(MoveScript));
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        this.script.IsAlive = false;
    }
}
