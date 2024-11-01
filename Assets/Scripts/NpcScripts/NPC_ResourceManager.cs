using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPC_ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI featherText;
    public TextMeshProUGUI swordText;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI tradeButtonText;
    public TextMeshProUGUI warButtonText;
    public TextMeshProUGUI allianceButtonText;
    public TextMeshProUGUI enemyList1;
    public TextMeshProUGUI enemyList2;
    public TextMeshProUGUI enemyList3;

    // Kaynaklarý güncelleyen yardýmcý fonksiyon
    public void UpdateResource(TextMeshProUGUI resourceText, int changeAmount)
    {
        int value;
        if (int.TryParse(resourceText.text, out value))
        {
            value += changeAmount;
            // Deðerin negatif olmasýný engeller
            resourceText.text = Mathf.Max(0, value).ToString();
        }
    }

    public int GetResourceValue(TextMeshProUGUI resourceText)
    {
        int value;
        if (int.TryParse(resourceText.text, out value))
        {
            return value;
        }
        return 0;
    }
}
