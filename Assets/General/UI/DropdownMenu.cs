using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropdownMenu : MonoBehaviour
{
    public UnityEvent addButtons;

    public List<GameObject> buttons = new List<GameObject>();
    [SerializeField]
    Button buttonPrefab;
    [SerializeField]
    Transform contentWindow;

    public void Toggle()
    {
        contentWindow.gameObject.SetActive(!contentWindow.gameObject.activeSelf);

        if (contentWindow.gameObject.activeSelf)
        {
            addButtons.Invoke();
        }
        else
        {
            RemoveButtons();
        }
    }

    public void AddButton(string name, UnityAction action)
    {
        Button newButton = Instantiate(buttonPrefab, contentWindow);
        newButton.GetComponentInChildren<Text>().text = name;
        newButton.onClick.AddListener(action);
    }

    public void RemoveButtons()
    {
        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
    }
}
