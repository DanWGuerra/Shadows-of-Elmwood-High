using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public string TipoPowerUp;

    BoxCollider collider;
    MeshRenderer renderer;
    public PowerUpPlayer playerPowerUp;


    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Rotate the GameObject around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReactivarPowerUp());

            collider.enabled = false;
            renderer.enabled = false;

            if (!playerPowerUp.hasPowerUp)
            {
                GivePowerUp();
            }
            
        }
    }
 

    private void GivePowerUp()
    {
        playerPowerUp.TienePowerUp(TipoPowerUp);
    }

    IEnumerator ReactivarPowerUp()
    {
        yield return new WaitForSeconds(5f);
        collider.enabled = true;
        renderer.enabled = true;
    }
}
