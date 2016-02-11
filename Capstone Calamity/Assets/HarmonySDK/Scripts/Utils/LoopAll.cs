
using UnityEngine;
using System.Collections;

/*!
 *  @class LoopAll
 *  Loop animation in all game objects in the scene.
 */
[AddComponentMenu("Harmony/Utils/LoopAll")]
public class LoopAll : MonoBehaviour {

  IEnumerator Start()
  {
    HarmonyAnimation[] animations = FindObjectsOfType<HarmonyAnimation>();

    //  Wait for audio to be complete before playing animation.
    foreach( HarmonyAnimation animation in animations )
    {
      GameObject gameObject = animation.gameObject;

      HarmonyRenderer renderer = gameObject.GetComponent<HarmonyRenderer>();
      if ( renderer != null )
      {
        //  Preemptively load clip.
        renderer.LoadClipIndex(0 /* first clip */);
      }

      //  Wait for audio if necessary.
      HarmonyAudio audio = gameObject.GetComponent<HarmonyAudio>();
      if ( audio != null )
      {
        yield return StartCoroutine(audio.WaitForDownloads());
      }
    }

    foreach( HarmonyAnimation animation in animations )
    {
      animation.LoopAnimation( 24.0f, 0 /* first clip */ );

      //  Loop only part of the animation.
      //animation.LoopFrames( 24.0f, 1.0f, 30.0f );
    }
  }
}
