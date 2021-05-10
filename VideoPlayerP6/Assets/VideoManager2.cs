using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager2 : MonoBehaviour
{
  public OSC osc;
  [Header("Film clips")]
  public ZoneList[] filmParts;

  int partIndex = 0;
  int zoneIndex = 0;
  int vpIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
      // Create gameobjects with videoPlayers for every 
      for (int part = 0; part < filmParts.Length; part++)
      {
          for (int clip = 0; clip < filmParts[part].zoneList.Length; clip++)
          {
            // create gameOject to hold videoPlayer
            GameObject vidHolder = new GameObject("VP_" + part + "_" + clip);
            vidHolder.transform.SetParent(transform);

            // add videoPlayer component to gameObject
            VideoPlayer videoPlayer = vidHolder.AddComponent<VideoPlayer>();

            videoPlayer.playOnAwake = false;

            videoPlayer.source = VideoSource.VideoClip;
            
            videoPlayer.clip = filmParts[part].zoneList[clip];
          }
      }

    

      // VideoPlayer automatically targets the camera backplane when it is added
      // to a camera object, no need to change videoPlayer.targetCamera.
      //videoPlayer1 = camera.GetComponent<UnityEngine.Video.VideoPlayer>()

      // By default, VideoPlayers added to a camera will use the far plane.
      // Let's target the near plane instead.
      //videoPlayers[0].renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

      //videoPlayer.started += Started;

      string vpName = "VP_" + partIndex + "_" + zoneIndex;
      //videoPlayers[0].Prepare();
      
      //StartCoroutine(playVideo(0));
    }
/*
    IEnumerator playVideo(int currentVp)
    {
      videoPlayers[currentVp].Play();

      bool lastInZone = false;
      int nextZoneVp;
      int nextPartVp;
      nextZoneVp = currentVp + 1 < videoPlayers.Length ? currentVp + 1 : 0;
      nextPartVp = nextZoneVp + 1 < videoPlayers.Length ? nextZoneVp + 1 : 0;

      // if the last clip is currently playing
      if (partIndex + 1 >= filmParts.Length)
      {
        yield break;
      }
      // if the current clip is the last in the attention zone
      if (zoneIndex + 1 >= filmParts[partIndex].zoneList.Length)
      {
        lastInZone = true;
        partIndex++;
        zoneIndex = 0;
      }
      else  // if the current clip is NOT the last in the attention zone
      {
        lastInZone = false;
        zoneIndex++;
      }

      
      bool reachedHalfWay = false;
      
      // Wait for current clip to stop playing
      while (videoPlayers[currentVp].isPlaying)
      {
        //(Check if we have reached half way)
        if (!reachedHalfWay && videoPlayers[currentVp].time >= (videoPlayers[currentVp].clip.length / 2))
        {
            reachedHalfWay = true; //Set to true so that we don't evaluate this again
            //Prepare the NEXT video
            Debug.Log("Halfway through. Preparing next clip");
            videoPlayers[nextPartVp].clip = filmParts[partIndex].zoneList[0];
            videoPlayers[nextPartVp].Prepare();

            if (lastInZone)
            {
              videoPlayers[nextZoneVp].clip = filmParts[partIndex].zoneList[zoneIndex];
              videoPlayers[nextZoneVp].Prepare();
            }
        }
        yield return null;
      }

      yield return null;

      int randomValue = Random.Range(1,3);
      bool engaged = randomValue == 1? true : false;
      Debug.Log("Viewer engaged: " + engaged);

      if (!lastInZone && engaged)
      {
        Debug.Log("Starting next clip in the zone");
        StartCoroutine(playVideo(nextZoneVp));
      }
      else
      {
        Debug.Log("Starting clip in the next part");
        StartCoroutine(playVideo(nextPartVp));
      }
    }
*/

    void EndReached(UnityEngine.Video.VideoPlayer vp)
   {

   }




   void Started(UnityEngine.Video.VideoPlayer vp){
      OscMessage message = new OscMessage();
      message.values.Add(1);
       message.address = "/videoINFO";
       osc.Send(message);
       }
}


[System.Serializable]
public class ZoneList
{
  public VideoClip[] zoneList;
}


// Examples of VideoPlayer function
//
// using UnityEngine;
//
// public class Example : MonoBehaviour
// {
//     void Start()
//     {
//         // Will attach a VideoPlayer to the main camera.
//         GameObject camera = GameObject.Find("Main Camera");
//
//         // VideoPlayer automatically targets the camera backplane when it is added
//         // to a camera object, no need to change videoPlayer.targetCamera.
//         var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
//
//         // Play on awake defaults to true. Set it to false to avoid the url set
//         // below to auto-start playback since we're in Start().
//         videoPlayer.playOnAwake = false;
//
//         // By default, VideoPlayers added to a camera will use the far plane.
//         // Let's target the near plane instead.
//         videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
//
//         // This will cause our Scene to be visible through the video being played.
//         videoPlayer.targetCameraAlpha = 0.5F;
//
//         // Set the video to play. URL supports local absolute or relative paths.
//         // Here, using absolute.
//         videoPlayer.url = "/Users/graham/movie.mov";
//
//         // Skip the first 100 frames.
//         videoPlayer.frame = 100;
//
//         // Restart from beginning when done.
//         videoPlayer.isLooping = true;
//
//         // Each time we reach the end, we slow down the playback by a factor of 10.
//         videoPlayer.loopPointReached += EndReached;
//
//         // Start playback. This means the VideoPlayer may have to prepare (reserve
//         // resources, pre-load a few frames, etc.). To better control the delays
//         // associated with this preparation one can use videoPlayer.Prepare() along with
//         // its prepareCompleted event.
//         videoPlayer.Play();
//     }
//
//     void EndReached(UnityEngine.Video.VideoPlayer vp)
//     {
//         vp.playbackSpeed = vp.playbackSpeed / 10.0F;
//     }
// }
