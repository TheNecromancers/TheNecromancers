using UnityEngine;

public class ParticleFXManager : MonoBehaviour
{
    public GameObject AttackParticleFX;
    public GameObject HitParticleFX;

    public void PlayParticleFX(Vector3 position, GameObject effect)
    {
        var fx = Instantiate(effect, position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        fx.GetComponent<ParticleSystem>().Play();
    }
}
