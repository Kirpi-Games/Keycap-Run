using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TouchpadControl
{
    Continuous,
    Transient
}

public class iOSHapticExampleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private bool inputOnTouchpad;

    private Vector2 normalizedInputPosition;

    [SerializeField]
    private TouchpadControl touchPadControlAssignment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputOnTouchpad = RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), new Vector2(Input.mousePosition.x, Input.mousePosition.y), Camera.main);
        //print("inputOnTouchpad for " + gameObject.name + ": " + inputOnTouchpad.ToString());

        if (inputOnTouchpad)
            GetPositionOnTouchPad();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(name + "Game Object Click in Progress");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(name + "Game Object PointerDown");

        switch (touchPadControlAssignment)
        {
            case TouchpadControl.Continuous:
                HapticManager.PlayContinuousHaptic(normalizedInputPosition.x, normalizedInputPosition.y, 30f);
                break;

            case TouchpadControl.Transient:
                HapticManager.PlayTransientHaptic(normalizedInputPosition.x, normalizedInputPosition.y);
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(name + "Game Object PointerUp");

        HapticManager.StopHaptic();

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(name + " OnDrag");

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(name + " OnEndDrag");

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log(name + " OnBeginDrag");


    }

    void GetPositionOnTouchPad()
    {
        Vector2 pointerPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 touchpadSize = GetComponent<RectTransform>().sizeDelta;

        normalizedInputPosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), pointerPosition, Camera.main, out normalizedInputPosition);

        normalizedInputPosition = new Vector2(Remap(normalizedInputPosition.x, -150f, 150f, 0f, 1f), Remap(normalizedInputPosition.y, -150f, 150f, 0f, 1f));

        // update haptic parameters

        switch(touchPadControlAssignment)
        {
            case TouchpadControl.Continuous:
                HapticManager.UpdateContinuousHaptic(normalizedInputPosition.x, normalizedInputPosition.y);
                break;

            case TouchpadControl.Transient:

                break;
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
