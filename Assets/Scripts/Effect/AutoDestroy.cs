using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void Start() {
        var ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, (ps != null) ? ps.main.duration + ps.main.startLifetime.constantMax : 0.1f);
    }
}
