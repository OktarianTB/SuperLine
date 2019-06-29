using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    new ParticleSystem particleSystem;

    float totalTime;

    private void Start()
    {
        ParticleSystem.Particle particle = new ParticleSystem.Particle();
        particleSystem = GetComponent<ParticleSystem>();

        totalTime = particle.startLifetime + particleSystem.main.duration;

        StartCoroutine(DestroyParticleSystem());
    }

    IEnumerator DestroyParticleSystem()
    {
        yield return new WaitForSeconds(totalTime + 0.5f);

        Destroy(gameObject);
    }

}
