using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCamera : MonoBehaviour
{

    [SerializeField] float screenWidth = 16.0f;

    [SerializeField] float screenHeight = 9.0f;

    // set the desired aspect ratio (the values in this example are
    // hard-coded for 16:9, but you could make them into public
    // variables instead so you can set them at design time)

    private float targetaspect;

    new private Camera camera;

    private Rect rect;

    float windowaspect;
    

    // Use this for initialization
    void Start () 
    {
        targetaspect = screenWidth / screenHeight;
        // obtain camera component so we can modify its viewport
        camera = GetComponent<Camera>();

        rect = camera.rect;

        // determine the game window's current aspect ratio
        windowaspect = (float)Screen.width / (float)Screen.height;

        Rescale(windowaspect);
        

        // if(Screen.currentResolution.height * 16 / 9 > Screen.currentResolution.width)
        //     Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.width * 9 / 16, true);        
        // else
        //     Screen.SetResolution(Screen.currentResolution.height * 16 / 9, Screen.currentResolution.height, true);
    }

    void Update()
    {
        float w = (float)Screen.width / (float)Screen.height;

        if(w != windowaspect)
        {
            windowaspect = w;
            Rescale(w);
        }
    }

    void Rescale(float w)
    {
        // current viewport height should be scaled by this amount
        float scaleheight = w / targetaspect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {  
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
            
            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
