using System.Collections.Generic;
using UnityEngine;

public class NPC_Decision : MonoBehaviour
{
    public List<NPC_ResourceManager> npcResourceManagers = new List<NPC_ResourceManager>(); // Tüm NPC'lerin kaynak yöneticileri
    public ResourceManager playerResourceManager; // Oyuncu kaynak yöneticisi
    public TurnManager turnManager; // Tur yöneticisi referansý

    // NPC'lerin birbirleriyle olan iliþkilerini tutan yapý (feather deðerlerini temsil eder)
    public Dictionary<NPC_ResourceManager, Dictionary<NPC_ResourceManager, int>> npcRelationships = new Dictionary<NPC_ResourceManager, Dictionary<NPC_ResourceManager, int>>();

    void Start()
    {
        InitializeNPCRelationships();
    }

    // NPC'lerin birbirleriyle olan iliþkilerini baþlatma
    void InitializeNPCRelationships()
    {
        foreach (var npc in npcResourceManagers)
        {
            npcRelationships[npc] = new Dictionary<NPC_ResourceManager, int>();

            // Diðer NPC'ler ile feather deðerlerini sýfýrlayalým (örneðin 50 ile baþlayabilir)
            foreach (var otherNpc in npcResourceManagers)
            {
                if (npc != otherNpc)
                {
                    npcRelationships[npc][otherNpc] = 50; // Baþlangýç feather deðeri 50
                }
            }
        }
    }

    // Karar verme fonksiyonu (her tur çaðrýlýr)
    public void MakeDecisions()
    {
        foreach (var npc in npcResourceManagers)
        {
            EvaluateWarAndPeace(npc);
        }
    }

    // Savaþ ve Barýþ koþullarýný deðerlendirme
    void EvaluateWarAndPeace(NPC_ResourceManager npc)
    {
        int coin = npc.GetResourceValue(npc.coinText);
        int apple = npc.GetResourceValue(npc.appleText);
        int feather = npc.GetResourceValue(npc.featherText);
        int sword = npc.GetResourceValue(npc.swordText); // Asker sayýsýný sword ile temsil ediyoruz

        // 2'den fazla düþman varsa savaþ açma
        int enemyCount = CountEnemies(npc);
        if (enemyCount > 2)
        {
            Debug.Log(npc.name + " 2'den fazla düþmaný olduðu için savaþ açmýyor.");
            return;
        }

        // Savaþ açma koþullarý
        if (coin > 150 && apple > 75 && feather < 40 && npc.statusText.text == "Alliance")
        {
            Debug.Log(npc.name + " savaþ açmak için uygun durumda.");

            // Eðer feather > 80 ise, alliance krallýklarýn düþmanlarýna savaþ açar
            if (feather > 80)
            {
                AttackEnemiesOfAllies(npc);
            }
            else
            {
                // Savaþ açýlacak uygun NPC'yi bul (kendisine savaþ açmasýný engellemek için kontrol)
                NPC_ResourceManager targetNpc = GetTargetNpc(npc);
                if (targetNpc != null)
                {
                    DeclareWar(npc, targetNpc); // Uygun bir NPC'ye savaþ aç
                }
            }
        }
        // Barýþ yapma durumu: Düþman varsa ve asker durumu 50'den az ise
        else if (enemyCount > 0 && sword < 50)
        {
            MakePeace(npc);
        }
    }

    // Ýttifak durumundaki krallýklarýn düþmanlarýna savaþ açma
    void AttackEnemiesOfAllies(NPC_ResourceManager npc)
    {
        foreach (var otherNpc in npcResourceManagers)
        {
            if (otherNpc != npc && otherNpc.statusText.text == "Alliance")
            {
                foreach (var enemy in npcRelationships[otherNpc])
                {
                    if (enemy.Value < 40) // Düþman durumu feather < 40 ise
                    {
                        DeclareWar(npc, enemy.Key);
                    }
                }
            }
        }
    }

    // Uygun bir NPC hedef bulma (örneðin oyuncu dýþýndaki NPC'lerden biri)
    NPC_ResourceManager GetTargetNpc(NPC_ResourceManager npc)
    {
        foreach (var potentialTarget in npcResourceManagers)
        {
            // NPC'nin kendisine savaþ açmasýný engellemek için kontrol ekledik
            if (potentialTarget != npc && potentialTarget.statusText.text == "Alliance") // Sadece Alliance durumundakilere saldýrabiliriz
            {
                return potentialTarget;
            }
        }
        return null; // Eðer uygun NPC yoksa null döner
    }

    // Savaþ açma iþlemi
    void DeclareWar(NPC_ResourceManager npc, NPC_ResourceManager target)
    {
        npc.UpdateResource(npc.featherText, -50); // Feather deðerini düþür
        npc.statusText.text = "Enemy"; // Durumu düþman yap
        npc.statusText.color = Color.red; // UI'da kýrmýzý yap
        AddToEnemyList(npc, target); // Hedefi düþman listesine ekle

        Debug.Log(npc.name + " " + target.name + " ile savaþa girdi!");
    }

    // Barýþ yapma iþlemi
    void MakePeace(NPC_ResourceManager npc)
    {
        npc.UpdateResource(npc.coinText, -30); // Barýþ maliyeti
        npc.UpdateResource(npc.featherText, +20); // Feather kazancý
        npc.statusText.text = "Alliance"; // Barýþ yapýldý
        npc.statusText.color = Color.green; // UI'da yeþil yap

        Debug.Log(npc.name + " barýþ yaptý!");
    }

    // Düþman sayýsýný sayma
    int CountEnemies(NPC_ResourceManager npc)
    {
        int enemyCount = 0;
        foreach (var relationship in npcRelationships[npc])
        {
            if (relationship.Value < 40) // Feather deðeri 40'tan düþükse düþmandýr
            {
                enemyCount++;
            }
        }
        return enemyCount;
    }

    // Düþman listesine ekleme
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
            Debug.Log(npc.name + " için düþman listesi dolu.");
        }
    }
}
