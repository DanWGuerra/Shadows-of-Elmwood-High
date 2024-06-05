using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{

    [SerializeField] GameObject Flashlight, Flashlight_Light;
    [SerializeField] AudioSource LightSoundOff;
    Animator FlashlightAnimator;

    private void Start()
    {
        FlashlightAnimator = Flashlight.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
        
    }


    void ToggleFlashlight()
    {
        if (Flashlight.activeSelf)
        {
            StartCoroutine(TurnOffFlashlight());
            
        }
        else
        {
            Flashlight.SetActive(true);
            Flashlight_Light.SetActive(true);
        }
    }

    IEnumerator TurnOffFlashlight()
    {
        LightSoundOff.Play();
        Flashlight_Light.SetActive(false);
        FlashlightAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        Flashlight.SetActive(false);
        FlashlightAnimator.ResetTrigger("Hide");
    }
}
