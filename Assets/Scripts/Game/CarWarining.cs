using System.Collections;
using System.Linq;
using UnityEngine;

public class CarWarining : MonoBehaviour
{
    public SoundEffect sound;
    public Sprite warningSprite;
    private Sprite defaultSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;

        var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.spatialBlend = 1f;

        sound.InitByAudioSource(audioSource);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.rigidbody.CompareTag("Player"))
            return;

        if (sound != null && sound.source.isPlaying)
            return;

        float impulse = 0f;

        collision.contacts.ToList().ForEach(c => impulse += c.normalImpulse);

        if (impulse > 17) {
            StartCoroutine(Warning(10));
        }
    }

    private IEnumerator Warning(float times)
    {
        Sprite[] sprites = new Sprite[] { warningSprite, defaultSprite };
        var delay = sound.GetTime() * .5f;

        for (var i = 0; i < times; i ++)
        {
            sound.source.Play();
            sprites = SwapSprite(sprites);

            yield return new WaitForSeconds(delay * 1.5f);

            sprites = SwapSprite(sprites);
            yield return new WaitForSeconds(delay * 1.5f);
        }

        spriteRenderer.sprite = defaultSprite;
    }

    private Sprite[] SwapSprite(Sprite[] sprites)
    {
        spriteRenderer.sprite = sprites[0];
        
        return sprites.Reverse().ToArray();
    }
}
