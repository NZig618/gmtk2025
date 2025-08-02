using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float startTime = 30;
    float currentTime;

    void Start()
    {
        currentTime = startTime;
    }

    public Animator fadeAnim;
    public float fadeTime = 1f;

    // Update is called once per frame
    void Update()
    {

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        //Death
        if (currentTime <= 0)
        {
            currentTime = 0;
            fadeAnim.Play("FadeToBlack");
            StartCoroutine(DelayTimerDeath());
        }
        //Print
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int miliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}", seconds, miliseconds);

    }
    
    IEnumerator DelayTimerDeath()
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Assets/Scenes/Death Screen.unity");
    }
}
