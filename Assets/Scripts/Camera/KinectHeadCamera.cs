using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Net.Sockets;
using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

public class KinectHeadCamera : MonoBehaviour {

    public int maxBodies = 12;
    public Transform kinect;
    public Transform kinectHeadTarget;
    public float spaceUnitsPerMeter;
    public Vector3 offset;
    public Vector3 kinectAngle;
    public bool useUdp = false;
    public int udpPort = 1234;
    public int udpWait = 10;

    public Transform focus;

    KinectSensor sensor;
    BodyFrameReader bodyReader;
    Body[] bodies;

    Vector3 kinectHeadPos;

    UdpClient udp;
    IPEndPoint ipEP;
    bool listening = false;

	// Use this for initialization
	void Start () {
        this.kinectHeadPos = this.transform.position;
        if (this.useUdp)
        {
            this.listening = true;
            this.ipEP = new IPEndPoint(IPAddress.Any, this.udpPort);
            this.udp = new UdpClient(this.ipEP);
            this.QueryUdp(null);
        } else
        {
            this.sensor = KinectSensor.GetDefault();
            this.sensor.Open();
            this.bodyReader = this.sensor.BodyFrameSource.OpenReader();
            //this.bodyReader.FrameArrived += this.OnBodyFrameArrived;
        }
    }


    // Update is called once per frame
    void Update () {
        if(!this.useUdp)
        {
            
            if (this.bodyReader != null)
            {
                var frame = this.bodyReader.AcquireLatestFrame();
                if (frame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[this.sensor.BodyFrameSource.BodyCount];
                    }
                    frame.GetAndRefreshBodyData(this.bodies);

                    frame.Dispose();
                    frame = null;
                }
            }
            float best = -1f;
            if (this.bodies != null)
            {
                foreach (Body b in this.bodies)
                {
                    if (b.IsTracked)
                    {
                        Vector3 headPos = this.GetVirtualPosition(
                            this.CSP2Vector3(b.Joints[JointType.Head].Position));
                        if (best == -1 || (Vector3.Distance(headPos, this.kinectHeadTarget.position)) < best)
                        {
                            this.kinectHeadPos = headPos;
                            best = Vector3.Distance(headPos, this.kinectHeadTarget.position);
                        }
                    }
                }
            }
            if (best == -1)
            {
                this.kinectHeadPos = this.kinectHeadTarget.position;
            }
        }
        Vector3 pos = this.kinectHeadPos;
        pos.z = Math.Min(1, pos.z);
        this.transform.position = pos;
    }

    void OnApplicationQuit()
    {
        if (this.bodyReader != null)
        {
            this.bodyReader.Dispose();
            this.bodyReader = null;
        }

        if (this.sensor != null)
        {
            if (this.sensor.IsOpen)
            {
                this.sensor.Close();
            }

            this.sensor = null;
        }
        this.listening = false;
    }

    private void OnBodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        BodyFrame frame = e.FrameReference.AcquireFrame();
        if (frame != null)
        {
            frame.GetAndRefreshBodyData(this.bodies);
            frame.Dispose();
            frame = null;
        }
    }

    void QueryUdp(IAsyncResult previous)
    {
        if(!this.listening)
        {
            return;
        }
        if(previous == null)
        {
            this.udp.BeginReceive(new AsyncCallback(QueryUdp), null);
        } else
        {
            byte[] data = this.udp.EndReceive(previous, ref this.ipEP);
            string[] parsed = Encoding.ASCII.GetString(data).Split(',');
            this.kinectHeadPos = new Vector3(float.Parse(parsed[0]), float.Parse(parsed[1]), float.Parse(parsed[2]));
            Debug.Log(this.kinectHeadPos);
            Thread.Sleep(this.udpWait);
            this.udp.BeginReceive(new AsyncCallback(QueryUdp), null);
        }

    }

    void DrawBodies()
    {
        foreach(Body b in this.bodies)
        {
            foreach (System.Collections.Generic.KeyValuePair<JointType, Windows.Kinect.Joint>
                j in b.Joints)
            {
                Vector3 pos = this.GetVirtualPosition(this.CSP2Vector3(j.Value.Position));
                UnityEngine.Vector3 angle = this.GetVirtualAngle(b.JointOrientations[j.Key].Orientation);
                Debug.DrawRay(pos, angle + pos);
            }
        }
    }

    Vector3 GetVirtualPosition(Vector3 a)
    {
        a = a * this.spaceUnitsPerMeter;
        a = Quaternion.Euler(this.kinectAngle) * a;
        a.z = a.z * -1;
        a = a + this.kinect.position + this.offset;
        return a;
    }

    Vector3 CSP2Vector3(CameraSpacePoint a)
    {
        return new Vector3(a.X, a.Y, a.Z);
    }

    UnityEngine.Vector4 GetVirtualAngle(Windows.Kinect.Vector4 a)
    {
        UnityEngine.Vector4 output = new UnityEngine.Vector4(a.X, a.Y, a.Z, a.W);
        return output;
    }
}
