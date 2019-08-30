//بِسْمِ اللهِ الرَّحْمٰنِ الرَّحِيْمِ
//Zanga 30/8/2019

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace ResidentJinn
{
    public static class AudioManager
    {

        public static List<AudioSource> audioSources;
        public static List<AudioClip> audioClips;
        public static Transform AudioSourcesParent;
        public static AudioMixer Audio;
        //public static AudioSource MusicSource, EventSource;
        public static void Initilaize(Transform AudioParent)
        {

            AudioSourcesParent = AudioParent;
            audioSources = new List<AudioSource>();
            for (int i = 0; i < AudioSourcesParent.childCount; i++)
                audioSources.Add(AudioSourcesParent.GetChild(i).GetComponent<AudioSource>());

            // audioClips = new List<AudioClip>();
            // audioClips.Add((AudioClip)Resources.Load("Audio/Event"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/Connected"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/Disconnected"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/Hit0"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/Hit1"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/Hit2"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/Hit3"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/PlayerDeath"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/UnitDeath0"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/UnitDeath1"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/UnitHeal0"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/UnitHeal1"));
            // audioClips.Add((AudioClip)Resources.Load("Audio/UnitBlock"));

        }

        public static void Play(Vector3 position, AudioClipName audioClipName, float volume = 0.5f)
        {
            for (int i = 0; i < audioSources.Count; i++)
                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].clip = audioClips[(int)audioClipName];
                    audioSources[i].transform.localPosition = position;
                    audioSources[i].volume = volume;
                    audioSources[i].Play();
                    return;
                }
        }

        public static void Volume(float value)
        {
            Audio.SetFloat("Vol", value);
            //MusicSource = value;
        }

        static private bool mute = false;

        public static void Mute()
        {
            mute = !mute;
            if (mute)
                Audio.SetFloat("Vol", -88);
            else
                Audio.SetFloat("Vol", 0);
        }
    }

    public enum AudioClipName
    {
        Quran,
        JinnScare,
        JinnHurt,
        JinnLaugh,
        ManHurt,
        ManScream,
        ManPray,
        ManEnterBR,
        ManExitBR,
        ClickButton,
        TVRandom,
        OpenDoor,
        WolfHawl,
        WolfAttack,
        WolfHurt,

    }
}
