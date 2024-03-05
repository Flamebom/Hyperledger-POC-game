using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private void Awake()
    {
        PlayerReference.player = gameObject;
    }
    public bool isStaggered = false;
    public float playerMovementSpeed = 3;
    public int playerHealth = 6;
    public int maxHealth = 6;
    public int HealthLimit = 20;
    public int blanks = 3;
    public bool Parrying = false;
    public bool LongParrying = false;
    public bool isDashing = false;
    public float Posture = 100;
    public float MaxPosture = 100;
    public int keys = 0;
    public bool criticalHealth = false;
    public Dictionary<string, int> PassiveInventory = new Dictionary<string, int>();
    public Dictionary<string, int> WeaponInventory = new Dictionary<string, int>();
    public float PostureRecovery = 0.1f;
    public int gems = 0;
    public int damage = 6;
    public Vector3 lastPosition = new Vector3();

}
