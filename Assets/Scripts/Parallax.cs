using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos, starty;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        starty = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x  * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + distance, 
                                            starty, 
                                            transform.position.z);

        if (temp > startpos + length) {
            startpos += length - 0.2f;
        // } else if (temp < startpos + length) {
        //     startpos -= length - 0.2f;
        }
    }
}
