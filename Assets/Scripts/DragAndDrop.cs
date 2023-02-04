using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera _camera;
    private SpriteRenderer _sprite;
    private Vector2 _dragOffset;
    [SerializeField] private float speed = 10f;
    
    private void Awake()
    {
        _camera = Camera.main;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        _dragOffset = (Vector2) transform.position - GetMousePos();
        _sprite.sortingOrder += 1;
    }

    private void OnMouseDrag()
    {
        transform.position = Vector2.MoveTowards(transform.position, GetMousePos() + _dragOffset, speed * Time.deltaTime);
    }

    private Vector2 GetMousePos()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
