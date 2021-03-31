using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromOSC : MonoBehaviour
{
    // the OSC object is set from the inspector
    // the OSC script is from:
    // https://thomasfredericks.github.io/UnityOSC/
    //
    // IMPORTANT! Make sure the Port Number in ZIG SIM matches "In Port" in the inspector
    // for the OSC game object
    public OSC osc;

    // how fast the box moves
    public float speed;

    // set to true for iOS, false for Android
    public bool isIOS;

    // set to Device UUID set in ZIG SIM
    public string deviceUUID;

    // then the name of the sensor in the docs for example:
    // https://1-10.github.io/zigsim/features/motion.html#gyro
    public string dataType;

    // ios exmaple:
    //string oscAddress = "/ZIGSIM/jbwios/gyro";
    //
    // android example (drop the "/ZIGSIM"):
    //string oscAddress = "/jbwand/gyro";

    string oscAddress = "";

    void Start()
    {
        // add the "/ZIGSIM" for ios
        if (isIOS) oscAddress += "/ZIGSIM";

        // add the device id
        oscAddress += "/" + deviceUUID;

        // add the name of the data
        oscAddress += "/" + dataType;

        //Debug.Log(oscAddress);

        // set a function to respond to OSC 
        osc.SetAddressHandler(oscAddress, GetMovement);
    }

    void GetMovement(OscMessage message)
    {
        // making the data go from -1 to 1 instead of 0 to 1
        float x = -message.GetFloat(0);
        float y = message.GetFloat(1);
        float z = message.GetFloat(2);

        //Debug.Log(x + "," + y + "," + z);

        Vector3 dir = Vector3.zero;
        dir.x = x;
        dir.y = y;

        dir *= Time.deltaTime;

        transform.Translate(dir * speed);
    }
}

