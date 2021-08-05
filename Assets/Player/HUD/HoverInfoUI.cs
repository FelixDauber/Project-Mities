using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoUI : MonoBehaviour
{
    private Text text;
    void Awake()
    {
        PlayerInteractor playerInteractor = FindObjectOfType<PlayerInteractor>();
        playerInteractor.NewHit.AddListener(UpdateUI);
        playerInteractor.NowOverlapsUI.AddListener(UpdateUI);
        text = GetComponent<Text>();
    }

    void UpdateUI(RaycastHit newHit)
    {
        if (newHit.collider != null)
            text.text = newHit.collider.gameObject.name;
        else
            text.text = "";
    }

    void UpdateUI(bool hitsUI)
    {
        text.enabled = !hitsUI;
    }
}
