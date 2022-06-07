using UnityEngine;

namespace Gameplay
{
    public class MusicController : MonoBehaviour
    {

        public AudioClip music;
        public bool shouldMusicLoop = true;
        public float musicVolume = .15f;
    
        void Start()
        {
            if (MusicMgr.Instance == null)
            {
                Debug.LogWarning("No MusicMgr created");
            }
            else
            {
                MusicMgr.Instance.PlayNewClip(music,shouldMusicLoop, musicVolume);
            }

            MusicMgr.Instance.musicSource.pitch = 1;

        }


    }
}
