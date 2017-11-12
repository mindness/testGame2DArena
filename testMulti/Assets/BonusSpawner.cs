using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BonusSpawner : NetworkBehaviour
{
    public Vector3 center;
    public Vector3 size;

    public GameObject bonusPrefab;
    public int numberOfBonus;

     public override void OnStartServer()
     {
        // test function random time and spawn 
        StartCoroutine(WaitbeforeSpawn());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            SpawnBonus();
            
    }
    private void SpawnBonus()
    {
        var spawnPosition = new Vector3(Random.Range(-size.x/2, size.x / 2)+33, 0.6f, Random.Range(-size.z / 2, size.z / 2)+40);
        //var spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        var bonus = (GameObject)Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(bonus);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    // gestion attente / temps -> appel spawn
    IEnumerator WaitbeforeSpawn()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        SpawnBonus();
    }
}
