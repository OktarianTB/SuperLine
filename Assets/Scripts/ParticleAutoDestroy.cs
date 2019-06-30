using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    new ParticleSystem particleSystem;

    float particleDuration;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        particleDuration = particleSystem.main.duration;

        StartCoroutine(DestroyParticleSystem());
    }

    IEnumerator DestroyParticleSystem()
    {
        yield return new WaitForSeconds(particleDuration + 0.5f);

        Destroy(gameObject);
    }

}
