using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    public int JumpSpeed = 2;
    private float _maxJumpDegree = 50f;
    private float _buttonPressedTime = 0.0f;
    private bool _isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("buttonPressSec");
        }
        else if(Input.GetKeyUp(KeyCode.Space) && _isJumping == false)
        {
            Jump();
        }
        Time.timeScale = 2;
    }

    IEnumerator buttonPressSec()
    {
        while(_isJumping==false && _buttonPressedTime < _maxJumpDegree)
        {
            yield return new WaitForSeconds(0.01f);
            _buttonPressedTime += 0.05f;
        }
    }
    void Jump()
    {
        if(_isJumping)
        {
            return;
        }
        _isJumping = true;
        Vector3 pos = transform.position;
        Debug.Log($"버튼 누른 시간: {_buttonPressedTime}");
        //_rigidbody.AddForce(0, 80f, _buttonPressedTime);
        _rigidbody.AddForce(0, 90f, 0);
        _rigidbody.AddForce(transform.forward * _buttonPressedTime * JumpSpeed);

        _buttonPressedTime = 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "DeadZone" && transform.position.y <= 0)
        {
            Die();
        }
        else
        {
            _isJumping = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground" && other.GetComponent<Platform>().IsOnClamped == false && _isJumping == false)
        {
            transform.LookAt(other.transform.position);
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
