using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameObject uiPanel;

    private void Awake()
    {
        uiPanel = gameObject;
    }

    // Makes UI visible and work
    public void ShowUI()
    {
        uiPanel.SetActive(true);
    }


    // Disables the UI
    public void HideUI()
    {
        uiPanel.SetActive(false);
    }
}
