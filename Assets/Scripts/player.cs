using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class player : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(150);
    public NetworkVariable<int> score = new NetworkVariable<int>(0);
    public NetworkVariable<int> totalScore = new NetworkVariable<int>(0);
    public int[] ammo;
    public int item;
    public GameObject[] items;
    public GameObject deathUI;
    public GameObject HUDObject;
    public bool initialized = false;
    GameObject camera;
    bool dead = false;

    public GameObject pencil;
    public GameObject image;
    public float forceMultiplier = 1;
    float force;

    private void Start()
    {
        deathUI = GameObject.Find("DeathUI");
        deathUI.SetActive(false);
    }

    public void Initialize()
    {
        health.Value = 150;
        item = 0;
        score.Value = 0;
        initialized = true;
        image = GameObject.Find("radial");
    }

    public void Respawn()
    {
        dead = false;
        deathUI.SetActive(false);
        health.Value = 150;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        GetComponent<movement>().enabled = true;
    }

    public void die()
    {
        dead = true;
        score.Value -= 10;
        totalScore.Value -= 10;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<movement>().enabled = false;
        StartCoroutine(respawnCoroutine());
    }

    void Update()
    {
        if (initialized)
        {
            HUDObject = GameObject.FindGameObjectWithTag("HUD");
            HUDObject.GetComponent<HUD>().healthf = health.Value;
            HUDObject.GetComponent<HUD>().staminaf = GetComponent<movement>().stamina;

            if (health.Value <= 0 && dead == false)
            {
                die();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pencil")
        {
            health.Value -= 100;
        }
        if (collision.gameObject.tag == "Scissors")
        {
            health.Value -= 50;
        }
    }

    public IEnumerator respawnCoroutine()
    {
        int seconds = 10;
        while (seconds > 0)
        {
            Debug.Log("Counting down till respawn " + seconds);
            yield return new WaitForSecondsRealtime(1);
            seconds--;
            deathUI.SetActive(true);
            GameObject t = GameObject.Find("countdownText");
            t.GetComponent<TextMeshProUGUI>().text = seconds.ToString();
        }
        Respawn();
    }
}
