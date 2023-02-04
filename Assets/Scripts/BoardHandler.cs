using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    private Camera _camera;
    private readonly Dictionary<ParticleType, GameObject> _particlePrefabs = new();

    private void Awake()
    {
        _camera = Camera.main;
        foreach (ParticleType particleType in Enum.GetValues(typeof(ParticleType)))
        {
            var particleName = Enum.GetName(typeof(ParticleType), particleType);
            _particlePrefabs[particleType] = Resources.Load($"Particles/{particleName}") as GameObject;
        }
    }

    private void Update()
    {
        var mousePos = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown("a"))
        {
            SpawnParticle(ParticleType.ParticleA, mousePos);
        }
        else if (Input.GetKeyDown("b"))
        {
            SpawnParticle(ParticleType.ParticleB, mousePos);
        }
        else if (Input.GetKeyDown("c"))
        {
            SpawnParticle(ParticleType.ParticleC, mousePos);
        }
    }

    public void SpawnParticle(ParticleType particle, Vector2 pos)
    {
        Instantiate(_particlePrefabs[particle], pos, Quaternion.identity);
    }
}
