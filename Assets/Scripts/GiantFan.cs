using UnityEngine;

public class GiantFan : MonoBehaviour
{
    public GameObject toDisable;

    public void TurnOn()
    {
        toDisable.SetActive(true);
    }
    
    public void TurnOff()
    {
        toDisable.SetActive(false);
    }
}
