using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private bool _isDragging = false;

    public void OnBeginDrag()
    {
        _isDragging = true;
    }

    private void Update()
    {
        if (!_isDragging)
        {
            return;
        }

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag()
    {
        _isDragging = false;
    }
}
