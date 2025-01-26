using UnityEngine;
using UnityEngine.EventSystems;

public class S_Carte : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private int originalSiblingIndex;

    public int dataValue;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = rectTransform.parent;
        originalSiblingIndex = rectTransform.GetSiblingIndex();

        rectTransform.SetParent(originalParent.parent);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!eventData.pointerEnter || !eventData.pointerEnter.CompareTag("DropSpot"))
        {
            rectTransform.SetParent(originalParent);
            rectTransform.SetSiblingIndex(originalSiblingIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
