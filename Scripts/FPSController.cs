using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    private CharacterController controller;
    private Item puttedItem;

    [SerializeField] private Transform head;
    [SerializeField] private GameObject canPutText;
    [SerializeField] private GameObject canDropText;
    [SerializeField] private Transform itemPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float interactionRange;

    private Item canPut;
    private Item CanPut
    {
        get => canPut;
        set
        {
            canPut = value;
            canPutText.SetActive(canPut != null);
        }
    }

    private Plate plate;
    private Plate CurrentPlate
    {
        get => plate;
        set
        {
            plate = value;
            if (plate == null)
                canDropText.SetActive(false);
            else
                canDropText.SetActive(plate.Type == puttedItem.type);
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // input ������������ �� �������� ������ �����������, ��� ��� ����� �������� �� �������� �����������
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(input);
        Move(move, moveSpeed);

        // � �� �� ���� ������ ���������� � ������, ������� ������ �� ��������
        controller.Move(new Vector3(0, -0.02f, 0));

        if (puttedItem == null) {
            FindItem();
            if (CanPut != null && Input.GetKeyDown(KeyCode.F))
                PutItem();
        }
        else {
            FindPlate();
            if (CurrentPlate != null) {
                if (CurrentPlate.Type == puttedItem.type && Input.GetKeyDown(KeyCode.F)) {
                    Destroy(puttedItem.gameObject);
                    puttedItem = null;
                    CurrentPlate.AddItem();
                    CurrentPlate = null;
                }
            }
        }
    }

    // ������� ������ ����������� � ��������� �����. ����� ���� ������� ������� ��� ������ ����������� � ��������� ����� MoveEngine
    private void Move(Vector3 direction, float speed)
    {
        controller.Move(direction * Time.deltaTime * speed);
    }

    private void FindItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(head.position, head.TransformDirection(Vector3.forward), out hit, interactionRange)) {
            CanPut = hit.transform.GetComponent<Item>();
        }
        else 
            CanPut = null;
    }

    private void FindPlate()
    {
        RaycastHit hit;
        if (Physics.Raycast(head.position, head.TransformDirection(Vector3.forward), out hit, interactionRange)) {
            CurrentPlate = hit.transform.GetComponent<Plate>();
        }
        else
            CurrentPlate = null;
    }

    private void PutItem()
    {
        if (CanPut != null) {
            puttedItem = CanPut;
            puttedItem.ChangePosition(head, itemPoint.position);
            CanPut = null;
        }
    }

    public void Respawn(Vector3 spawnPoint)
    {
        transform.rotation = Quaternion.identity;
        head.rotation = Quaternion.identity;

        // ��� ��� ���������� �������������� position, �������� ��� ��� ����������� ��������� ��������� ����� transform
        controller.enabled = false;
        transform.position = spawnPoint;
        controller.enabled = true;

    }
}
