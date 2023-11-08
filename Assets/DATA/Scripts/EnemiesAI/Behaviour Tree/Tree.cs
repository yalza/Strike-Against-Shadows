using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DATA.Scripts.EnemiesAI.Behaviour_Tree
{
    public abstract class Tree : MonoBehaviour
    {
        protected Node root;
        protected abstract Node SetupTree();
    }
}