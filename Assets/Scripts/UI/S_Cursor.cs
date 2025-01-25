using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Cursor : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] private RSE_CallCursor callCursor;
    [SerializeField] private RSE_UnCallCursor unCallCursor;

    [Header("References")]
    [SerializeField] private Texture2D handCursor;

    private void OnEnable()
    {
        callCursor.action += OnMouseEnter;
        unCallCursor.action += OnMouseLeave;
    }

    private void OnDisable()
    {
        callCursor.action -= OnMouseEnter;
        unCallCursor.action -= OnMouseLeave;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseEnter()
    {
        Vector2 cursorOffset = new Vector2(handCursor.width / 3, handCursor.height / 40);

        Cursor.SetCursor(handCursor, cursorOffset, CursorMode.Auto);
    }

    private void OnMouseLeave()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
