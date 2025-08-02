using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float startTime = 30;
    float currentTime;

    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int miliseconds = Mathf.FloorToInt((currentTime * 100) % 60);
        timerText.text = string.Format("{0:00}:{1:00}", seconds, miliseconds);
    }
}
