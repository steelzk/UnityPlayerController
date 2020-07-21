using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// Lock/Un-lock cursor with bool
    /// </summary>
    /// <param name="isLocked"></param>
    void LockCursor(bool isLocked)
    {
        // lock cursor
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // un-lock cursor
        if (!isLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// Lock cursor on start
    /// </summary>
    void Start()
    {
        LockCursor(true);
    }
}
