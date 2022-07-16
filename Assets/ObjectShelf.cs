using System.Collections.Generic;
using UnityEngine;

public class ObjectShelf : MonoBehaviour
{
    [SerializeField] private List<Transform> _putDownPoints;

    public List<Transform> PutDownPoints => _putDownPoints;
}
