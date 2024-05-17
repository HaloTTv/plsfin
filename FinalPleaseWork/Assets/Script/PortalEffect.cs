using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEffect : MonoBehaviour
{
    public ParticleSystem portalParticles;

    public void PlayPortalEffect()
    {
        if (portalParticles != null)
        {
            portalParticles.Play();
        }
    }

    public void StopPortalEffect()
    {
        if (portalParticles != null)
        {
            portalParticles.Stop();
        }
    }
}
