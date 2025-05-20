using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System;

public class player : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(150);
    public int[] ammo;
    public int item;
    public GameObject[] items;
    public GameObject deathUI;
    public GameObject playerObject;
    public GameObject HUDObject;

    void Start()
    {
        HUDObject = GameObject.Find("HUD");
    }

    void Update()
    {
        HUDObject.GetComponent<HUD>().healthf = health.Value;
        HUDObject.GetComponent<HUD>().staminaf = GetComponent<movement>().stamina;
        HUDObject.GetComponent<HUD>().itemString = items[item].name;
        HUDObject.GetComponent<HUD>().ammof = ammo[item];

        if (health.Value <= 0)
        {
            deathUI.SetActive(true);
            GetComponent<movement>().enabled = false;
        }
    }

    public void Respawn()
    {
        health.Value = 150;
        deathUI.SetActive(false);
        GetComponent<movement>().enabled = true;
    }
}
