using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 newPlayerPosition;
    private Transform player;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            SceneManager.LoadScene(sceneToLoad);
            player.position = newPlayerPosition; //This is a TEMPORARY placement until we add fades!
        }
    }
}
