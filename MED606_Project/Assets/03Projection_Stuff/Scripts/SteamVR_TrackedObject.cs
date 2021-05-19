
//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: For controlling in-game objects with tracked devices.
//
//=============================================================================

using UnityEngine;
using Valve.VR;


namespace Valve.VR
{

    public class SteamVR_TrackedObject : MonoBehaviour
    {

        public int smoothNum = 5;
        [SerializeField]
        Vector3[] prevPositions;
        int posIndex = 0;
        void Start()
        {
            prevPositions = new Vector3[smoothNum];
        }
        public enum EIndex
        {
            None = -1,
            Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15,
            Device16
        }

        public EIndex index;

        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public Vector3 Position_Calibrate;
        public Vector3 Position_Offset;
        public Vector3 Rotation;

        private Vector3 PositionPrev;
        private Vector3 Positioncurrent;

        private Vector3 velocity = Vector3.zero;

        public float smoothTime = 0.3F;

        public bool isValid { get; private set; }

        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {
            if (index == EIndex.None)
                return;

            var i = (int)index;

            isValid = false;
            if (poses.Length <= i)
                return;

            if (!poses[i].bDeviceIsConnected)
                return;

            if (!poses[i].bPoseIsValid)
                return;

            isValid = true;

            var pose = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);

            prevPositions[posIndex] = pose.pos;
            posIndex = posIndex+1 < prevPositions.Length ? posIndex+1 : 0;
            Vector3 posAccum = new Vector3();
            foreach (Vector3 pos in prevPositions)
            {
                posAccum += pos;
            }
            Vector3 smoothPos = posAccum/smoothNum;

            if (origin != null)
            {
                

                //transform.position = Vector3.SmoothDamp(PositionPrev, transform.position, ref velocity, smoothTime);




                transform.position = origin.transform.TransformPoint(smoothPos);
                transform.position -= Position_Calibrate+Position_Offset;

                //transform.rotation = origin.rotation * pose.rot;
                //transform.eulerAngles += Rotation;
                



            }
            else
            {

                
                transform.localPosition = smoothPos;
                transform.position -= Position_Calibrate+Position_Offset;

                //transform.localRotation = pose.rot;
                //transform.eulerAngles += Rotation;
                

            }

            //PositionPrev = transform.position;
        }



        SteamVR_Events.Action newPosesAction;

        SteamVR_TrackedObject()
        {
            newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
        }

        private void Awake()
        {
            OnEnable();
        }

        void OnEnable()
        {
            var render = SteamVR_Render.instance;
            if (render == null)
            {
                enabled = false;
                return;
            }

            newPosesAction.enabled = true;
        }

        void OnDisable()
        {
            newPosesAction.enabled = false;
            isValid = false;
        }

        public void SetDeviceIndex(int index)
        {
            if (System.Enum.IsDefined(typeof(EIndex), index))
                this.index = (EIndex)index;
        }
    }
}