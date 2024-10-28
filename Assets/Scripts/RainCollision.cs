using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCollision : MonoBehaviour
{
    public ParticleSystem rippleParticleSystem;

    private ParticleSystem rainParticleSystem;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        rainParticleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = rainParticleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            ParticleCollisionEvent collisionEvent = collisionEvents[i];
            Vector3 collisionPos = collisionEvent.intersection;
            Vector3 collisionNormal = collisionEvent.normal;

            EmitRipple(collisionPos, collisionNormal);
        }
    }

    void EmitRipple(Vector3 position, Vector3 normal)
    {
        // Создаем новую частицу ряби
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = position;
        emitParams.rotation3D = Quaternion.LookRotation(normal).eulerAngles;
        
        rippleParticleSystem.Emit(emitParams, 1);
    }
}