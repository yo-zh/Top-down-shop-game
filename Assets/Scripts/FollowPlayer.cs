using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 thirdPersonOffset = new Vector3(0, 5, -3);
    [SerializeField] Vector2 firstPersonTurn;
    private bool cameraChanged = false;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("ChangeCamera"))
        {
            cameraChanged = !cameraChanged;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !cameraChanged)
        {
            if (thirdPersonOffset.y > 7f)
            {
                thirdPersonOffset.y = 7f;
            }
            else if (thirdPersonOffset.y < 2f)
            {
                thirdPersonOffset.y = 2f;
            }
            thirdPersonOffset.y = thirdPersonOffset.y - Input.GetAxis("Mouse ScrollWheel") * 2;
        }

        if (!cameraChanged)
        {
            transform.position = player.transform.position + thirdPersonOffset;
            transform.LookAt(player.transform.position + Vector3.up);
        }
        else
        {
            firstPersonTurn.x += Input.GetAxis("Mouse X");
            firstPersonTurn.y += Input.GetAxis("Mouse Y");
            transform.position = player.transform.position + Vector3.up/2;
            transform.localRotation = Quaternion.Euler(-firstPersonTurn.y, firstPersonTurn.x, 0);
        }
    }
}
