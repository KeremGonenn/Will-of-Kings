using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSoldier : MonoBehaviour
{
    public GameObject prefab; // Üretilecek prefab
    public Transform spawnPoint; // Üretim noktasý
    public Transform aimPoint; // Hedef noktasý
    public float moveSpeed = 1f; // Hareket hýzý
    public TextMeshProUGUI kingdomStatusText;
    public TextMeshProUGUI armySizeText;

    public SendTroopPanel troopPanel;


    // Eldar Krallýðýna asker gönderme
    public void SendSoldier()
    {
        if (kingdomStatusText.text == "Enemy" )
        {
            int armySize = int.Parse(armySizeText.text);
            if (armySize>0 && troopPanel.army)
            {
                GameObject soldier = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                TextMeshProUGUI soldierText = soldier.GetComponentInChildren<TextMeshProUGUI>();
                soldierText.text = armySize.ToString();
                SoldierMovement soldierMovement = soldier.AddComponent<SoldierMovement>();
                soldierMovement.SetTarget(aimPoint, moveSpeed);
            }   
        }
    }
}

public class SoldierMovement : MonoBehaviour
{
    private Transform target; // Hedef nokta
    private float moveSpeed; // Hareket hýzý
    private SoldierAnimationController soldierAnimController;

    public void SetTarget(Transform aimPoint, float speed)
    {
        target = aimPoint;
        moveSpeed = 3f;

        // Prefab'daki animasyon kontrolünü bul
        soldierAnimController = GetComponent<SoldierAnimationController>();
    }

    void Update()
    {
        if (target != null)
        {
            // Prefab'ý hedefe doðru hareket ettir
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            soldierAnimController.SetWalk(true); // Yürüyüþ animasyonunu baþlat

            // Prefab'ý hedefe doðru döndür
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // Yatay düzlemde dönmesini saðla
            if (direction != Vector3.zero) // Sýfýr vektör olmadýðýndan emin ol
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Hedefe ulaþtýðýnda hareketi durdur
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // Hareketi ve animasyonu durdur
                if (soldierAnimController != null)
                {
                    soldierAnimController.SetIdle(); // Idle animasyonuna geç
                }

                //Destroy(this); // Hareket scriptini kaldýr
            }
        }
    }
}
