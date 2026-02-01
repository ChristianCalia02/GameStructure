using Core.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private int poolSize = 10;
        [SerializeField] private AudioSource audioSourcePrefab;

        private Queue<AudioSource> pool = new Queue<AudioSource>();

        [Header("-SFX-")]
        [SerializeField] private AudioClip defaultFootstep;

        void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                AudioSource src = Instantiate(audioSourcePrefab, transform);
                src.gameObject.SetActive(false);
                pool.Enqueue(src);
            }
        }

        void OnEnable()
        {
            GameEvents.OnFootstep += PlayFootstep;
            GameEvents.OnPlaySoundAtPosition += PlaySoundAtPosition;
            GameEvents.OnPlayUISound += PlayUISound;
        }

        void OnDisable()
        {
            GameEvents.OnFootstep -= PlayFootstep;
            GameEvents.OnPlaySoundAtPosition -= PlaySoundAtPosition;
            GameEvents.OnPlayUISound -= PlayUISound;
        }

        private void PlayFootstep(Vector3 position)
        {
            GameEvents.OnPlaySoundAtPosition?.Invoke(
                defaultFootstep,
                position
            );
        }

        private void PlaySoundAtPosition(AudioClip clip, Vector3 position)
        {
            if (clip == null) return;

            AudioSource src = GetSource();
            src.transform.position = position;
            src.spatialBlend = 1f;
            src.clip = clip;
            src.Play();

            StartCoroutine(ReleaseWhenDone(src));
        }

        private void PlayUISound(AudioClip clip)
        {
            if (clip == null) return;

            AudioSource src = GetSource();
            src.transform.position = Vector3.zero;
            src.spatialBlend = 0f;
            src.clip = clip;
            src.Play();

            StartCoroutine(ReleaseWhenDone(src));
        }

        private AudioSource GetSource()
        {
            AudioSource src;

            if (pool.Count > 0)
            {
                src = pool.Dequeue();
            }
            else
            {
                src = Instantiate(audioSourcePrefab, transform);
            }

            src.gameObject.SetActive(true);  
            //src.clip = null;                 
            return src;
        }

        private System.Collections.IEnumerator ReleaseWhenDone(AudioSource src)
        {
            yield return new WaitWhile(() => src.isPlaying);
            src.gameObject.SetActive(false);
            pool.Enqueue(src);
        }
    }
}
