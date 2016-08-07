using UnityEngine;
using System.Collections;

public class Ruler : MonoBehaviour {

    public Material HoverMaterial;
    public Material OutlineMaterial;

	void Start () {
        Manipulatable m = GetComponent<Manipulatable>();
        m.CustomHoverMaterial = HoverMaterial;
        m.CustomOutlineMaterial = OutlineMaterial;
	}
}
