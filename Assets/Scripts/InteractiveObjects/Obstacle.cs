using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject player;

    private PlayerScript script;

    public void Start()
    {
        this.script = (PlayerScript)player.GetComponent(typeof(PlayerScript));
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            this.script.IsAlive = false;
        }
    }
}
