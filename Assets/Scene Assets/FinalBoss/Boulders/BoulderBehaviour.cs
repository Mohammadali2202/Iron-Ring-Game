using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class BoulderBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public int index;

    [SerializeField] float spreadValue = 0.25f;

    [SerializeField] Sprite[] sprites;

    private Vector3 moveDirection;

    [SerializeField] GameObject particlePrefab;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
        float scale = Random.Range(0.25f, 0.5f);
        transform.localScale = new Vector3(scale, scale, 1);

        float speedValue = Random.Range(0.5f, 2f);
        Vector3 randomOffset = new Vector3(Random.Range(0, spreadValue), Random.Range(0, spreadValue), 0);

        moveDirection = _player.transform.position - transform.position;
        _rb.velocity = (moveDirection + randomOffset) * speedValue;
    }

    private void SetSprite()
    {
        index = Random.Range(0, sprites.Length);
        _spriteRenderer.sprite = sprites[index];
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.z != 2.01f)
        {
            int num = Random.Range(1, 6);
            for (int i = 0; i < num; i++)
            {
                Vector3 offset = new Vector3(Random.Range(0, 0.25f), Random.Range(0, 0.25f), 0);
                Vector3 pos = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, 2.01f);
                Instantiate(particlePrefab, pos, Quaternion.identity, transform);
            }
        }
        Destroy(gameObject, 0.01f);
    }
}