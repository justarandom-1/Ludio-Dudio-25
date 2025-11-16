using UnityEngine;

public class CameraListener : MonoBehaviour
{
    [SerializeField] int index;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            UnderwaterCamera.instance.setBorder(index, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            UnderwaterCamera.instance.setBorder(index, false);
        }
    }
}
