using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UpgradeManager : MonoBehaviour
{
    public int upgradeCount;
    public List<int> collectedUpgrades = new List<int>();
    [SerializeField] TextMeshProUGUI upgradeText;
    public int heldID;
    [SerializeField] TextMeshProUGUI heldText;

    // Update is called once per frame
    void Update()
    {
        upgradeText.text = "Upgrades: " + upgradeCount.ToString();
        var upgrades = GameObject.FindObjectsByType<PowerUp>(FindObjectsSortMode.None);
        foreach (var upgrade in upgrades) {
            if (IsUpgradeCollected(upgrade.PowerUpId)) {
                Destroy(upgrade.gameObject);
            }
        }
    }

    public void HoldUpgrade(int id, String name)
    {
        heldID = id;
        heldText.text = "Held: " + name;
    }

    public void AddUpgrade()
    {
        if (heldID > 0)
        {
            collectedUpgrades.Add(heldID);
            upgradeCount++;
            heldID = 0;
            heldText.text = "Held: None";
        }
    }

    public bool IsUpgradeCollected(int id)
    {
        return collectedUpgrades.Contains(id) || id == heldID;
    }

}

