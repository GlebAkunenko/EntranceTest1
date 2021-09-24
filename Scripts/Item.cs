using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private new Collider collider;

    // так как типов предметов немного думаю удобнее всего будет использовать enum. ≈сли бы их было n-ное количество, завЄл бы массив
    [SerializeField] private Type variation;

    public Type type { get => variation; set => variation = value; }

    public void ChangePosition(Transform newParent, Vector3 newPosition)
    {
        collider.enabled = false;
        transform.position = newPosition;
        transform.parent = newParent;

        Rigidbody rb;
        if (TryGetComponent<Rigidbody>(out rb))
            rb.isKinematic = true;
        
    }

    public enum Type
    {
        cube = 0,
        capsule,
        sphere
    }
}
