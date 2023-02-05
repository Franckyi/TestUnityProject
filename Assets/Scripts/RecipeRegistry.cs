using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeRegistry : MonoBehaviour
{
    private readonly Dictionary<Recipe, ParticleType> _registry = new();
    private void Awake()
    {
        Register(ParticleType.ParticleA, ParticleType.ParticleB, ParticleType.ParticleC);
    }

    private void Register(ParticleType a, ParticleType b, ParticleType result)
    {
        var recipe = new Recipe(a, b);
        _registry[recipe] = result;
    }

    public CraftResult TryCraftUsing(ParticleType a, ParticleType b)
    {
        var result = _registry
            .Where(pair => pair.Key.ParticleA == a && pair.Key.ParticleB == b)
            .Select(pair => new CraftResult(pair.Value))
            .FirstOrDefault();
        return result;
    }
}
