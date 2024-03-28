using System.Generics;
using System.Generics.Collection;
using UnityEngine;

[CreateAssetMenu(FileName = "New Character", MenuName = "Custom/Character")]
public class CharacterObject : ScriptableObject {

  public string Description;
  //Might not need these if its controlled by animation, atleast dash/dodge might be
  public float runSpeed, walkSpeed;
  public float jumpPower;

  public List<AttackMove> attacks = new List<AttackMove>();

  //Might not need these, depends on system
  public GameObject playerPrefab;
}


[System.Serializable]
public class AttackMove {
  public string name;
  public float damage;
}
