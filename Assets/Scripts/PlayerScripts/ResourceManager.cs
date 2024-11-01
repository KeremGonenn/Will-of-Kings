using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI swordText;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI paperText;

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
