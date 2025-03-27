using System.Collections;
using UnityEngine;

/// <summary>
/// Attached to FX001_01 particle effect prefab. 
/// Handles the offset adjustment and destruction of particle effect
/// </summary>
public class ParticleFXDestroy : MonoBehaviour
{

    private void OnEnable()
    {
        // Offset the particle effect to the center of the object it was instaniated at.
        transform.position = transform.position + new Vector3(0.5f, 0.5f, 0f);
    }

    void Start()
    {
        StartCoroutine(WaitAndDoSomething(0.5f));
    }

    IEnumerator WaitAndDoSomething(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}