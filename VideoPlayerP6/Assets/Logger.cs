using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using System;
// https://stackoverflow.com/questions/47401759/play-different-videos-back-to-back-seamlessly-using-unity-video-player
public class Logger : MonoBehaviour
{
  // StreamWriter LogFile;
  public OSC oscReference;
    // Start is called before the first frame update
  VideoPlayer videoPlayer;
    void Start()
    {
      // LogFile = new StreamWriter("TestLog.csv", true);
      WriteLog("UnityTime,NodeJSTime,Attention,Meditation,PoorSignalLevel,Frame,Delta,Theta,LowAlpha,HighAlpha,LowBeta,HighBeta,LowGamma,HighGamma,Time");
      oscReference.SetAddressHandler( "/mynddata" , OnReceive );
      GameObject camera = GameObject.Find("Main Camera");
      videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnReceive(OscMessage message) {
      var unityTime = DateTime.UtcNow.ToString("HH:mm:ss:ms");
     // print("unityTime" + unityTime);
      var vidTime = videoPlayer.time.ToString();
      vidTime = vidTime.Replace(",", ".");
      // float data = message.GetString(0);
      WriteLog(message.GetString(0)  + "," + unityTime + ", "+ message.GetFloat(1) + "," + message.GetFloat(2) +  "," + message.GetFloat(3) + "," + videoPlayer.frame + "," + message.GetFloat(4) + "," + message.GetFloat(5) + "," + message.GetFloat(6) + "," + message.GetFloat(7) + "," + message.GetFloat(8) + "," + message.GetFloat(9) + "," + message.GetFloat(10) + "," + message.GetFloat(11) + "," + vidTime);
     // print(message.GetString(0)  + "," + unityTime + ", "+ message.GetFloat(1) + "," + message.GetFloat(2) +  "," + message.GetFloat(3) + "," + videoPlayer.frame + "," + message.GetFloat(4) + "," + message.GetFloat(5) + "," + message.GetFloat(6) + "," + message.GetFloat(7) + "," + message.GetFloat(8) + "," + message.GetFloat(9) + "," + message.GetFloat(10) + "," + message.GetFloat(11) + "," + vidTime );

    }
    public void WriteLog(string message)
    {
      using (System.IO.StreamWriter LogFile = new System.IO.StreamWriter("TestLog.csv", true))
      {
            LogFile.WriteLine(message);
        }
    }
}
