using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] objectsToBlink;
    public float blinkInterval = 0.5f; // Interval between blinks
    public float minBlinkDuration = 0.1f; // Minimum duration of a blink
    public float maxBlinkDuration = 0.3f; // Maximum duration of a blink

    private bool blinking = false;

    void Start()
    {
        StartBlink();
    }

    void StartBlink()
    {
        if (!blinking)
        {
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        blinking = true;

        while (true)
        {
            // Select a random object to blink
            GameObject objectToBlink = objectsToBlink[Random.Range(0, objectsToBlink.Length)];

            // Turn off the selected object
            objectToBlink.SetActive(false);

            // Wait for a random duration
            yield return new WaitForSeconds(Random.Range(minBlinkDuration, maxBlinkDuration));

            // Turn on the selected object
            objectToBlink.SetActive(true);

            // Wait for blink interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit button pressed");
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
