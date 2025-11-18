using UnityEngine;

public class DolphinSpawner : AnomalocarisSpawner
{
    protected override void Start()
    {
        base.Start();

        timer = Random.Range(0, spawnRate);
    }   
 
    // Update is called once per frame
    protected override void Update()
    {
        float playerX = Player.instance.getPosition().x;

        if(playerX > range.x && playerX < range.y)
        {
            timer += Time.deltaTime;

            if(timer >= spawnRate)
            {
                timer -= spawnRate;
                Instantiate(anomalocaris, spawnPos, Quaternion.identity);
            }
        }

        
    }
}
