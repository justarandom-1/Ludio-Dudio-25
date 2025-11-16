using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UnderwaterCamera : MonoBehaviour
{
    // private SpriteRenderer Water;

    // [SerializeField] Object Bubbles;

    public static UnderwaterCamera instance;

    // 0 - up, 1 - down, 2 - left, 3 - right
    [SerializeField] bool[] borders = {false, true, true, false};

    // Use this for initialization
    void Start () 
    {
        instance = this;
        // Water = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // float PlayerY = Player.instance.GetPosition().y;
        // if(-230 < PlayerY && PlayerY < 0){
        //     float brightness = (PlayerY + 230) / 230F;
        //     Water.color = new Color(brightness, brightness, brightness);
        // }

        Vector2 check = Player.instance.isInBoundary();

        // check = new Vector2(1, 1);

        Vector2 playerPosition = Player.instance.getPosition();

        Vector2 distance = playerPosition - (Vector2) transform.position;

        if(check.x == 1 && ((distance.x < 0 && !borders[2]) || (distance.x > 0 && !borders[3])))
            transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z);

        if(check.y == 1 && ((distance.y > 0 && !borders[0]) || (distance.y < 0 && !borders[1])))
            transform.position = new Vector3(transform.position.x, playerPosition.y, transform.position.z);

        
        // if(Random.Range(0, 40) == 0){
        //     Instantiate(Bubbles, new Vector3(Random.Range(-8F, 8F) + transform.position.x, -7.6F + transform.position.y, -3F), Quaternion.identity);
        // }

    }

    public void setBorder(int i, bool isBordered)
    {
        borders[i] = isBordered;
    }

}
