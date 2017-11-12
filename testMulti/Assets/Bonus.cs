using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        print("touche bonus");
        //Récupération de l'objet touché par la ball
         var hit = collision.gameObject;
         var speed = hit.GetComponent<PlayerController>();
        speed.addSpeed(5.0f);
        
        Destroy(gameObject);
    }
}