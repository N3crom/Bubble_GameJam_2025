using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [TabGroup("References")]
    [Title("Rect Transform")]
    [SerializeField] private RectTransform rectTransform;

    [TabGroup("References")]
    [Title("Canvas Group")]
    [SerializeField] private CanvasGroup canvasGroup;

    [TabGroup("References")]
    [Title("Item")]
    [SerializeField] private Image image;

    //[Header("Inputs")]

    //[Header("Outputs")]

    private Transform originalParent = null;
    private int originalSiblingIndex = 0;
    private bool isTake = false;

    public void Setup(S_ClassItem item)
    {
        image.sprite = item.sprite;

        isTake = false;

        originalParent = rectTransform.parent;
        originalSiblingIndex = rectTransform.GetSiblingIndex();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isTake = true;

        rectTransform.SetParent(originalParent.parent.parent);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isTake = false;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        rectTransform.SetParent(originalParent);
        rectTransform.SetSiblingIndex(originalSiblingIndex);
    }

    public void ResetCard()
    {
        if (!isTake) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        rectTransform.SetParent(originalParent);
        rectTransform.SetSiblingIndex(originalSiblingIndex);
    }
}