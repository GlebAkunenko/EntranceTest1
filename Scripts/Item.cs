using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private new Collider collider;

    // ��� ��� ����� ��������� ������� ����� ������� ����� ����� ������������ enum. ���� �� �� ���� n-��� ����������, ���� �� ������
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
