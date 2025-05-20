using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Image hpBar;
    public Image staminaBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI itemText;
    public string itemString;
    public float healthf;
    public float staminaf;
    public float ammof;

    private void Update()
    {
        hpBar.fillAmount = healthf / 150;
        staminaBar.fillAmount = staminaf / 100;
        hpText.text = healthf.ToString();
        ammoText.text = ammof.ToString();
        itemText.text = itemString;
    }
}
