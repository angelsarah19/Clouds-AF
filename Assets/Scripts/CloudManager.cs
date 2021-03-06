﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
	public GameObject cloud;
	public Sprite[] ShapeArray;
    public bool isShape;
    public bool timerReset;
    public GameObject chosenCloud;
    public int cloudNum;
    public Sprite chosenShape;
    public GameObject[] CloudArray;
    private GameObject[] chosenCloudArray; //keep track of the clouds that have been chosen

    public int shapeCount;// how many shapes to show before ending game?
                          //timer
    public float radius = 5f;

    // Ranges for positioning clouds when they spawn
    private Vector3 Min;
    private Vector3 Max;
    private float _xAxis;
    private float _yAxis;
    private float _zAxis; //If you need this, use it
    private Vector3 _randomPosition;
    public bool _canInstantiate;

    //behaviors:
    //tell EventManager to "SpawnShape" (CloudArray[n], ShapeArray[n])
    //tell EventManager to remove "SpawnShape" (CloudArray[n])??
    void OnEnable()
    {
        EventManager.StartListening("FoundCloud", TurnOffCloud);
    }

    void OnDisable()
    {
        EventManager.StopListening("FoundCloud", TurnOffCloud);
    }



    // Start is called before the first frame update
    void Start()
	{
        //start some kind of timer?
        //set the possible locations for clouds

        SetRanges();
        InstantiateRandomObjects();

        //cloudNum = (Random.Range(0, CloudArray.Length));
        //chosenCloud = CloudArray[cloudNum];
        shapeCloud();


    }
    //possible locations for clouds
    private void SetRanges()
    {
        //(left/right, far/near, up/down)
        Min = new Vector3(-40, 20, -40); //Random value.
        Max = new Vector3(40, 30, 40); //Another random value, just for the example.
    }

    private void shapeCloud()
    {
        //choose a random cloud to turn into a shape
        int shapeNum = (Random.Range(0, ShapeArray.Length));
        chosenShape = ShapeArray[shapeNum];
        cloudNum = (Random.Range(0, CloudArray.Length));
        chosenCloud = CloudArray[cloudNum];
        Debug.Log("chosenCloud: " + chosenCloud);

        TurnOffCloud(); //tells cloud that it is not a shae anymore, changes it's shape...

        EventManager.TriggerEvent("SpawnShape"); //tell a cloud to turn into a shape

        //maybe trigger this once the cloud has become a shape?

        EventManager.TriggerEvent("Talk"); //start talking about the shape

        
    }

    private void TurnOffCloud()
    {
        chosenCloud.GetComponent<Cloud>().turnOff();
    }

    // Update is called once per frame
    void Update()
    {

        //call this on a timer, or set of co-routines
        if (Input.GetKeyDown("o"))
        {
            shapeCloud();
        }


    }
    //for prefab instantiation, see: https://docs.unity3d.com/Manual/InstantiatingPrefabs.html

    private void InstantiateRandomObjects()
    {
        if (_canInstantiate)
        {

            //set random locations
            //Debug.Log("_xAxis: " + _yAxis);
            for (int i = 0; i < CloudArray.Length; i++)
                {

                _xAxis = Random.Range(Min.x, Max.x);
                _yAxis = Random.Range(Min.y, Max.y);
                _zAxis = Random.Range(Min.z, Max.z);
                _randomPosition = new Vector3(_xAxis, _yAxis, _zAxis);


                CloudArray[i] = (GameObject)Instantiate(cloud, _randomPosition, Quaternion.EulerRotation(-90, 0, 0));

                CloudArray[i].name = "Cloud" + i;
                Debug.Log("cloud instantiated: " + CloudArray[i].name);



            }
        }

    }

}
