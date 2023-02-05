using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ParticleHandler : MonoBehaviour, IPointerClickHandler // I think that's right, just let auto complete help you
{
    [SerializeField] private ParticleType particleType;
    [SerializeField] private float dragSpeed = 5;
    [SerializeField] private float velocityDecay = 0.005f;
    private Camera _camera;
    private Rigidbody2D _rigidBody;
    
    private void Awake()
    {
        _camera = Camera.main;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // every frame we make the velocity of the object slowly decay by dividing it by (1 + velocityDecay)
        _rigidBody.velocity /= (1 + velocityDecay);
    }

    private void OnMouseDrag()
    {
        // move the object by increasing its velocity
        _rigidBody.velocity = (GetMousePos() - (Vector2) transform.position) * dragSpeed;
    }

    private Vector2 GetMousePos()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // check that it collided with another particle
        var otherGameObject = collision.gameObject;
        var otherParticleHandler = otherGameObject.GetComponent<ParticleHandler>();
        if (otherParticleHandler == null) return;
        
        // check if there's a recipe between these two particles
        var otherParticleType = otherParticleHandler.particleType;
        var craftResult = GameManager.Instance.RecipeRegistry.TryCraftUsing(particleType, otherParticleType);
        if (craftResult == null) return;
        
        // create the new particle
        var newParticle = GameManager.Instance.BoardHandler.SpawnParticleAt(craftResult.ParticleType, (transform.localPosition + otherGameObject.transform.localPosition) / 2);
        
        // give new particle some velocity
        var rigidBodyNew = newParticle.GetComponent<Rigidbody2D>();
        rigidBodyNew.velocity = (_rigidBody.velocity + otherParticleHandler._rigidBody.velocity) / 2;
        
        // destroy the two merged particles
        Destroy(gameObject);
        Destroy(otherGameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
