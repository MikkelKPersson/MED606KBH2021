using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class FrameSkip : MonoBehaviour
{
    public Text text;
    VideoPlayer videoPlayer;

    [Header("Frames where the sequences ends")]
    public FrameList[] zones;

    int zoneIndex = 0;
    int switchIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();

        videoPlayer.Prepare();
        //videoPlayer.Play();

        StartCoroutine(controlVideo());
    }

    void Update()
    {
        text.text = videoPlayer.frame.ToString();
        //Debug.Log(videoPlayer.frame);

        if (Input.GetMouseButtonDown(0))
            Debug.Log("Zone  " + zoneIndex + ", switch " + switchIndex);
    }

    IEnumerator controlVideo()
    {
        while(!videoPlayer.isPrepared)
        { 
            Debug.Log("is preparing");
            yield return null;
        }
        videoPlayer.Play();
      
        Debug.Log(videoPlayer.isPlaying);
        // Wait for current clip to stop playing
        while (videoPlayer.isPlaying)
        {
            // check if current zone is the last
            if (zoneIndex >= zones.Length)
            {
                Debug.Log("Done");
                yield break;
            }
                
            // Check if we have reached a switch frame
            if (videoPlayer.frame == zones[zoneIndex].switchFrames[switchIndex])
            {

                int randomValue = Random.Range(1,3);
                //bool engaged = randomValue == 1? true : false;
                bool engaged = false;
                Debug.Log("Viewer engaged: " + engaged);

                if (engaged)
                {
                    // if the current switch is the last in the attention zone
                    if (switchIndex + 1 >= zones[zoneIndex].switchFrames.Length) {
                        zoneIndex++;
                        switchIndex = 0;
                    } else {           // if the current switch is NOT the last in the attention zone
                        switchIndex++;
                    }
                    Debug.Log("Zone  " + zoneIndex + ", switch " + switchIndex);
                }
                else
                {
                    zoneIndex++;
                    switchIndex = 0;
                    Debug.Log("Skipping to zone  " + zoneIndex + ", switchIndex " + switchIndex);
                    videoPlayer.frame = zones[zoneIndex].switchFrames[switchIndex] + 1;
                    zoneIndex++;
                }


            }
            yield return null;
        }
        yield return null;
    }

    [System.Serializable]
    public class FrameList
    {
    public int[] switchFrames;
    }

}
