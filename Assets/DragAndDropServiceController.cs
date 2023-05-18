using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropServiceController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform dragObject;
    private CanvasGroup dragObjectCanvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    private void Awake()
    {
        dragObject = GetComponent<RectTransform>();
        dragObjectCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Cache the original position and parent of the drag object
        originalPosition = dragObject.anchoredPosition;
        originalParent = dragObject.parent;

        // Set the drag object as the last sibling to render it on top
        dragObject.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Disable the drag object's raycast to prevent it from blocking raycasts on other objects
        dragObjectCanvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the drag object's position and parent to their original values
        dragObject.anchoredPosition = originalPosition;
        dragObject.parent = originalParent;

        // Re-enable the drag object's raycast
        dragObjectCanvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert the mouse position to a position in the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragObject.parent as RectTransform, Input.mousePosition, null, out Vector2 localMousePosition);

        // Update the position of the drag object based on the mouse position
        dragObject.localPosition = localMousePosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
