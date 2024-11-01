using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendTroopPanel : MonoBehaviour
{
    public Slider armySizeSlider; // Slider referans�
    public TextMeshProUGUI armySizeText; // Slider'daki de�eri g�stermek i�in TextMeshPro
    public TextMeshProUGUI paperRequirementText, appleRequirementText, coinRequirementText;
    public int paperPer10Units = 5, applePer10Units = 10, coinPer10Units = 10;

    public TextMeshProUGUI kingdomStatusText; // Alliance veya Enemy stat�s�n� g�sterecek TextMeshPro

    private int paperRequirement, appleRequirement, coinRequirement;
    public bool army;
    // Player kaynak y�neticisi referans�
    public ResourceManager playerResourceManager;


    void Start()
    {
        // Slider'da de�i�iklik oldu�unda UpdateArmySizeText fonksiyonunu �a��r
        armySizeSlider.onValueChanged.AddListener(delegate { UpdateArmySizeText(); });

        // Slider'� her de�i�tirdi�inde kaynaklar� g�ncelle
        armySizeSlider.onValueChanged.AddListener(delegate { UpdateRequirements(); });
    }

    // Slider de�erini metin olarak g�nceller
    void UpdateArmySizeText()
    {
        // Slider'�n de�erini Army Size text alan�na yazd�r
        int armySize = Mathf.RoundToInt(armySizeSlider.value);
        armySizeText.text = "Army Size: " + armySize.ToString();
    }

    // Slider de�i�tik�e gerekli kaynaklar� g�ncelle
    void UpdateRequirements()
    {
        int armySize = Mathf.RoundToInt(armySizeSlider.value / 10) * 10; // 10 ve katlar� olacak �ekilde yuvarlama
        armySizeText.text = armySize.ToString();

        // Gereksinimleri hesapla
        paperRequirement = (armySize / 10) * paperPer10Units;
        appleRequirement = (armySize / 10) * applePer10Units;
        coinRequirement = (armySize / 10) * coinPer10Units;

        // Gereksinimleri UI'da g�ster
        paperRequirementText.text = paperRequirement.ToString();
        appleRequirementText.text = appleRequirement.ToString();
        coinRequirementText.text = coinRequirement.ToString();
    }

    // Asker g�nderme butonuna bas�ld���nda �al��acak fonksiyon
    public void SendTroops()
    {
        if (kingdomStatusText.text == "Enemy")
        {
            // Kaynaklar�n yeterli olup olmad���n� kontrol et
            int playerPaper = playerResourceManager.GetResourceValue(playerResourceManager.paperText);
            int playerApple = playerResourceManager.GetResourceValue(playerResourceManager.appleText);
            int playerCoin = playerResourceManager.GetResourceValue(playerResourceManager.coinText);
            int armySize = Mathf.RoundToInt(armySizeSlider.value);

            if (playerPaper >= paperRequirement && playerApple >= appleRequirement && playerCoin >= coinRequirement)
            {
                // Asker g�nderim i�lemini yap, kaynaklardan d��
                playerResourceManager.UpdateResource(playerResourceManager.paperText, -paperRequirement);
                playerResourceManager.UpdateResource(playerResourceManager.appleText, -appleRequirement);
                playerResourceManager.UpdateResource(playerResourceManager.coinText, -coinRequirement);
                playerResourceManager.UpdateResource(playerResourceManager.swordText, -armySize);

                Debug.Log("Asker g�nderildi: " + armySizeSlider.value + " asker.");
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
            Debug.Log("Bu krall�kla d��man de�ilsiniz.");
        }
    }
}
