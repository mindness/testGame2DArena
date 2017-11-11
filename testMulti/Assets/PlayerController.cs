using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public int speedBullet =  7;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // speed qd et mvt
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
        // speed zs et mvt
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 7.0f;

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


       /* if (this.transform.position.x == bullet.transform.position.x && this.transform.position.y == bullet.transform.position.y && this.transform.position.z == bullet.transform.position.z)
        {
            Destroy(bullet);
        }*/
        // Destruction de la balle après 2 secondes
        Destroy(bullet, 2.0f);
    }


    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}