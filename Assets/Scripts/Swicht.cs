using UnityEngine;

public class Swicht : MonoBehaviour
{
    public Light lightSource;
    
    private bool playerNearby = false;

    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E)) // Se presiona "E" cerca del botón
        {
            ToggleLight();
        }
    }

    private void ToggleLight()
    {
        if (lightSource != null)
        {
            lightSource.enabled = !lightSource.enabled;
            

            Light1.SetActive(false);

            if (!lightSource.enabled)
            {
                MissionManager.Instance.CompleteMission(1); // Apagar la luz
                Light1.SetActive(false);
                Light2.SetActive(false);
                Light3.SetActive(false);
            }
            else
            {
                MissionManager.Instance.CompleteMission(2); // Encender la luz
                Light1.SetActive(true);
                Light2.SetActive(true);
                Light3.SetActive(true);
            }

            Debug.Log("Botón presionado. Luz: " + (lightSource.enabled ? "Encendida" : "Apagada"));
        }
    }
}

