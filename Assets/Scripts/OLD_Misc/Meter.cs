using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    public Texture[] meters;
    private RawImage meter;
    public int index = 10;
    public bool show = true;

    // Start is called before the first frame update
    void Start()
    {
        meter = gameObject.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        meter.texture = meters[index - 1];
    }
}
