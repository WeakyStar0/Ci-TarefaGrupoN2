using UnityEngine;

[System.Serializable]
public class HoleEntry
{
    public GameObject hole; // Reference to the hole GameObject (with a child Mole)
    public bool isGood;     // Defines if the mole in this hole is good
}
