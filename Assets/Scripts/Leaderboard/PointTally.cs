using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointTally : MonoBehaviour
{
    public TMP_Text textPoints;

    public int totalPoints;

    private void Update()
    {
        textPoints.text = totalPoints.ToString();
    }
}
