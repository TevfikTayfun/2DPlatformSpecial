using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    SpriteRenderer sprite;
    float timer = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        GameObject playerMov = GameObject.Find("Player");
        transform.position = playerMov.transform.position;
        transform.localScale = playerMov.transform.localScale;

        sprite.sprite = playerMov.GetComponent<Movement>().playerSprite.sprite;
        sprite.color = new Vector4(50, 50, 50, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
