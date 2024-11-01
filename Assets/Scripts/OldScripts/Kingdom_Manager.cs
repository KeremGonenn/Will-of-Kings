using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Kingdom_Manager : MonoBehaviour
{
    // UI Elementleri
    public TextMeshProUGUI swordText; // Kýlýcýn yanýndaki text (asker)
    public TextMeshProUGUI appleText; // Elmanýn yanýndaki text (yiyecek)
    public TextMeshProUGUI coinText;  // Paranýn yanýndaki text
    public TextMeshProUGUI paperText; // Kaðýdýn yanýndaki text (refah)
    public TextMeshProUGUI newsText;  // Haberler kýsmýndaki text

    // Butonlar
    public Button trainSoldierButton;
    public Button donateFarmerButton;
    public Button takeTaxButton;

    // Random mesajlar için diziler
    private string[] lowPaperMessages = { "10 Asker Kaybettin", "10 Yiyecek Kaybettin", "Hazineden 10 altýn çalýndý", "Her þey yolunda", "Her þey yolunda" };
    private string[] lowSwordMessages = { "10 Refah kaybettin", "10 Yiyecek kaybettin", "Hazineden 10 altýn çalýndý", "Her þey yolunda", "Her þey yolunda" };
    private string[] lowAppleMessages = { "15 Asker Kaybettin", "15 Refah Kaybettin", "Her þey yolunda", "Her þey yolunda" };
    private string[] lowCoinMessages = { "10 Asker Kaybettin", "10 Refah Kaybettin", "10 Yiyecek Kaybettin", "Her þey yolunda", "Her þey yolunda" };

    void Start()
    {
        // Butonlara iþlevlerini ekle
        trainSoldierButton.onClick.AddListener(TrainSoldier);
        donateFarmerButton.onClick.AddListener(DonateFarmer);
        takeTaxButton.onClick.AddListener(TakeTax);
    }

    // Train Soldier iþlemi: Kýlýç ve Kaðýt deðerini 10 artýrýr
    public void TrainSoldier()
    {
        int swordValue = int.Parse(swordText.text) + 10;
        swordText.text = swordValue.ToString();

        int paperValue = int.Parse(paperText.text) + 10;
        paperText.text = paperValue.ToString();

        int coinValue = int.Parse(coinText.text) - 10;
        coinText.text = coinValue.ToString();

        CheckConditions(); // Koþullarý kontrol et
    }

    // Donate Farmer iþlemi: Elma ve Kaðýt deðerini 10 artýrýr, Para deðerini 10 azaltýr
    public void DonateFarmer()
    {
        int appleValue = int.Parse(appleText.text) + 10;
        appleText.text = appleValue.ToString();

        int paperValue = int.Parse(paperText.text) + 10;
        paperText.text = paperValue.ToString();

        int coinValue = int.Parse(coinText.text) - 10;
        coinText.text = coinValue.ToString();

        CheckConditions(); // Koþullarý kontrol et
    }

    // Take Tax iþlemi: Para deðerini 10 artýrýr, Kaðýt deðerini 10 azaltýr
    public void TakeTax()
    {
        int coinValue = int.Parse(coinText.text) + 10;
        coinText.text = coinValue.ToString();

        int paperValue = int.Parse(paperText.text) - 10;
        paperText.text = paperValue.ToString();

        CheckConditions(); // Koþullarý kontrol et
    }

    // Koþullarý kontrol eden fonksiyon
    private void CheckConditions()
    {
        int swordValue = int.Parse(swordText.text);
        int appleValue = int.Parse(appleText.text);
        int paperValue = int.Parse(paperText.text);
        int coinValue = int.Parse(coinText.text);

        // Kaðýt (Refah) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (paperValue <= 10)
        {
            ApplyRandomEffect(lowPaperMessages);
        }

        // Kýlýç (Asker) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (swordValue <= 10)
        {
            ApplyRandomEffect(lowSwordMessages);
        }

        // Elma (Yiyecek) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (appleValue <= 10)
        {
            ApplyRandomEffect(lowAppleMessages);
        }

        // Para (Coin) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (coinValue <= 10)
        {
            ApplyRandomEffect(lowCoinMessages);
        }
    }

    // Rastgele bir mesaj seçip, bu mesajýn etkisini kaynaklara uygular
    private void ApplyRandomEffect(string[] messageArray)
    {
        string randomMessage = messageArray[Random.Range(0, messageArray.Length)];
        newsText.text += "\n" + randomMessage; // Yeni mesajý haber alanýna ekle

        // Mesajýn etkisini kaynaklara uygula
        switch (randomMessage)
        {
            case "10 Asker Kaybettin":
                UpdateResource(swordText, -10);
                break;
            case "10 Yiyecek Kaybettin":
                UpdateResource(appleText, -10);
                break;
            case "Hazineden 10 altýn çalýndý":
            case "10 Altýn Kaybettin":
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
            // "Her þey yolunda" olduðunda hiçbir þey yapma
            default:
                break;
        }
    }

    // Kaynaklarý güncelleyen yardýmcý fonksiyon
    private void UpdateResource(TextMeshProUGUI resourceText, int changeAmount)
    {
        int value;
        if (int.TryParse(resourceText.text, out value))
        {
            value += changeAmount;
            //resourceText.text = Mathf.Max(0, value).ToString(); // Deðerin negatif olmasýný engeller
        }
    }
}
