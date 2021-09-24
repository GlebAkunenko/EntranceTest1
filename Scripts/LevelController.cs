using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController self { get; private set; }

    [SerializeField] private int itemCount;
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private Plate[] plates;
    [SerializeField] private GameObject gameEndWindow;
    [SerializeField] private FPSController player;
    [SerializeField] private Vector3 playerSpawnPoint;

    // надо как-то ограничить область спауна. Мне показалось что квадрат по двум точкам это неплохое решение
    [Header("Spawn zone is a squere by points A and B")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private void Start()
    {
        if (self != null)
            throw new System.Exception("More than 1 singletones in scene!!!");
        self = this;

        SpawnLevel();
    }

    private void SpawnLevel()
    {
        player.Respawn(playerSpawnPoint);

        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float minZ = Mathf.Min(pointA.position.z, pointB.position.z);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);
        float maxZ = Mathf.Max(pointA.position.z, pointB.position.z);

        float y = transform.position.y;

        for (int i = 0; i < itemCount; i++) {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            Instantiate(itemPrefabs[i % itemPrefabs.Length], new Vector3(x, y, z), Quaternion.identity);
        }
    }

    private int progress;
    public int Progress
    {
        get => progress;
        set
        {
            progress = value;
            if (progress == itemCount) {
                gameEndWindow.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ResetGame()
    {
        Progress = 0;
        foreach (Plate p in plates)
            p.ResetCounter();
        gameEndWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        SpawnLevel();
    }
}
