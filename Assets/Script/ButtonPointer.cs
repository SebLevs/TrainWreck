using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Selectable selectable;

    private void Start()
    {
        selectable = GetComponent<Selectable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectable.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectable.OnPointerExit(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.selectedObject.GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
