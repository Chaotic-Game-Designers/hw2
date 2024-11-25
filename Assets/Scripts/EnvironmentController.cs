using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject floor;
    public GameObject ceiling;
    public GameObject obstacle;

    public float baseHeight = 1f;
    public float maxHeightVariation = 2f;
    public float minGapSize = 3f;

    public float obstacleSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float obstacleRespawnX = 10f;

    private Camera mainCamera;
    private float screenWidth;

    void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        UpdateFloorAndCeiling();
        ResetObstaclePosition();
    }

    void Update()
    {
        MoveObstacle();
        CheckObstacleOutOfView();
        obstacleSpeed += speedIncreaseRate * Time.deltaTime;
    }

    void UpdateFloorAndCeiling()
    {
        float newFloorHeight = Random.Range(baseHeight, baseHeight + maxHeightVariation);
        float newCeilingHeight = Random.Range(baseHeight, baseHeight + maxHeightVariation);
        float gapSize = mainCamera.orthographicSize * 2 - (newFloorHeight + newCeilingHeight);

        if (gapSize < minGapSize)
        {
            UpdateFloorAndCeiling();
            return;
        }

        floor.transform.localScale = new Vector3(screenWidth * 2, newFloorHeight, 1);
        floor.transform.position = new Vector3(0, -mainCamera.orthographicSize + newFloorHeight / 2, 0);

        ceiling.transform.localScale = new Vector3(screenWidth * 2, newCeilingHeight, 1);
        ceiling.transform.position = new Vector3(0, mainCamera.orthographicSize - newCeilingHeight / 2, 0);
    }

    void MoveObstacle()
    {
        obstacle.transform.Translate(Vector2.left * obstacleSpeed * Time.deltaTime);
    }

    void CheckObstacleOutOfView()
    {
        if (obstacle.transform.position.x < -screenWidth)
        {
            UpdateFloorAndCeiling();
            ResetObstaclePosition();
        }
    }

    void ResetObstaclePosition()
    {
        float newObstacleY = Random.Range(floor.transform.position.y + floor.transform.localScale.y / 2 + obstacle.transform.localScale.y / 2,
                                          ceiling.transform.position.y - ceiling.transform.localScale.y / 2 - obstacle.transform.localScale.y / 2);
        obstacle.transform.position = new Vector3(obstacleRespawnX, newObstacleY, 0);
    }
}
