using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Lobby : MonoBehaviour
{
    public Button button_Start;
    public Button button_Return;
    public Button button_Buy;
    public Button button_Upgrade;
    public TextMeshProUGUI tmp_Gold;
    public TextMeshProUGUI tmp_UpgradeGold;
    public TextMeshProUGUI tmp_WeaponLV;

    int price = 0;
    int levelForPrice = 0;

    private int GOLD_FOR_TEST = 100;
    private int UPGRADE_LEVEL_FOR_TEST = 1;


    // Start is called before the first frame update
    void Start()
    {
        button_Start.onClick.AddListener(StartClick);
        button_Return.onClick.AddListener(ReturnClick);
        button_Upgrade.onClick.AddListener(UpgradeClick);
        button_Buy.onClick.AddListener(BuyClick);


        tmp_Gold.SetText(GOLD_FOR_TEST.ToString());
        levelForPrice = UPGRADE_LEVEL_FOR_TEST;
        tmp_WeaponLV.SetText("Lv " + levelForPrice.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    void ReturnClick()
    {
        SceneManager.LoadScene("MainScene");
    }
    void BuyClick()
    {
        GOLD_FOR_TEST -= price;
        UPGRADE_LEVEL_FOR_TEST = levelForPrice;
        price = 0;
        button_Buy.gameObject.SetActive(false);
        tmp_Gold.SetText(GOLD_FOR_TEST.ToString());
        tmp_UpgradeGold.SetText(price.ToString());

        //¿˙¿Â
    }
    void UpgradeClick()
    {
       if(price + UpgradePrice(levelForPrice + 1) <= GOLD_FOR_TEST)
        {
            price += UpgradePrice(levelForPrice + 1);
            levelForPrice++;

            if (!button_Buy.IsActive()) button_Buy.gameObject.SetActive(true);
            tmp_UpgradeGold.SetText(price.ToString());
            tmp_WeaponLV.SetText("Lv " + levelForPrice.ToString());
        }
    }

    int UpgradePrice(int targetLv)
    {
        return targetLv * 10;
    }
}
