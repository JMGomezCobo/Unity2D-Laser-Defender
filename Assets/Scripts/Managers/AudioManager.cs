using UnityEngine;

namespace LaserDefender.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Shooting")]
        [SerializeField]
        private AudioClip shootingClip;
        [SerializeField] [Range(0f, 1f)] private float shootingVolume = 1f;

        [Header("Damage")]
        [SerializeField]
        private AudioClip damageClip;
        [SerializeField] [Range(0f, 1f)] private float damageVolume = 1f;

        private static AudioManager instance;

        private void Awake()
        {
            ManageSingleton();
        }

        private void ManageSingleton()
        {
            if(instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlayShootingClip()
        {
            PlayClip(shootingClip, shootingVolume);
        }
    
        public void PlayDamageClip()
        {
            PlayClip(damageClip, damageVolume);
        }

        private void PlayClip(AudioClip clip, float volume)
        {
            if (clip == null) return;
            
            var cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}