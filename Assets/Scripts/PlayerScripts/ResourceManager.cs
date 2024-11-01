using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI swordText;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI paperText;

    // Kaynaklar� g�ncelleyen yard�mc� fonksiyon
    public void UpdateResource(TextMeshProUGUI resourceText, int changeAmount)
    {
        int value;
        if (int.TryParse(resourceText.text, out value))
        {
            value += changeAmount;
            // De�erin negatif olmas�n� engeller
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
