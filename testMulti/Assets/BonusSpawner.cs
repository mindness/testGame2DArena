using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BonusSpawner : NetworkBehaviour
{
    public Vector3 center;
    public Vector3 size;

    public GameObject bonusPrefab;
    // public int numberOfBonus = 5;
    public float sphereRadius = 0.5f;


    public override void OnStartServer()
    {
        // test function random time and spawn 
            StartCoroutine("WaitbeforeSpawn");

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            SpawnBonus();
            
    }
    private void SpawnBonus()
    {
        bool spawn = false;
        do
        {
            var spawnPosition = new Vector3(Random.Range(-size.x / 2, size.x / 2) + 33, 0.6f, Random.Range(-size.z / 2, size.z / 2) + 40);
            //var spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            if (!Physics.CheckSphere(spawnPosition, sphereRadius))
            {
                spawn = true;
                var bonus = (GameObject)Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
                NetworkServer.Spawn(bonus);
               // print("Created" + spawn + "    " + spawnPosition);
            }
           /* else
            {
                print("on wall test fhiufhufhbf" + spawn + "    " + spawnPosition);
            }*/
        } while (spawn == false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    // gestion attente / temps -> appel spawn
    IEnumerator WaitbeforeSpawn()
    {
        while (true)
        {
            int randomTimeSpawn = Random.Range(10,20); // random number to choose the time before spawning bonus
            print(randomTimeSpawn);

            // print(Time.time);
            yield return new WaitForSeconds(randomTimeSpawn);
            // print(Time.time);
            SpawnBonus();
        }
    }
}
