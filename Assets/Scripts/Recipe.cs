using System;
using System.Linq;

public class Recipe
{
       public readonly ParticleType ParticleA;
       public readonly ParticleType ParticleB;

       public Recipe(ParticleType particleA, ParticleType particleB)
       {
              ParticleA = particleA;
              ParticleB = particleB;
       }
}