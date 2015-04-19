using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Initally was just for dialog mumble, since the logic is similar I'm going to put walk cycle noises in here as well.
//TODO: Etiehr genericize the logic in here, or separate out the walk logic to its own thing
public class MumblePlayer : MonoBehaviourBase
{

    public enum MumbleType
    {
        Undefined,
        Duck,
        Fight
    }

    private Dictionary<MumbleType, string> _walkPaths = new Dictionary<MumbleType, string>()
    {
        {MumbleType.Duck, "Sounds/DuckGrabBag"},
        {MumbleType.Fight,   "Sounds/FightingGrabBag"},
    };

    private Dictionary<MumbleType, AudioClip[]> _walkMap
        = new Dictionary<MumbleType, AudioClip[]>();

    private AudioSource _source;
    private AudioSource _walkSource;

    // Use this for initialization
    void Awake()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _walkSource = gameObject.AddComponent<AudioSource>();

        foreach (var walkPath in _walkPaths)
        {
            var walkSet = Resources.LoadAll(walkPath.Value).Cast<AudioClip>().ToArray();

            if (walkSet.Length == 0)
            {
                Debug.LogError("Unable to load walk clips for " + walkPath.Key);
            }
            _walkMap.Add(walkPath.Key, walkSet);
        }

    }

    private IEnumerator _walkRoutine;
    public void PlayMumble(MumbleType type)
    {
        if (_walkRoutine != null) return;

        _walkRoutine = WalkLoop(type);
        StartCoroutine(_walkRoutine);
    }

    IEnumerator WalkLoop(MumbleType type)
    {
        _walkSource.volume = 1f;

        while (true)
        {
            var clips = _walkMap[type].AsRandom().ToArray();
            var cutOffMultiplier = 1f;
            var delayAmount = 0f;
            if (type == MumbleType.Duck)
            {
                delayAmount = 0f;
                _walkSource.volume = .05f;
            }
            else if (type == MumbleType.Fight)
            {
                delayAmount = 0f;
                cutOffMultiplier = .9f;
                _walkSource.volume = .15f;
            }

            foreach (var clip in clips)
            {
                _walkSource.clip = clip;
                _walkSource.loop = false;
                _walkSource.Play();


                //There's some silence at the end of each walk clip, so we need to cut it off a bit early
                while (_walkSource.time <= clip.length * cutOffMultiplier)
                {
                    if (!_walkSource.isPlaying) break;

                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(delayAmount);
            }
            yield return new WaitForEndOfFrame();
        }
    }


    public void StopMumble()
    {
        if (_walkRoutine == null) return;
        StopCoroutine(_walkRoutine);
        _walkRoutine = null;
        _walkSource.Stop();
    }


}
