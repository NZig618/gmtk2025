using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public int upgradeCount;
    public List<int> collectedUpgrades = new List<int>();
    [SerializeField] TextMeshProUGUI upgradeText;

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

    public void AddUpgrade(int id)
    {
        collectedUpgrades.Add(id);
    }

    public bool IsUpgradeCollected(int id)
    {
        return collectedUpgrades.Contains(id);
    }

}

