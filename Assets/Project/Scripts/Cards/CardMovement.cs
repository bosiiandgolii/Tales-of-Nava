using System;
using Nav_sprint;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPositon;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject gloweffect;
    [SerializeField] private GameObject playArrow;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalRotation = rectTransform.localRotation;
        originalPosition = rectTransform.localPosition;
    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();         
                break;
        }
    }

    private void TransitionToState0()
    {
        Debug.Log("Using method TransitionToState0. => State 0");
        currentState = 0;
        rectTransform.localScale = originalScale;
        rectTransform.localRotation = originalRotation;
        rectTransform.localPosition = originalPosition;

        //disable play arrow and glow
        gloweffect.SetActive(false);
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Entered");
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            Debug.Log("Pointer Entered. State 0 => State 1");
            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit. State: " + currentState);
        if (currentState == 1)
        {
            Debug.Log("Pointer Exit. State 1 => State 0");
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down" + currentState);
        if (currentState == 1)
        {
            Debug.Log("Start dragging. State 1 => State 2");
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);

            originalPanelLocalPositon = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging. State: " + currentState);
        
        if (currentState == 2)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Input.mousePosition;
                
                Debug.Log("Mouse: " + Input.mousePosition);
                Debug.Log("Card Pos: " + rectTransform.localPosition);

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    Debug.Log("Above play position. State 2 => State 3");
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = playPosition;
                }
            }
        }
    }

    private void HandleHoverState()
    {
        gloweffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        Debug.Log ("Play state active");

        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < cardPlay.y)
        {
            Debug.Log("Restoring to drag state 3 => 2");
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}
