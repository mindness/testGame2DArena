using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    int vitesseMax = 17;
    void OnCollisionEnter(Collision collision)
    {

        print("touche bonus");
        //Récupération de l'objet touché par la ball
        var hit = collision.gameObject;
        var speed = hit.GetComponent<PlayerController>();

        if (speed.speedPlayer < vitesseMax)
            speed.addSpeed(5.0f);

        Destroy(gameObject);
    }
}