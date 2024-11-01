using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Kingdom_Manager : MonoBehaviour
{
    // UI Elementleri
    public TextMeshProUGUI swordText; // K�l�c�n yan�ndaki text (asker)
    public TextMeshProUGUI appleText; // Elman�n yan�ndaki text (yiyecek)
    public TextMeshProUGUI coinText;  // Paran�n yan�ndaki text
    public TextMeshProUGUI paperText; // Ka��d�n yan�ndaki text (refah)
    public TextMeshProUGUI newsText;  // Haberler k�sm�ndaki text

    // Butonlar
    public Button trainSoldierButton;
    public Button donateFarmerButton;
    public Button takeTaxButton;

    // Random mesajlar i�in diziler
    private string[] lowPaperMessages = { "10 Asker Kaybettin", "10 Yiyecek Kaybettin", "Hazineden 10 alt�n �al�nd�", "Her �ey yolunda", "Her �ey yolunda" };
    private string[] lowSwordMessages = { "10 Refah kaybettin", "10 Yiyecek kaybettin", "Hazineden 10 alt�n �al�nd�", "Her �ey yolunda", "Her �ey yolunda" };
    private string[] lowAppleMessages = { "15 Asker Kaybettin", "15 Refah Kaybettin", "Her �ey yolunda", "Her �ey yolunda" };
    private string[] lowCoinMessages = { "10 Asker Kaybettin", "10 Refah Kaybettin", "10 Yiyecek Kaybettin", "Her �ey yolunda", "Her �ey yolunda" };

    void Start()
    {
        // Butonlara i�levlerini ekle
        trainSoldierButton.onClick.AddListener(TrainSoldier);
        donateFarmerButton.onClick.AddListener(DonateFarmer);
        takeTaxButton.onClick.AddListener(TakeTax);
    }

    // Train Soldier i�lemi: K�l�� ve Ka��t de�erini 10 art�r�r
    public void TrainSoldier()
    {
        int swordValue = int.Parse(swordText.text) + 10;
        swordText.text = swordValue.ToString();

        int paperValue = int.Parse(paperText.text) + 10;
        paperText.text = paperValue.ToString();

        int coinValue = int.Parse(coinText.text) - 10;
        coinText.text = coinValue.ToString();

        CheckConditions(); // Ko�ullar� kontrol et
    }

    // Donate Farmer i�lemi: Elma ve Ka��t de�erini 10 art�r�r, Para de�erini 10 azalt�r
    public void DonateFarmer()
    {
        int appleValue = int.Parse(appleText.text) + 10;
        appleText.text = appleValue.ToString();

        int paperValue = int.Parse(paperText.text) + 10;
        paperText.text = paperValue.ToString();

        int coinValue = int.Parse(coinText.text) - 10;
        coinText.text = coinValue.ToString();

        CheckConditions(); // Ko�ullar� kontrol et
    }

    // Take Tax i�lemi: Para de�erini 10 art�r�r, Ka��t de�erini 10 azalt�r
    public void TakeTax()
    {
        int coinValue = int.Parse(coinText.text) + 10;
        coinText.text = coinValue.ToString();

        int paperValue = int.Parse(paperText.text) - 10;
        paperText.text = paperValue.ToString();

        CheckConditions(); // Ko�ullar� kontrol et
    }

    // Ko�ullar� kontrol eden fonksiyon
    private void CheckConditions()
    {
        int swordValue = int.Parse(swordText.text);
        int appleValue = int.Parse(appleText.text);
        int paperValue = int.Parse(paperText.text);
        int coinValue = int.Parse(coinText.text);

        // Ka��t (Refah) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (paperValue <= 10)
        {
            ApplyRandomEffect(lowPaperMessages);
        }

        // K�l�� (Asker) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (swordValue <= 10)
        {
            ApplyRandomEffect(lowSwordMessages);
        }

        // Elma (Yiyecek) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (appleValue <= 10)
        {
            ApplyRandomEffect(lowAppleMessages);
        }

        // Para (Coin) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (coinValue <= 10)
        {
            ApplyRandomEffect(lowCoinMessages);
        }
    }

    // Rastgele bir mesaj se�ip, bu mesaj�n etkisini kaynaklara uygular
    private void ApplyRandomEffect(string[] messageArray)
    {
        string randomMessage = messageArray[Random.Range(0, messageArray.Length)];
        newsText.text += "\n" + randomMessage; // Yeni mesaj� haber alan�na ekle

        // Mesaj�n etkisini kaynaklara uygula
        switch (randomMessage)
        {
            case "10 Asker Kaybettin":
                UpdateResource(swordText, -10);
                break;
            case "10 Yiyecek Kaybettin":
                UpdateResource(appleText, -10);
                break;
            case "Hazineden 10 alt�n �al�nd�":
            case "10 Alt�n Kaybettin":
                UpdateResource(coinText, -10);
                break;
            case "10 Refah kaybettin":
                UpdateResource(paperText, -10);
                break;
            case "15 Asker Kaybettin":
                UpdateResource(swordText, -15);
                break;
            case "15 Refah Kaybettin":
                UpdateResource(paperText, -15);
                break;
            // "Her �ey yolunda" oldu�unda hi�bir �ey yapma
            default:
                break;
        }
    }

    // Kaynaklar� g�ncelleyen yard�mc� fonksiyon
    private void UpdateResource(TextMeshProUGUI resourceText, int changeAmount)
    {
        int value;
        if (int.TryParse(resourceText.text, out value))
        {
            value += changeAmount;
            //resourceText.text = Mathf.Max(0, value).ToString(); // De�erin negatif olmas�n� engeller
        }
    }
}
