using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPlayer : MonoBehaviour
{
    public RawImage powerupImage;
    public List<Texture2D> powerupsImgs;

    public bool hasPowerUp = false;
    public string PowerUpQueTiene;

    public string layer1Name = "Layer1"; // Name of Layer 1
    public string layer2Name = "Layer2"; // Name of Layer 2

    GameObject MuroInstancia;

    private int layer1; // Layer 1 ID
    private int layer2; // Layer 2 ID


    private Rigidbody rb;

    // Variables para el power-up de velocidad

    public float velocidadAdicional = 15f; // Velocidad adicional proporcionada por el power-up


    // Variables para el power-up de obstáculo
    public GameObject wallPrefab; // Prefab del muro


    // Variables para el power-up de colisión
    private bool tieneColision = false;
    public float tiempoInicioColision;
    public float tiempoSinColision = 5f; // Tiempo en segundos sin colisiones


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        layer1 = LayerMask.NameToLayer(layer1Name);
        layer2 = LayerMask.NameToLayer(layer2Name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasPowerUp)
        {
            // Verificar si el jugador tiene alguno de los tipos de power-up especificados

            // Activar el power-up según su tipo
            ActivarPowerUp();

        }



        // Verificar si el tiempo sin colisión ha pasado para desactivar el power-up de colisión
        if (tieneColision && Time.time - tiempoInicioColision >= tiempoSinColision)
        {
            // Implementa tu lógica para restaurar la capacidad de colisión normal del jugador aquí
            tieneColision = false;
        }
    }

    public bool TienePowerUp(string tipo)
    {
        PowerUpQueTiene = tipo;
        hasPowerUp = true;
        CambiarIconoPwrUp();
        return hasPowerUp;
    }

    void ActivarPowerUp()
    {

        Debug.Log("Activando power-up: " + PowerUpQueTiene);

        if (PowerUpQueTiene == "Velocidad")
        {
            ActivarPowerUpVelocidad();
        }
        else if (PowerUpQueTiene == "Obstaculo")
        {
            ActivarPowerUpObstaculo();
        }
        else if (PowerUpQueTiene == "Colision")
        {
            ActivarPowerUpColision();
        }
        hasPowerUp = false;
        powerupImage.texture = null;
    }

    void ActivarPowerUpVelocidad()
    {
        // Dar velocidad adicional al jugador 
        rb.velocity = transform.forward * velocidadAdicional;
    }

    void ActivarPowerUpObstaculo()
    {
        // Colocar un muro detrás del jugador
         MuroInstancia = Instantiate(wallPrefab, transform.position - transform.forward * 5f, Quaternion.identity);
        Invoke("DestruirMuro", 5); // Invocar quitar muro despues de 5 segundos
        Debug.Log("Se ha colocado un muro detrás del jugador.");
    }

    void DestruirMuro()
    {
        Destroy(MuroInstancia);
    }


    void ActivarPowerUpColision()
    {
        // Desactivar las colisiones con otros jugadores durante un tiempo limitado
        Physics.IgnoreLayerCollision(layer1, layer2, true);
        Invoke("RevertCollision", tiempoSinColision); // Invoke the method to revert collision after duration
        Debug.Log("El jugador no puede chocar con otros jugadores durante " + tiempoSinColision + " segundos.");
    }

    void RevertCollision()
    {
        // Revert the collision between the two layers after the duration
        Physics.IgnoreLayerCollision(layer1, layer2, false);
    }



    public void CambiarIconoPwrUp()
    {

        if (PowerUpQueTiene == "Velocidad")
        {
            powerupImage.texture = powerupsImgs[0];
        }
        else if (PowerUpQueTiene == "Obstaculo")
        {
            powerupImage.texture = powerupsImgs[1];
        }
        else
        {
            powerupImage.texture = powerupsImgs[2];
        }
    }
}
