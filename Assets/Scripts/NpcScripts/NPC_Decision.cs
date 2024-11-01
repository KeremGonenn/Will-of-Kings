using System.Collections.Generic;
using UnityEngine;

public class NPC_Decision : MonoBehaviour
{
    public List<NPC_ResourceManager> npcResourceManagers = new List<NPC_ResourceManager>(); // T�m NPC'lerin kaynak y�neticileri
    public ResourceManager playerResourceManager; // Oyuncu kaynak y�neticisi
    public TurnManager turnManager; // Tur y�neticisi referans�

    // NPC'lerin birbirleriyle olan ili�kilerini tutan yap� (feather de�erlerini temsil eder)
    public Dictionary<NPC_ResourceManager, Dictionary<NPC_ResourceManager, int>> npcRelationships = new Dictionary<NPC_ResourceManager, Dictionary<NPC_ResourceManager, int>>();

    void Start()
    {
        InitializeNPCRelationships();
    }

    // NPC'lerin birbirleriyle olan ili�kilerini ba�latma
    void InitializeNPCRelationships()
    {
        foreach (var npc in npcResourceManagers)
        {
            npcRelationships[npc] = new Dictionary<NPC_ResourceManager, int>();

            // Di�er NPC'ler ile feather de�erlerini s�f�rlayal�m (�rne�in 50 ile ba�layabilir)
            foreach (var otherNpc in npcResourceManagers)
            {
                if (npc != otherNpc)
                {
                    npcRelationships[npc][otherNpc] = 50; // Ba�lang�� feather de�eri 50
                }
            }
        }
    }

    // Karar verme fonksiyonu (her tur �a�r�l�r)
    public void MakeDecisions()
    {
        foreach (var npc in npcResourceManagers)
        {
            EvaluateWarAndPeace(npc);
        }
    }

    // Sava� ve Bar�� ko�ullar�n� de�erlendirme
    void EvaluateWarAndPeace(NPC_ResourceManager npc)
    {
        int coin = npc.GetResourceValue(npc.coinText);
        int apple = npc.GetResourceValue(npc.appleText);
        int feather = npc.GetResourceValue(npc.featherText);
        int sword = npc.GetResourceValue(npc.swordText); // Asker say�s�n� sword ile temsil ediyoruz

        // 2'den fazla d��man varsa sava� a�ma
        int enemyCount = CountEnemies(npc);
        if (enemyCount > 2)
        {
            Debug.Log(npc.name + " 2'den fazla d��man� oldu�u i�in sava� a�m�yor.");
            return;
        }

        // Sava� a�ma ko�ullar�
        if (coin > 150 && apple > 75 && feather < 40 && npc.statusText.text == "Alliance")
        {
            Debug.Log(npc.name + " sava� a�mak i�in uygun durumda.");

            // E�er feather > 80 ise, alliance krall�klar�n d��manlar�na sava� a�ar
            if (feather > 80)
            {
                AttackEnemiesOfAllies(npc);
            }
            else
            {
                // Sava� a��lacak uygun NPC'yi bul (kendisine sava� a�mas�n� engellemek i�in kontrol)
                NPC_ResourceManager targetNpc = GetTargetNpc(npc);
                if (targetNpc != null)
                {
                    DeclareWar(npc, targetNpc); // Uygun bir NPC'ye sava� a�
                }
            }
        }
        // Bar�� yapma durumu: D��man varsa ve asker durumu 50'den az ise
        else if (enemyCount > 0 && sword < 50)
        {
            MakePeace(npc);
        }
    }

    // �ttifak durumundaki krall�klar�n d��manlar�na sava� a�ma
    void AttackEnemiesOfAllies(NPC_ResourceManager npc)
    {
        foreach (var otherNpc in npcResourceManagers)
        {
            if (otherNpc != npc && otherNpc.statusText.text == "Alliance")
            {
                foreach (var enemy in npcRelationships[otherNpc])
                {
                    if (enemy.Value < 40) // D��man durumu feather < 40 ise
                    {
                        DeclareWar(npc, enemy.Key);
                    }
                }
            }
        }
    }

    // Uygun bir NPC hedef bulma (�rne�in oyuncu d���ndaki NPC'lerden biri)
    NPC_ResourceManager GetTargetNpc(NPC_ResourceManager npc)
    {
        foreach (var potentialTarget in npcResourceManagers)
        {
            // NPC'nin kendisine sava� a�mas�n� engellemek i�in kontrol ekledik
            if (potentialTarget != npc && potentialTarget.statusText.text == "Alliance") // Sadece Alliance durumundakilere sald�rabiliriz
            {
                return potentialTarget;
            }
        }
        return null; // E�er uygun NPC yoksa null d�ner
    }

    // Sava� a�ma i�lemi
    void DeclareWar(NPC_ResourceManager npc, NPC_ResourceManager target)
    {
        npc.UpdateResource(npc.featherText, -50); // Feather de�erini d���r
        npc.statusText.text = "Enemy"; // Durumu d��man yap
        npc.statusText.color = Color.red; // UI'da k�rm�z� yap
        AddToEnemyList(npc, target); // Hedefi d��man listesine ekle

        Debug.Log(npc.name + " " + target.name + " ile sava�a girdi!");
    }

    // Bar�� yapma i�lemi
    void MakePeace(NPC_ResourceManager npc)
    {
        npc.UpdateResource(npc.coinText, -30); // Bar�� maliyeti
        npc.UpdateResource(npc.featherText, +20); // Feather kazanc�
        npc.statusText.text = "Alliance"; // Bar�� yap�ld�
        npc.statusText.color = Color.green; // UI'da ye�il yap

        Debug.Log(npc.name + " bar�� yapt�!");
    }

    // D��man say�s�n� sayma
    int CountEnemies(NPC_ResourceManager npc)
    {
        int enemyCount = 0;
        foreach (var relationship in npcRelationships[npc])
        {
            if (relationship.Value < 40) // Feather de�eri 40'tan d���kse d��mand�r
            {
                enemyCount++;
            }
        }
        return enemyCount;
    }

    // D��man listesine ekleme
    void AddToEnemyList(NPC_ResourceManager npc, NPC_ResourceManager enemy)
    {
        if (npc.enemyList1.text == "")
        {
            npc.enemyList1.text = enemy.name;
        }
        else if (npc.enemyList2.text == "")
        {
            npc.enemyList2.text = enemy.name;
        }
        else if (npc.enemyList3.text == "")
        {
            npc.enemyList3.text = enemy.name;
        }
        else
        {
            Debug.Log(npc.name + " i�in d��man listesi dolu.");
        }
    }
}
