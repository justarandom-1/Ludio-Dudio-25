using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    [SerializeField] Vector2 xRange;

    [SerializeField] Vector2 playerXRange;

    [SerializeField] bool usePlayerX;

    [SerializeField] Vector2 yRange;

    [SerializeField] Vector2 playerYRange;

    [SerializeField] bool usePlayerY;

    private bool isXValid;

    private bool isYValid;

    void Start()
    {
        if(usePlayerX) 
            playerXRange = Player.instance.getXRange();

        if(usePlayerY) 
            playerYRange = Player.instance.getYRange();

        isXValid = xRange.x != xRange.y && playerXRange.x != playerXRange.y;
        isYValid = yRange.x != yRange.y && playerYRange.x != playerYRange.y;
    }

    void FixedUpdate()
    {
        if(!isXValid && !isYValid) return;

        Vector2 playerPos = Player.instance.getPosition();

        if(isXValid)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(xRange.x, xRange.y, (playerPos.x - playerXRange.x) / (playerXRange.y - playerXRange.x)), transform.localPosition.y, transform.localPosition.z);
        }

        if(isYValid)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(yRange.x, yRange.y, (playerPos.y - playerYRange.x) / (playerYRange.y - playerYRange.x)), transform.localPosition.z);            
        }
        
    }
}
