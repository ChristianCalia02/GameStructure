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
            AudioEvents.OnFootstep += PlayFootstep;
            AudioEvents.OnPlayAtPosition += PlaySoundAtPosition;
            AudioEvents.OnPlayUI += PlayUISound;
        }

        void OnDisable()
        {
            AudioEvents.OnFootstep -= PlayFootstep;
            AudioEvents.OnPlayAtPosition -= PlaySoundAtPosition;
            AudioEvents.OnPlayUI -= PlayUISound;
        }

        private void PlayClip(AudioClip clip, Vector3 position, float spatialBlend)
        {
            if (clip == null) return;

            AudioSource src = GetSource();
            src.transform.position = position;
            src.spatialBlend = spatialBlend;
            src.clip = clip;
            src.Play();

            StartCoroutine(ReleaseWhenDone(src));
        }

        private void PlayFootstep(Vector3 position) => PlayClip(defaultFootstep, position, 1f);
        private void PlaySoundAtPosition(AudioClip clip, Vector3 position) => PlayClip(clip, position, 1f);
        private void PlayUISound(AudioClip clip) => PlayClip(clip, Vector3.zero, 0f);

        private AudioSource GetSource()
        {
            AudioSource src = pool.Count > 0 ? pool.Dequeue() : Instantiate(audioSourcePrefab, transform);
            src.gameObject.SetActive(true);
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
