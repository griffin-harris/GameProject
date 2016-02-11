
using UnityEngine;
using System.Collections;

/*!
 *  @class DemoCallback
 *  Demo that shows how to use callbacks with the HarmonyAnimation component.
 */
[AddComponentMenu("Harmony/Demo/DemoCallback")]
public class DemoCallback : MonoBehaviour {

  public float frameRate = 24.0f;
  public string locatorName;

  IEnumerator Start()
  {
    //  Preemptively load clips.
    HarmonyRenderer renderer = GetComponent<HarmonyRenderer>();
    if ( (renderer == null) || (renderer.clipNames == null) )
      yield break;

    //  Preemptively load clip.
    renderer.LoadClipIndex(0 /* first clip */);

    //  Wait for audio if necessary.
    HarmonyAudio audio = gameObject.GetComponent<HarmonyAudio>();
    if ( audio != null )
    {
      yield return StartCoroutine(audio.WaitForDownloads());
    }

    HarmonyAnimation animation = gameObject.GetComponent<HarmonyAnimation>();
    if ( animation != null )
    {
      HarmonyAnimation.CallbackEvents callbacks = new HarmonyAnimation.CallbackEvents();

      //  Trigger a callback at frame 10 of animation.
      callbacks.AddCallbackAtFrame( 10.0f, CallbackMethod1 );

      //  Trigget a callback at end of animation.
      callbacks.AddCallbackAtEnd( CallbackMethod2 );

      //  Loop animation indefinitely.
      animation.LoopAnimation( frameRate, 0 /* first clip */, 1.0f /*first frame*/, false /*forward*/, callbacks );
    }
  }

  void CallbackMethod1( GameObject sender )
  {
    HarmonyAnchor anchor = sender.GetComponentInChildren<HarmonyAnchor>();
    if ( anchor != null )
    {
      //  Create a projectile at locator location and following same direction.
      GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      projectile.AddComponent<Rigidbody>();

      projectile.transform.localPosition = anchor.gameObject.transform.TransformPoint( 0, 0, 0 );

      Vector3 direction = anchor.gameObject.transform.localToWorldMatrix.MultiplyVector( Vector3.right ).normalized;
      projectile.GetComponent<Rigidbody>().AddForce( direction * 1000 );

      //  Destroy projectile after 2 seconds.
      Destroy(projectile, 2);
    }
  }

  void CallbackMethod2( GameObject sender )
  {
    HarmonyAnchor anchor = sender.GetComponentInChildren<HarmonyAnchor>();
    if ( anchor != null )
    {
      //  Create a projectile at anchor location and following same direction.
      GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Cube);
      projectile.AddComponent<Rigidbody>();

      projectile.transform.localPosition = anchor.gameObject.transform.TransformPoint( 0, 0, 0 );

      Vector3 direction = anchor.gameObject.transform.localToWorldMatrix.MultiplyVector( Vector3.right ).normalized;
      projectile.GetComponent<Rigidbody>().AddForce( direction * 500 );

      //  Destroy projectile after 2 seconds.
      Destroy(projectile, 2);
    }
  }



}
