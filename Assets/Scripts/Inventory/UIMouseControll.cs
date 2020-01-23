using UnityEngine;

public class UIMouseControll : MonoBehaviour {
    public GameObject mouseSlot;

    private void Update() {
        if(mouseSlot.activeSelf)
            MouseSlot();
    }


    private void MouseSlot() {
        Vector2 newPos = (Input.mousePosition);
        mouseSlot.transform.position = newPos;
    }
}