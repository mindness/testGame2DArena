using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;
    public bool destroyOnDeath;

    private NetworkStartPosition[] spawnPoints;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    private void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }
    // Prendre des dégats
    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }
        // On baisse la vie actuelle selon le nombre de dégats
        currentHealth -= amount;

        //Si la vie actuelle ateint 0 le personnage meurt
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;
                RpcRespawn();
            }
        }
    }

    public void addHealth(int amount)
    {
        if (!isServer)
        {
            return;
        }
        // On augmente la vie actuelle selon le nombre de soin
        currentHealth += amount;
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        }
    }
    // point de vie player courant 
    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Vie : " + healthBar.sizeDelta.x + " / " + maxHealth);
            
        }

    }
}



