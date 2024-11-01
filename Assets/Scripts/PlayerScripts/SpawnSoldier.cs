using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSoldier : MonoBehaviour
{
    public GameObject prefab; // �retilecek prefab
    public Transform spawnPoint; // �retim noktas�
    public Transform aimPoint; // Hedef noktas�
    public float moveSpeed = 1f; // Hareket h�z�
    public TextMeshProUGUI kingdomStatusText;
    public TextMeshProUGUI armySizeText;

    public SendTroopPanel troopPanel;


    // Eldar Krall���na asker g�nderme
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
    private float moveSpeed; // Hareket h�z�
    private SoldierAnimationController soldierAnimController;

    public void SetTarget(Transform aimPoint, float speed)
    {
        target = aimPoint;
        moveSpeed = 3f;

        // Prefab'daki animasyon kontrol�n� bul
        soldierAnimController = GetComponent<SoldierAnimationController>();
    }

    void Update()
    {
        if (target != null)
        {
            // Prefab'� hedefe do�ru hareket ettir
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            soldierAnimController.SetWalk(true); // Y�r�y�� animasyonunu ba�lat

            // Prefab'� hedefe do�ru d�nd�r
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // Yatay d�zlemde d�nmesini sa�la
            if (direction != Vector3.zero) // S�f�r vekt�r olmad���ndan emin ol
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Hedefe ula�t���nda hareketi durdur
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // Hareketi ve animasyonu durdur
                if (soldierAnimController != null)
                {
                    soldierAnimController.SetIdle(); // Idle animasyonuna ge�
                }

                //Destroy(this); // Hareket scriptini kald�r
            }
        }
    }
}
