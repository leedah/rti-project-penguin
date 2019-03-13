﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;
using RedRunner.Utilities;

namespace RedRunner.Collectables
{
	public class Coin : Collectable
	{
        const int SPRITE_SCALE = 2;

		[SerializeField]
		protected ParticleSystem m_ParticleSystem;
		[SerializeField]
		protected SpriteRenderer m_SpriteRenderer;
		[SerializeField]
		protected Collider2D m_Collider2D;
        [SerializeField]
		protected bool m_UseOnTriggerEnter2D = true;

		public override SpriteRenderer SpriteRenderer {
			get {
				return m_SpriteRenderer;
			}
		}

		public override Collider2D Collider2D {
			get {
				return m_Collider2D;
			}
		}

        public override bool UseOnTriggerEnter2D {
			get {
				return m_UseOnTriggerEnter2D;
			}
			set {
				m_UseOnTriggerEnter2D = value;
			}
		}

		public override void OnTriggerEnter2D (Collider2D other)
		{
			Character character = other.GetComponent<Character> ();
			if (m_UseOnTriggerEnter2D && character != null) {
				Collect ();
			}
		}

		public override void OnCollisionEnter2D (Collision2D collision2D)
		{
			Character character = collision2D.collider.GetComponent<Character> ();
			if (!m_UseOnTriggerEnter2D && character != null) {
				Collect ();
			}
		}

		public override void Collect ()
		{
            if (GameManager.Singleton == null)
            {
                return;
            }
            GameManager.Singleton.m_Coin-=1;
            GameManager.Singleton.m_Logger.CollectItem(m_SpriteRenderer.sprite.name);
			m_ParticleSystem.Play ();
			m_SpriteRenderer.enabled = false;
			m_Collider2D.enabled = false;
			Destroy (gameObject, m_ParticleSystem.main.duration);
            AudioManager.Singleton.PlayCoinSound (transform.position);
		}

        //public void Awake()
        //{
        //    Debug.Log("Coin Awake!");
        //}
    }
}