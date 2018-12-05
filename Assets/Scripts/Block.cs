using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Config Params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockParticleVFX;
    [SerializeField] Sprite[] hitSprites;


    // Cached Reference
    Level level;

    // State Variables
    [SerializeField] int currentHits; // TODO only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            ManageHit();
        }
    }

    private void ManageHit()
    {
        currentHits++;
        int maxHits = hitSprites.Length + 1;
        if (currentHits >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = currentHits - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from arrary " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSession>().IncreaseScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        triggerParticles();
        Destroy(gameObject);
        level.BlockDestroyed();
    }

    private void triggerParticles()
    {
        GameObject particles = Instantiate(blockParticleVFX, transform.position, transform.rotation);
        Destroy(particles, 1f);
    }
}
