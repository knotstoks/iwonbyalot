using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeslots : MonoBehaviour
{
	public GameObject slot_prefab;
	public List<GameObject> slots;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	void Init(int start_hour, int end_hour) {
		for (int i = start_hour; i < end_hour; i++) {
			GameObject slot = Instantiate(slot_prefab, this.transform) as GameObject;
			slots.Add(slot);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
