//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: The object attached to the player's hand that spawns and fires the
//			arrow
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class ArrowHand : MonoBehaviour
	{
		private Hand hand;
		private Longbow bow;
		private GameObject currentArrow;
		public GameObject arrowPrefab;
		public GameObject laserPrefab;
		public GameObject gunPrefab;
		public Transform arrowNockTransform;
		private GameObject gun;

		public float nockDistance = 0.1f;
		public float lerpCompleteDistance = 0.08f;
		public float rotationLerpThreshold = 0.15f;
		public float positionLerpThreshold = 0.15f;

		private bool allowArrowSpawn = true;
		private bool nocked;

		private bool inNockRange = false;
		private bool arrowLerpComplete = false;

		public SoundPlayOneshot arrowSpawnSound;

		private AllowTeleportWhileAttachedToHand allowTeleport = null;

		public int maxArrowCount = 10;
		private List<GameObject> arrowList;


		//-------------------------------------------------
		void Awake()
		{
			allowTeleport = GetComponent<AllowTeleportWhileAttachedToHand>();
			allowTeleport.teleportAllowed = true;
			allowTeleport.overrideHoverLock = false;

			arrowList = new List<GameObject>();
			gun = Instantiate (gunPrefab, arrowNockTransform);
			gun.transform.rotation = Quaternion.Euler (0, 90, 40);
		}


		//-------------------------------------------------
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
			FindBow();
		}


		//-------------------------------------------------
		private GameObject InstantiateArrow()
		{
			GameObject arrow = Instantiate( arrowPrefab, arrowNockTransform.position, arrowNockTransform.rotation ) as GameObject;
			arrow.name = "Bow Arrow";
			arrow.transform.parent = arrowNockTransform;
			Util.ResetTransform( arrow.transform );

			arrowList.Add( arrow );

			while ( arrowList.Count > maxArrowCount )
			{
				GameObject oldArrow = arrowList[0];
				arrowList.RemoveAt( 0 );
				if ( oldArrow )
				{
					Destroy( oldArrow );
				}
			}

			return arrow;
		}

		//my stuff
		private GameObject InstantiateLaser()
		{
			//laserTransform = arrowNockTransform;
			//laserTransform.parent = arrowNockTransform;
			Vector3 laserPos = new Vector3(arrowNockTransform.position.x, arrowNockTransform.position.y, arrowNockTransform.position.z);
			Quaternion laserRot = Quaternion.Euler (arrowNockTransform.rotation.x, arrowNockTransform.rotation.y, arrowNockTransform.rotation.z);
			GameObject lazer = Instantiate( laserPrefab, laserPos, laserRot);
			lazer.name = "Laser";
			//laser.transform.parent = hand.transform;//change to hand
			//Util.ResetTransform( laser.transform );
			lazer.GetComponent<Rigidbody>().AddForce( arrowNockTransform.forward * 10f, ForceMode.VelocityChange );
			//laser.GetComponent<Rigidbody>().AddTorque( currentArrow.transform.forward * 10 );
			return lazer;
		}

		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			if ( bow == null )
			{
				FindBow();
			}

			if ( bow == null )
			{
				return;
			}

			if ( allowArrowSpawn && ( currentArrow == null ) ) // If we're allowed to have an active arrow in hand but don't yet, spawn one
			{
				currentArrow = InstantiateArrow();
				arrowSpawnSound.Play();
			}

			float distanceToNockPosition = Vector3.Distance( transform.parent.position, bow.nockTransform.position );

			// If there's an arrow spawned in the hand and it's not nocked yet
			if ( !nocked )
			{
				// If grip is not being held down, swap arrow for gun
				if (!hand.controller.GetPress (SteamVR_Controller.ButtonMask.Grip)) {
					if (GameObject.Find ("arrow_prop")) {
						GameObject.Find ("arrow_prop").GetComponent<Renderer> ().enabled = false;
						GameObject.Find ("arrow_tip").GetComponent<Renderer> ().enabled = false;
						GameObject.Find ("Hull").GetComponent<Renderer> ().enabled = true;
						GameObject.Find ("Munition").GetComponent<Renderer> ().enabled = true;
						GameObject.Find ("Trigger").GetComponent<Renderer> ().enabled = true;
					}
				} else {
					if (GameObject.Find ("arrow_prop")) {
						GameObject.Find ("arrow_prop").GetComponent<Renderer> ().enabled = true;
						GameObject.Find ("arrow_tip").GetComponent<Renderer> ().enabled = true;
						GameObject.Find ("Hull").GetComponent<Renderer> ().enabled = false;
						GameObject.Find ("Munition").GetComponent<Renderer> ().enabled = false;
						GameObject.Find ("Trigger").GetComponent<Renderer> ().enabled = false;
					}
				}
				// If we're close enough to nock position that we want to start arrow rotation lerp, do so
				if ( distanceToNockPosition < rotationLerpThreshold )
				{
					float lerp = Util.RemapNumber( distanceToNockPosition, rotationLerpThreshold, lerpCompleteDistance, 0, 1 );

					arrowNockTransform.rotation = Quaternion.Lerp( arrowNockTransform.parent.rotation, bow.nockRestTransform.rotation, lerp );
				}
				else // Not close enough for rotation lerp, reset rotation
				{
					arrowNockTransform.localRotation = Quaternion.identity;
				}

				// If we're close enough to the nock position that we want to start arrow position lerp, do so
				if ( distanceToNockPosition < positionLerpThreshold )
				{
					float posLerp = Util.RemapNumber( distanceToNockPosition, positionLerpThreshold, lerpCompleteDistance, 0, 1 );

					posLerp = Mathf.Clamp( posLerp, 0f, 1f );

					arrowNockTransform.position = Vector3.Lerp( arrowNockTransform.parent.position, bow.nockRestTransform.position, posLerp );
				}
				else // Not close enough for position lerp, reset position
				{
					arrowNockTransform.position = arrowNockTransform.parent.position;
				}


				// Give a haptic tick when lerp is visually complete
				if ( distanceToNockPosition < lerpCompleteDistance )
				{
					if ( !arrowLerpComplete )
					{
						arrowLerpComplete = true;
						hand.controller.TriggerHapticPulse( 500 );
					}
				}
				else
				{
					if ( arrowLerpComplete )
					{
						arrowLerpComplete = false;
					}
				}

				// Allow nocking the arrow when controller is close enough
				if ( distanceToNockPosition < nockDistance )
				{
					if ( !inNockRange )
					{
						inNockRange = true;
						bow.ArrowInPosition();
					}
				}
				else
				{
					if ( inNockRange )
					{
						inNockRange = false;
					}
				}

				// If arrow is close enough to the nock position and we're pressing the trigger, and we're not nocked yet, Nock
				if ( ( distanceToNockPosition < nockDistance ) && hand.controller.GetPress( SteamVR_Controller.ButtonMask.Grip ) && !nocked )
				{
					if ( currentArrow == null )
					{
						currentArrow = InstantiateArrow();
					}

					nocked = true;
					bow.StartNock( this );
					hand.HoverLock( GetComponent<Interactable>() );
					allowTeleport.teleportAllowed = false;
					currentArrow.transform.parent = bow.nockTransform;
					Util.ResetTransform( currentArrow.transform );
					Util.ResetTransform( arrowNockTransform );
				}
			}


			// If arrow is nocked, and we release the trigger
			if ( nocked && ( !hand.controller.GetPress( SteamVR_Controller.ButtonMask.Grip ) || hand.controller.GetPressUp( SteamVR_Controller.ButtonMask.Grip ) ) )
			{
				if ( bow.pulled ) // If bow is pulled back far enough, fire arrow, otherwise reset arrow in arrowhand
				{
					FireArrow();
				}
				else
				{
					arrowNockTransform.rotation = currentArrow.transform.rotation;
					currentArrow.transform.parent = arrowNockTransform;
					Util.ResetTransform( currentArrow.transform );
					nocked = false;
					bow.ReleaseNock();
					hand.HoverUnlock( GetComponent<Interactable>() );
					allowTeleport.teleportAllowed = true;
				}

				bow.StartRotationLerp(); // Arrow is releasing from the bow, tell the bow to lerp back to controller rotation
			}

				if (hand.controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)&&!hand.controller.GetPress( SteamVR_Controller.ButtonMask.Grip))
				FireLaser ();
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}


		//-------------------------------------------------
		private void FireArrow()
		{
			currentArrow.transform.parent = null;

			Arrow arrow = currentArrow.GetComponent<Arrow>();
			arrow.shaftRB.isKinematic = false;
			arrow.shaftRB.useGravity = true;
			arrow.shaftRB.transform.GetComponent<BoxCollider>().enabled = true;

			arrow.arrowHeadRB.isKinematic = false;
			arrow.arrowHeadRB.useGravity = true;
			arrow.arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = true;

			arrow.arrowHeadRB.AddForce( currentArrow.transform.forward * bow.GetArrowVelocity(), ForceMode.VelocityChange );
			arrow.arrowHeadRB.AddTorque( currentArrow.transform.forward * 10 );

			nocked = false;

			currentArrow.GetComponent<Arrow>().ArrowReleased( bow.GetArrowVelocity() );
			bow.ArrowReleased();

			allowArrowSpawn = false;
			Invoke( "EnableArrowSpawn", 0.5f );
			StartCoroutine( ArrowReleaseHaptics() );

			currentArrow = null;
			allowTeleport.teleportAllowed = true;
		}

		//my changes
		private void FireLaser()
		{
			GameObject laser = InstantiateLaser();

			StartCoroutine( LaserReleaseHaptics() );
			laser.transform.parent = null;
			allowTeleport.teleportAllowed = true;
		}
		//-------------------------------------------------
		private void EnableArrowSpawn()
		{
			allowArrowSpawn = true;
		}


		//-------------------------------------------------
		private IEnumerator ArrowReleaseHaptics()
		{
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 1500 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 800 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 500 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 300 );
		}

		private IEnumerator LaserReleaseHaptics()
		{
			yield return new WaitForSeconds( 0.05f );

			hand.controller.TriggerHapticPulse( 1500 );
			yield return new WaitForSeconds( 0.05f );

			hand.controller.TriggerHapticPulse( 800 );
			yield return new WaitForSeconds( 0.05f );

			hand.controller.TriggerHapticPulse( 500 );
			yield return new WaitForSeconds( 0.05f );

			hand.controller.TriggerHapticPulse( 300 );
		}

		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			gameObject.SetActive( true );
		}


		//-------------------------------------------------
		private void FindBow()
		{
			bow = hand.otherHand.GetComponentInChildren<Longbow>();
		}
	}
}
