using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NewsManager : MonoBehaviour
{
    public TextMeshProUGUI newsText;
    public ResourceManager resourceManager;

    // Maksimum mesaj say�s�
    private const int maxMessages = 15;

    // Mesajlar� tutan liste
    private List<string> messageHistory = new List<string>();

    private string[] lowPaperMessages = { "10 Asker kaybettin", "10 Yiyecek kaybettin", "10 Alt�n �al�nd�", "Her �ey yolunda", "Her �ey yolunda" };
    private string[] lowSwordMessages = { "10 Refah kaybettin", "10 Yiyecek kaybettin", "10 Alt�n �al�nd�", "Her �ey yolunda", "Her �ey yolunda" };
    private string[] lowAppleMessages = { "15 Asker kaybettin", "15 Refah kaybettin", "Her �ey yolunda", "Her �ey yolunda" };
    private string[] lowCoinMessages = { "10 Asker kaybettin", "10 Refah kaybettin", "10 Yiyecek kaybettin", "Her �ey yolunda", "Her �ey yolunda" };

    // Rastgele bir mesaj se�ip, bu mesaj�n etkisini kaynaklara uygular
    public void ApplyRandomEffect(string[] messageArray)
    {
        string randomMessage = messageArray[Random.Range(0, messageArray.Length)];

        // Mesaj� listeye ekleyin
        AddNewsMessage(randomMessage);

        // Mesaj�n etkisini kaynaklara uygula
        switch (randomMessage)
        {
            case "10 Asker kaybettin":
                resourceManager.UpdateResource(resourceManager.swordText, -10);
                break;
            case "10 Yiyecek kaybettin":
                resourceManager.UpdateResource(resourceManager.appleText, -10);
                break;
            case "10 Alt�n �al�nd�":
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
            // "Her �ey yolunda" oldu�unda hi�bir �ey yapma
            default:
                break;
        }
    }

    // Yeni mesaj ekleyen ve eski mesajlar� silen fonksiyon
    private void AddNewsMessage(string newMessage)
    {
        // Listeye yeni mesaj ekle
        messageHistory.Add(newMessage);

        // E�er mesaj say�s� 10'u ge�tiyse en eski mesaj� kald�r
        if (messageHistory.Count > maxMessages)
        {
            messageHistory.RemoveAt(0);  // En eski mesaj� listeden kald�r
        }

        // Haber alan�n� g�ncelle
        UpdateNewsText();
    }

    // Haberler k�sm�n� g�ncelleyen fonksiyon
    private void UpdateNewsText()
    {
        newsText.text = "";  // Mevcut metni s�f�rla
        foreach (string message in messageHistory)
        {
            newsText.text += message + "\n";  // Her mesaj� ekle
        }
    }

    // Mesaj dizilerini d�nd�ren fonksiyonlar
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
