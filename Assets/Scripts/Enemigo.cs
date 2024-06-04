using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public GameObject enemigo, Jugador;

    private void Start()
    {
       
        enemigo.GetComponent<NavMeshAgent>().SetDestination(Jugador.transform.position);
    }


    private void Update()
    {
        if (Jugador != null)
        {

            enemigo.GetComponent<NavMeshAgent>().SetDestination(Jugador.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemigo.GetComponent<NavMeshAgent>().speed = 6f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(dejarDeCorrer());
        }
    }

    IEnumerator dejarDeCorrer()
    {
        yield return new WaitForSeconds(1.5f);
       
            enemigo.GetComponent<NavMeshAgent>().speed = 3.5f;
        
    }
}
