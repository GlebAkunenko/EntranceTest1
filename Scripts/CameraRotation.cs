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

        // так как встроенное свойство transform эквевалентно вызову метода GetComponent<Transform>(), в целях оптимизации произвожу кэширование Transform'а
        head = GetComponent<Transform>();

        // так как в начале игры из-за стартового положения мыши камеру "откидывает", делаю задержку перед включением скрипта
        StartCoroutine(EnableScript());
        enabled = false;
    }

    private IEnumerator EnableScript()
    {
        yield return new WaitForSeconds(0.5f);
        enabled = true;
    }

    // была идея организовать вызов метода Update() из класса FPSController, но в данном случае решил не усложнять
    private void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensorics * Time.deltaTime;
        float y = -Input.GetAxis("Mouse Y") * sensorics * Time.deltaTime;

        head.rotation *= Quaternion.Euler(y, 0, 0);
        player.rotation *= Quaternion.Euler(0, x, 0);

    }
}
