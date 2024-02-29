using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Gates
{
    public class Gates : MonoBehaviour
    {
        private BoxCollider2D m_BoxCollider;
        [SerializeField]
        private GameObject exitChecker;
        private Animator m_Animator;

        private void Awake()
        {
            Init();
        }
        public void Init()
        {
            m_BoxCollider = GetComponent<BoxCollider2D>();
            m_Animator = GetComponent<Animator>();
            Open();
        }
        public void Open()
        {
            exitChecker.SetActive(false);
            m_Animator.SetBool("IsOpen", true);
        }
        public void Close()
        {
            exitChecker.SetActive(false);
            m_Animator.SetBool("IsOpen", false);
        }
    }
}

