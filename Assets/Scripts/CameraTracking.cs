using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public GameObject Player;
    public int CameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeHeight(int value)
    {
        CameraHeight += value;
    }
    public void UpdateCameraPosition()
    {

        Vector3 MovingPosition = Player.transform.position;

        transform.position = new Vector3(MovingPosition.x, CameraHeight, MovingPosition.z-4);
    }
}
