using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameOver : MonoBehaviour
{
    public GameObject UI;

    public VideoPlayer videoPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            UI.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(RecargarEscena());
        }
    }


    IEnumerator RecargarEscena()
    {
        float duracionVideo = (float)videoPlayer.clip.length;


        yield return new WaitForSeconds(duracionVideo);
        SceneManager.LoadScene("Nivel2");
    }
}
