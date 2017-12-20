using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControllers : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    [Header("Movemment Variables")]
    public float speedPlayer = 7f;
    public float speedRoationPlayer = 250.0f;

    
    float bulletSpeed = 15f;
    float bulletRange = 1f;

    Rigidbody localRigidbody;


    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        localRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!isLocalPlayer)
         {
             return;
         }
        // speed qd et mvt
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        // speed zs et mvt
        float z = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 deltaTranslation = transform.position + transform.forward * speedPlayer * z * Time.deltaTime;
        localRigidbody.MovePosition(deltaTranslation);

        Quaternion deltaRotation = Quaternion.Euler(speedRoationPlayer * new Vector3(0, x, 0) * Time.deltaTime);
        localRigidbody.MoveRotation(localRigidbody.rotation * deltaRotation);

        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);

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
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // faire apparaitre la balle sur les clients
        NetworkServer.Spawn(bullet);
        
        // Destruction de la balle après x secondes
        Destroy(bullet, bulletRange);
 
    }
 

    public void AddSpeedPlayer(float amount)
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