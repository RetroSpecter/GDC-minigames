using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMovement : MonoBehaviour {

    public float speed = 5;
    private Rigidbody2D rigid;
    public delegate void action();
    public action die;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rigid.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rigid.velocity.y);
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        die.Invoke();
    }
}
