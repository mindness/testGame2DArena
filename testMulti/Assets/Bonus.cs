using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    int vitesseMax = 15;
    int rand;


    private void Start()
    {
        rand = Random.Range(1,3);
        switch (rand)
        {
            case 1:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 2:
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        if (!hit.tag.Equals("Player")) { return; }
        switch (rand)
        {
            case 1:
                var player = hit.GetComponent<PlayerControllers>();
                if (player.speedPlayer < vitesseMax )
                {
                    player.AddSpeedPlayer(5.0f);
                }
                break;
            case 2:
                var healthPlayer = hit.GetComponent<Health>();
                if (healthPlayer.currentHealth < 120)
                {
                    healthPlayer.addHealth(10);
                }
               
                break;
        }
      
        //on supprime l objet
        Destroy(gameObject);
    }
}