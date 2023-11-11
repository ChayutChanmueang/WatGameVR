using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EventTrigger : MonoBehaviour
{
    enum TriggerState
    {
        
    }

    [SerializeField] private Vector3 UIOffet;
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private bool isPlayerTrigger;
    void Start()
    {
        
    }

    #region Trigger Event
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            ShowUI(other.transform.position);
            isPlayerTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ShowUI(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            HideUI();
            isPlayerTrigger = false;
        }
    }

    private void OnPlayerEnter(bool isShow)
    {
        
    }
    
    #endregion

    #region UI

    private void ShowUI(Vector3 position)
    {
        triggerUI.SetActive(true);
        triggerUI.transform.position = position + UIOffet;
    }

    private void HideUI()
    {
        triggerUI.SetActive(false);
    }

    #endregion
}
