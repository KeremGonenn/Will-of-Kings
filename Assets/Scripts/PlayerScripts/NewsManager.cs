using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NewsManager : MonoBehaviour
{
    public TextMeshProUGUI newsText;
    public ResourceManager resourceManager;

    // Maksimum mesaj sayýsý
    private const int maxMessages = 15;

    // Mesajlarý tutan liste
    private List<string> messageHistory = new List<string>();

    private string[] lowPaperMessages = { "10 Asker kaybettin", "10 Yiyecek kaybettin", "10 Altýn çalýndý", "Her þey yolunda", "Her þey yolunda" };
    private string[] lowSwordMessages = { "10 Refah kaybettin", "10 Yiyecek kaybettin", "10 Altýn çalýndý", "Her þey yolunda", "Her þey yolunda" };
    private string[] lowAppleMessages = { "15 Asker kaybettin", "15 Refah kaybettin", "Her þey yolunda", "Her þey yolunda" };
    private string[] lowCoinMessages = { "10 Asker kaybettin", "10 Refah kaybettin", "10 Yiyecek kaybettin", "Her þey yolunda", "Her þey yolunda" };

    // Rastgele bir mesaj seçip, bu mesajýn etkisini kaynaklara uygular
    public void ApplyRandomEffect(string[] messageArray)
    {
        string randomMessage = messageArray[Random.Range(0, messageArray.Length)];

        // Mesajý listeye ekleyin
        AddNewsMessage(randomMessage);

        // Mesajýn etkisini kaynaklara uygula
        switch (randomMessage)
        {
            case "10 Asker kaybettin":
                resourceManager.UpdateResource(resourceManager.swordText, -10);
                break;
            case "10 Yiyecek kaybettin":
                resourceManager.UpdateResource(resourceManager.appleText, -10);
                break;
            case "10 Altýn çalýndý":
                resourceManager.UpdateResource(resourceManager.coinText, -10);
                break;
            case "10 Refah kaybettin":
                resourceManager.UpdateResource(resourceManager.paperText, -10);
                break;
            case "15 Asker kaybettin":
                resourceManager.UpdateResource(resourceManager.swordText, -15);
                break;
            case "15 Refah kaybettin":
                resourceManager.UpdateResource(resourceManager.paperText, -15);
                break;
            // "Her þey yolunda" olduðunda hiçbir þey yapma
            default:
                break;
        }
    }

    // Yeni mesaj ekleyen ve eski mesajlarý silen fonksiyon
    private void AddNewsMessage(string newMessage)
    {
        // Listeye yeni mesaj ekle
        messageHistory.Add(newMessage);

        // Eðer mesaj sayýsý 10'u geçtiyse en eski mesajý kaldýr
        if (messageHistory.Count > maxMessages)
        {
            messageHistory.RemoveAt(0);  // En eski mesajý listeden kaldýr
        }

        // Haber alanýný güncelle
        UpdateNewsText();
    }

    // Haberler kýsmýný güncelleyen fonksiyon
    private void UpdateNewsText()
    {
        newsText.text = "";  // Mevcut metni sýfýrla
        foreach (string message in messageHistory)
        {
            newsText.text += message + "\n";  // Her mesajý ekle
        }
    }

    // Mesaj dizilerini döndüren fonksiyonlar
    public string[] GetLowPaperMessages()
    {
        return lowPaperMessages;
    }

    public string[] GetLowSwordMessages()
    {
        return lowSwordMessages;
    }

    public string[] GetLowAppleMessages()
    {
        return lowAppleMessages;
    }

    public string[] GetLowCoinMessages()
    {
        return lowCoinMessages;
    }
}
