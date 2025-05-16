using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerMovementParameters", order = 1)]
public class PlayerMovementParameters : ScriptableObject
{

    #region Run
    [Header("Run")]
    [Range(0, 50)]
    [Tooltip("Vitesse maximale horizontale du joueur")] public float maxSpeed = 5;
    #endregion
}