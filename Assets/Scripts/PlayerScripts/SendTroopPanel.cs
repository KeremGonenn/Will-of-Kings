using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendTroopPanel : MonoBehaviour
{
    public Slider armySizeSlider; // Slider referansý
    public TextMeshProUGUI armySizeText; // Slider'daki deðeri göstermek için TextMeshPro
    public TextMeshProUGUI paperRequirementText, appleRequirementText, coinRequirementText;
    public int paperPer10Units = 5, applePer10Units = 10, coinPer10Units = 10;

    public TextMeshProUGUI kingdomStatusText; // Alliance veya Enemy statüsünü gösterecek TextMeshPro

    private int paperRequirement, appleRequirement, coinRequirement;
    public bool army;
    // Player kaynak yöneticisi referansý
    public ResourceManager playerResourceManager;


    void Start()
    {
        // Slider'da deðiþiklik olduðunda UpdateArmySizeText fonksiyonunu çaðýr
        armySizeSlider.onValueChanged.AddListener(delegate { UpdateArmySizeText(); });

        // Slider'ý her deðiþtirdiðinde kaynaklarý güncelle
        armySizeSlider.onValueChanged.AddListener(delegate { UpdateRequirements(); });
    }

    // Slider deðerini metin olarak günceller
    void UpdateArmySizeText()
    {
        // Slider'ýn deðerini Army Size text alanýna yazdýr
        int armySize = Mathf.RoundToInt(armySizeSlider.value);
        armySizeText.text = "Army Size: " + armySize.ToString();
    }

    // Slider deðiþtikçe gerekli kaynaklarý güncelle
    void UpdateRequirements()
    {
        int armySize = Mathf.RoundToInt(armySizeSlider.value / 10) * 10; // 10 ve katlarý olacak þekilde yuvarlama
        armySizeText.text = armySize.ToString();

        // Gereksinimleri hesapla
        paperRequirement = (armySize / 10) * paperPer10Units;
        appleRequirement = (armySize / 10) * applePer10Units;
        coinRequirement = (armySize / 10) * coinPer10Units;

        // Gereksinimleri UI'da göster
        paperRequirementText.text = paperRequirement.ToString();
        appleRequirementText.text = appleRequirement.ToString();
        coinRequirementText.text = coinRequirement.ToString();
    }

    // Asker gönderme butonuna basýldýðýnda çalýþacak fonksiyon
    public void SendTroops()
    {
        if (kingdomStatusText.text == "Enemy")
        {
            // Kaynaklarýn yeterli olup olmadýðýný kontrol et
            int playerPaper = playerResourceManager.GetResourceValue(playerResourceManager.paperText);
            int playerApple = playerResourceManager.GetResourceValue(playerResourceManager.appleText);
            int playerCoin = playerResourceManager.GetResourceValue(playerResourceManager.coinText);
            int armySize = Mathf.RoundToInt(armySizeSlider.value);

            if (playerPaper >= paperRequirement && playerApple >= appleRequirement && playerCoin >= coinRequirement)
            {
                // Asker gönderim iþlemini yap, kaynaklardan düþ
                playerResourceManager.UpdateResource(playerResourceManager.paperText, -paperRequirement);
                playerResourceManager.UpdateResource(playerResourceManager.appleText, -appleRequirement);
                playerResourceManager.UpdateResource(playerResourceManager.coinText, -coinRequirement);
                playerResourceManager.UpdateResource(playerResourceManager.swordText, -armySize);

                Debug.Log("Asker gönderildi: " + armySizeSlider.value + " asker.");
                army = true;
            }
            else
            {
                Debug.Log("Yeterli kaynak yok!");
                army = false;
            }
        }
        else
        {
            Debug.Log("Bu krallýkla düþman deðilsiniz.");
        }
    }
}
