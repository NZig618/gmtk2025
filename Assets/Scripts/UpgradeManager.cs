using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public int upgradeCount;
    [SerializeField] TextMeshProUGUI upgradeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        upgradeText.text = "Upgrades: " + upgradeCount.ToString();
    }
}
