using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator playAnimator;

    public float moveDistance;      //  Distancia a ser movida
    public float moveSpeed;         //  velocidade do movimento

    public bool isIdle;             //  indica se o personagem esta parado
    public bool isDead;             //  indica se o personagem esta morto
    public bool isMoving;             //  indica se o personagem esta se movendo
    public bool isCanMove;          //  indica se pode se mover
    public bool jumpStart;          //  indica o inicio do pulo
    public bool isJumping;          //  indica se o personagem esta puland



    public Vector3 target;          // armazena o destino do movimento

    // Use this for initialization
    void Start ()
    {
        isIdle = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CanIdle();
        CanMove();
        Moving();
        AnimatorController();
	}

    #region Metodos criados pelo desenvolvedor

    void CanIdle()
    {
        if (isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                CheckIfCanMove();
            }
        }
    }

    void CheckIfCanMove()
    {
        // TODO  raycast para verificar possivel obstaculos

        SetMove();
    }

    void SetMove()
    {
        isIdle = false;
        isCanMove = true;

        jumpStart = true;
    }

    void CanMove()
    {
        if (isCanMove)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow)) // ao apertar seta para cima
            {
                target = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow)) // ao apertar seta para baixo
            {
                target = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow)) // ao apertar seta para esquerda
            {
                target = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow)) // ao apertar seta para direita
            {
                target = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                jumpStart = false;
                isJumping = true;
                isMoving = true;
                isCanMove = false;
            }
        }
    }

    // metodo responsavel pelo movimento
    void Moving()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
            if (transform.position == target) MoveComplete();
        }
    }

    void MoveComplete()
    {
        isIdle = true;
        isMoving = false;
        isJumping = false;
    }

    void GoHit()
    {

    }

    void AnimatorController()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) { transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) { transform.rotation = Quaternion.Euler(0, 90, 0);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) { transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) { transform.rotation = Quaternion.Euler(0, -90, 0);}

        playAnimator.SetBool("dead", isDead);

        playAnimator.SetBool("preJump", jumpStart);

        playAnimator.SetBool("jump", isJumping);
    }

    #endregion
}
