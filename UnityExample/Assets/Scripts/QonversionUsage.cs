﻿using System.Collections;
using System.Collections.Generic;
using Qonversion.Scripts;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class QonversionUsage : MonoBehaviour
{
    public QonversionPurchases QonversionPurchases;
    public Text Text;
    private IAPManagerConfigurator _iapManagerConfigurator;

    public GameObject LoadingIAPText;
    public GameObject IAPBtnsContainer;

    public Button AndrTestPurchasedBtn;

    void Start()
    {
        LoadingIAPText.SetActive(true);
        IAPBtnsContainer.SetActive(false);

        InitializeBilling();

        AndrTestPurchasedBtn.onClick.AddListener(() =>
        {
            _iapManagerConfigurator.PurchaseClick("com.qonversion.sdktestproduct1");
            Debug.Log("PurchaseClicked");
        });

        QonversionPurchases.Initialize("test2");
    }

    private void InitializeBilling()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#if UNITY_ANDROID && !UNITY_EDITOR
        builder.AddProduct("com.qonversion.sdktestproduct1", ProductType.Consumable);
        //#elif UNITY_IPHONE && !UNITY_EDITOR
#endif
        
        _iapManagerConfigurator = new IAPManagerConfigurator(() =>
        {
            IAPBtnsContainer.SetActive(true);
            LoadingIAPText.SetActive(false);
        }, builder, QonversionPurchases);
    }
}