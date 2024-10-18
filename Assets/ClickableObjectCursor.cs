using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjectCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorHoverTexture;
    [SerializeField] private Texture2D defautCursor;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;

    void Start(){
        //defautCursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SetCursor>().defaultCursorTexture;
        //cursorHoverTexture = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SetCursor>().hoverCursorTexture;
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHoverTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defautCursor, Vector2.zero, cursorMode);
    }
}
