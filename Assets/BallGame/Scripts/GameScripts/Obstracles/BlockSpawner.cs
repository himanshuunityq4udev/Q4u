using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockObject; // Coin prefab to spawn
    public int minWidth = 3;
    public int maxWidth = 3;
    public int minHeight = 4;
    public int maxHeight = 4;
    private void Start()
    {
        CreateWall();
    }

    private void CreateWall()
    {
        int width = Random.Range(minWidth, maxWidth);
        int height = Random.Range(minHeight, maxHeight);


        float startX = -(width - 1) / 2f; // Calculate the starting x position for symmetry

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
               
                float spawnX = startX + x; // Adjust x position based on the starting point
                Vector3 localPosition = new Vector3(spawnX, y + 1, 0); // Local position for the block
                Vector3 worldPosition = transform.position + transform.rotation * localPosition; // Convert to world position

                Instantiate(blockObject, worldPosition, transform.rotation, transform);
            }
        }
    }
}
