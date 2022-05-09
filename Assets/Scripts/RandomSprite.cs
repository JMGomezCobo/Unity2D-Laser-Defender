using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LaserDefender
{
    public class RandomSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            var randomSprite = Random.Range(0, _sprites.Length);
            
            _spriteRenderer.sprite = _sprites[randomSprite];
        }
    }
}