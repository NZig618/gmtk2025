using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public Vector2 newPlayerPosition;
    private Transform player;
    public float fadeTime = 1f;

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
            fadeAnim.Play("FadeToBlack");
            StartCoroutine(DelayFade(collision));
        }
    }

    IEnumerator DelayFade(Collider2D collision)
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
        player = collision.transform;
        player.position = newPlayerPosition; //This is a TEMPORARY placement until we add fades!
    }
}
