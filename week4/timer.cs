using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    float timer;

    GameObject timerUI;
    int seconds = 0;
    int minutes = 0;
    void Start()
    {
        timer = 0.0f;
        timerUI = GameObject.Find("timerText");
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        //seconds = (int)timer;
        minutes = (int)(timer / 60);
        seconds = (int)(timer % 60);

        Debug.Log(minutes + " : " + seconds);
        timerUI.GetComponent<TextMeshProUGUI>().text = minutes.ToString("00") + " : " + seconds.ToString("00");

        if (timer > 120)
        {
            Debug.Log("Times up");
            SceneManager.LoadScene("week3");
        }
    }
}
