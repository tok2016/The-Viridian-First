using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop shop;
    public GameObject urlText;
    public bool isShopOfficial;

    private void Awake()
    {
        shop = this;
    }

    void Start()
    {
        CreateURL();
    }

    void Update()
    {
        
    }

    private void CreateURL()
    {
        var stringBuilder = new StringBuilder();
        if (isShopOfficial)
        {
            for (var i = 0; i < goodUrl.Length; i++)
            {
                stringBuilder.Append(goodUrl[i][Random.Range(0, goodUrl[i].Length)]);
                stringBuilder.Append(separators[i]);
            }
        }
        else
        {
            foreach (var sequence in badUrl)
            {
                stringBuilder.Append(sequence[Random.Range(0, sequence.Length)]);
            }
        }
        urlText.GetComponent<Text>().text = stringBuilder.ToString();
    }

    private string[][] goodUrl = new[] {
        new[] { "https", "https" },
        new[] { "www", "store", "site", "shop", "online" },
        new[] { "ebay", "amazon", "walmart", "alibaba", "aliexpress", "yahoo", "bestbuy", "flipkart" },
        new[] { "ru", "ua", "uk", "com", "pro", "io", "org", "info" },
        new[] { "shop", "category-shop", "sh", "fg", "rf", "nh", "fm", "vbn", "enf", "bfd", "ok" } };
    private string[] separators = new[] { "://", ".", ".", "/", "" };

    private string[][] badUrl = new[] {
        new[] { "http://", "ftp://", "https", "http" },
        new[] { "www-amazon.er", "139.105.47.92", "178.248.232.27", "amazon.com@evil.kz", "bestbuy.org@bbhfcurd.jp",
        "wwwebayy.pro", "www.alibaba.com/rd?go=http://devil.pl", "www.amazzon.vb", "ebayy.kz", "shop.expressali.bz", "site.fl1pkart0.zz",
        "store.yah00.by", "store.be55tbuy.com", "shop.ali-baba.com", "wwwwamazoncom.kb", "www.al1express.kz", "store.amaz0n.ebay.nb",
        "0nline.eday.com", "online.bestprice.fr", "shop.zoneshop.com/sh?go=http://zlo.ml", "site.bestbuy-shop.com" } };
}
