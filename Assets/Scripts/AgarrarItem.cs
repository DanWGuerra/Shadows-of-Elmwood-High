using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class AgarrarItem : MonoBehaviour
{

    public TextMeshProUGUI contador;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.ItemsAgarrados++;
            contador.text = GameManager.ItemsAgarrados.ToString();

            Destroy(gameObject);
        }
    }


}
