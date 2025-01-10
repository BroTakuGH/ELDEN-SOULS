using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preBeamScript : MonoBehaviour
{
    public GameObject beam;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2) {
            Vector3 beamPosition = new Vector3(this.transform.position.x, this.transform.position.y + 3);
			Instantiate(beam, beamPosition, Quaternion.identity);
            timer = 0;
		}
    }
}
