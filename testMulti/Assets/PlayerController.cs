using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public int speedBullet =  14;
    public float speedPlayer = 7f;
    public float speedRoationPlayer = 250.0f;


   
    void Update()
    {
        if (!isLocalPlayer)
         {
             return;
         }
        // speed qd et mvt
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speedRoationPlayer;
        // speed zs et mvt
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speedPlayer;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

    }

    [Command]
    void CmdFire()
    {
        // Création de la balle à partir de la prefab "BULLET"
        var bullet = (GameObject)Instantiate(
         bulletPrefab,
         bulletSpawn.position,
         bulletSpawn.rotation);
        


        // Ajout de velocité à la balle
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speedBullet;

        // faire apparaitre la balle sur les clients
        NetworkServer.Spawn(bullet);


 
        // Destruction de la balle après 2 secondes
        Destroy(bullet, 2.0f);
    }
 

    public void addSpeed(float amount)
    {
        if (!isServer && !isLocalPlayer)
        {
            return;
        }
        // On baisse la vie actuelle selon le nombre de dégats
        speedPlayer += amount;

    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
        

}