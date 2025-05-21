using UnityEngine;
using Unity.Netcode;
using System.Collections;
using TMPro;

public class roundManager : NetworkBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject startUI;
    public GameObject startGameUI;
    public GameObject endUI;
    public GameObject hud;
    protected GameObject[] players;
    public int qualityPreset = 0;
    [Header("Quality Preset Objects")]
    public GameObject low;
    public GameObject medium;
    public GameObject high;
    [Header("Timer and UI")]
    public TextMeshProUGUI timerText;
    public NetworkVariable<int> minutes = new NetworkVariable<int>(2);
    public NetworkVariable<int> seconds = new NetworkVariable<int>(30);

    public void SetQualityPreset(int quality)
    {
        qualityPreset = quality;
    }

    public void StartGame()
    {
        startGameUI.SetActive(false);

        #region SET QUALITY PRESETS
        switch (qualityPreset)
        {
            case 0:
                low.SetActive(false);
                medium.SetActive(false);
                high.SetActive(false);
                break;
            case 1:
                low.SetActive(true);
                medium.SetActive(false);
                high.SetActive(false);
                break;
            case 2:
                low.SetActive(true);
                medium.SetActive(true);
                high.SetActive(false);
                break;
            case 3:
                low.SetActive(true);
                medium.SetActive(true);
                high.SetActive(true);
                break;
        }
        #endregion

        #region SPAWNING
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (i < players.Length)
            {
                players[i].transform.position = spawnLocations[i].transform.position;
                Debug.Log("Set position for: " + players[i].name + " at spawn point " + spawnLocations[i].name);
            }
            else
            {
                Debug.LogWarning("Not enough players to fill all spawn locations.");
                break;
            }
        }
        #endregion

        minutes.Value = 2; seconds.Value = 30;
        StartCoroutine(startDelay());
    }

    void OnEnable()
    {
        minutes.OnValueChanged += UpdateTimerText;
        seconds.OnValueChanged += UpdateTimerText;
    }

    void OnDisable()
    {
        minutes.OnValueChanged -= UpdateTimerText;
        seconds.OnValueChanged -= UpdateTimerText;
    }

    private void UpdateTimerText(int oldValue, int newValue)
    {
        timerText.text = minutes.Value.ToString("00") + ":" + seconds.Value.ToString("00");
    }


    public void allowMovement(bool allow = true)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                Debug.Log(players[i]);
                players[i].GetComponent<movement>().enabled = allow;
                players[i].GetComponent<player>().Initialize();
                Debug.Log("Initialized player " + players[i].name);
            }
        }
    }

    public IEnumerator startDelay()
    {
        startUI.SetActive(true);
        yield return new WaitForSeconds(3);
        startUI.SetActive(false);
        hud.SetActive(true);
        GameObject.Find("Main Camera").SetActive(false);
        yield return new WaitForEndOfFrame();
        allowMovement(true);
        Debug.Log("Game Started!");
        StartCoroutine(roundTimer());
    }

    public IEnumerator roundTimer()
    {
        Debug.Log("Round Timer Started!");
        while (minutes.Value > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            seconds.Value--;
            if (seconds.Value < 0)
            {
                minutes.Value--;
                seconds.Value = 59;
            }
        }
        endRound();
    }

    public void endRound()
    {
        allowMovement(false);
        endUI.SetActive(true);
    }
}