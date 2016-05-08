using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayGame : MonoBehaviour {

	// Use this for initialization

    public void ChangeToSceen(string sceen)
    {
        SceneManager.LoadScene(sceen);
    }
}
