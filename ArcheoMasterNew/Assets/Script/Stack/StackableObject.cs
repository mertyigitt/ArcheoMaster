using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableObject : MonoBehaviour
{
    [SerializeField] private ObjectType objectType;
    public ObjectType ObjectType => objectType;
    
}

public enum ObjectType
{
    BrokenBone,
    Bone,
    BrokenJar,
    Jar,
    BrokenCrock,
    Crock
}
