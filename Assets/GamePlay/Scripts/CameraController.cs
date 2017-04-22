using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// camera ortografica
public class CameraController : MonoBehaviour {

    public Transform player;

    public float moveSpeed;

    public Vector3 offset;
    public float rotX, rotY;
    public bool isFollow;
    public float sizeCameraInit;

    private Vector3 posCamera;
    private Quaternion rotCamera;
    private float sizeCamera;

	// Use this for initialization
	void Start () {
        // posicao e rotacao inicial da camera
        transform.position = offset;
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (isFollow)
        {
            posCamera = Vector3.Lerp(transform.position, player.position + offset, moveSpeed * Time.deltaTime);
            rotCamera = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotX, rotY, 0), moveSpeed * Time.deltaTime);
            sizeCamera = Mathf.Lerp(Camera.main.orthographicSize, sizeCameraInit, moveSpeed * Time.deltaTime);

            transform.position = posCamera;
            transform.rotation = rotCamera;
            Camera.main.orthographicSize = sizeCamera;
        }
	}
}
