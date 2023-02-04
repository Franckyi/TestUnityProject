using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] private ParticleType particleType;

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
        
        // create the new particle and delete the other ones
        GameManager.Instance.BoardHandler.SpawnParticle(craftResult.ParticleType, (gameObject.transform.localPosition + otherGameObject.transform.localPosition) / 2);
        Destroy(gameObject);
        Destroy(otherGameObject);
    }
}
