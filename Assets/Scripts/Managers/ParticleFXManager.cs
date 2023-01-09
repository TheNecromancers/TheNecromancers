using UnityEngine;

public class ParticleFXManager : MonoBehaviour
{
    [field: SerializeField] public GameObject AttackParticleFX { get; set; }
    [field: SerializeField] public GameObject HitParticleFX { get; set; }
    [field: SerializeField] public GameObject ParryParticleFX { get; set; }

    public void PlayParticleFX(Vector3 position, GameObject effect)
    {
        var fx = Instantiate(effect, position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        fx.GetComponent<ParticleSystem>().Play();
    }
}
