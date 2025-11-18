using UnityEngine;

public class AnomalocarisSpawner : MonoBehaviour
{
    [SerializeField] Vector2 range;
    [SerializeField] GameObject anomalocaris;
    [SerializeField] float spawnRate;

    private float timer = 0;

    private float span;

    private Vector3 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        span = Mathf.Abs(range.x - range.y);
        spawnPos = new Vector3(0, transform.position.y, anomalocaris.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float x = Random.Range(range.x, range.y);

        float playerX = Player.instance.getPosition().x;

        // if(playerX > range.x && playerX < range.y && Random.Range(0, 2) == 0)
        // {   
        //     x = Random.Range(Mathf.Max(range.x, playerX - span / 2), Mathf.Min(range.y, playerX + span / 2));
        // }

        if(timer >= spawnRate)
        {
            timer -= spawnRate;
            Instantiate(anomalocaris, spawnPos + Vector3.right * x, Quaternion.identity);
        }
    }
}
