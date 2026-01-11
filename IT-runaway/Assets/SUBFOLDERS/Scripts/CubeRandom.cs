using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;       // het blokje dat je wilt spawnen
    public Transform classroomsParent;  // de map waarin alle classrooms zitten
    private Transform[] spawnPoints;

    public float spawnHeightOffset = 0.5f;  // hoogte boven de plane

    void Start()
    {
        // Haal alle children van de parent op als spawnpunten
        spawnPoints = classroomsParent.GetComponentsInChildren<Transform>();

        // Spawn cube op een random classroom
        SpawnCube();
    }

    public void SpawnCube()
    {
        if (spawnPoints.Length == 0 || cubePrefab == null) return;

        // Kies een random child, behalve de parent zelf (index 0)
        int index = Random.Range(1, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        // Pas hoogte aan
        Vector3 spawnPosition = spawnPoint.position + new Vector3(0, spawnHeightOffset, 0);

        // Spawn cube
        Instantiate(cubePrefab, spawnPosition, spawnPoint.rotation);
    }
}
