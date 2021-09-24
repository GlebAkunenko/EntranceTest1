using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool lockCursorOnStart = true;
    [SerializeField] private float sensorics;

    private Transform head;
    
    private void Start()
    {
        if (lockCursorOnStart)
            Cursor.lockState = CursorLockMode.Locked;

        // ��� ��� ���������� �������� transform ������������ ������ ������ GetComponent<Transform>(), � ����� ����������� ��������� ����������� Transform'�
        head = GetComponent<Transform>();

        // ��� ��� � ������ ���� ��-�� ���������� ��������� ���� ������ "����������", ����� �������� ����� ���������� �������
        StartCoroutine(EnableScript());
        enabled = false;
    }

    private IEnumerator EnableScript()
    {
        yield return new WaitForSeconds(0.5f);
        enabled = true;
    }

    // ���� ���� ������������ ����� ������ Update() �� ������ FPSController, �� � ������ ������ ����� �� ���������
    private void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensorics * Time.deltaTime;
        float y = -Input.GetAxis("Mouse Y") * sensorics * Time.deltaTime;

        head.rotation *= Quaternion.Euler(y, 0, 0);
        player.rotation *= Quaternion.Euler(0, x, 0);

    }
}
