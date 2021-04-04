using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject[] cubes = new GameObject[512];
    public float maxScale;

    public AudioVisualizer audioVisualizer;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject instance = (GameObject)Instantiate(cubePrefab);

            instance.transform.position = this.transform.position;  // zero it out first
            instance.transform.parent = this.transform;
            instance.name = "Sample" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            instance.transform.position = Vector3.forward * 100;
            cubes[i] = instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (cubes != null)
            {
                cubes[i].transform.localScale = new Vector3(10, audioVisualizer.samples[i] * maxScale, 10);
            }
        }
    }
}
