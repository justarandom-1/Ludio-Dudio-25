using UnityEngine;

public class AnomalocarisSpawner : MonoBehaviour
{
    [SerializeField] protected Vector2 range;
    [SerializeField] protected GameObject anomalocaris;
    [SerializeField] protected float spawnRate;

    protected float timer = 0;

    protected float span;

    protected Vector3 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        span = Mathf.Abs(range.x - range.y);
        spawnPos = new Vector3(0, transform.position.y, anomalocaris.transform.position.z);
    }

    // Update is called once per frame
    protected virtual void Update()
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
