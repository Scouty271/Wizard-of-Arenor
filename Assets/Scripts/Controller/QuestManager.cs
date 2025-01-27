using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Text questText;

    public int anzahlPortale;
    public int portaleZerstört;
    public int portaleGespawnt;

    public bool bossSpawned;

    public Vector3 letztePortalPos;

    private void Start()
    {
        updatePortalQuestText(anzahlPortale);
    }

    private void Update()
    {
        if (portaleZerstört == 5 && !FindObjectOfType<CheatController>().noBoss)
        {
            if (!bossSpawned)
            {
                FindObjectOfType<MapArenor>().enemys.Add(Instantiate(FindObjectOfType<EnemyContainer_Arenor>().boss, new Vector3(20, 20, -0.5f), Quaternion.identity, FindObjectOfType<EnemyContainer_Arenor>().transform));
                bossSpawned = true;
            }
        }
    }

    public void portalDestroyed()
    {
        anzahlPortale--;
        updatePortalQuestText(anzahlPortale);
    }

    public void portalCreated()
    {
        anzahlPortale++;
        portaleGespawnt++;
        updatePortalQuestText(anzahlPortale);
    }

    private void updatePortalQuestText(int _anzahlPortale)
    {
        questText.text = "- Zerstöre alle 5 Portale" + "(" + _anzahlPortale.ToString() + "/5)";
    }
}
