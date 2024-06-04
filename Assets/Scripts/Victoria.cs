using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : MonoBehaviour
{
    public CinemachineBrain virtualCamera;
    public GameObject cineMachine, CameraAnimacion, enemigo, UIGanaste;
    public FirstPersonController personController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && GameManager.ItemsAgarrados == 3)
        {
            virtualCamera.enabled = false;
            cineMachine.SetActive(false);
            personController.enabled = false;
            enemigo.SetActive(false);
            UIGanaste.SetActive(true);


            CameraAnimacion.GetComponent<Animator>().Play("Victoria");
        }
    }
}
