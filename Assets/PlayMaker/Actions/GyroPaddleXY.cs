// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	/// <summary>
	/// Action version of Unity's builtin MouseLook behaviour.
	/// TODO: Expose invert Y option.
	/// </summary>
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Rotates a GameObject based on mouse movement. Minimum and Maximum values can be used to constrain the rotation.")]
	public class GyroPaddleXY : ComponentAction<Rigidbody>
	{
		public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The axes to rotate around.")]
		public RotationAxes axes = RotationAxes.MouseXAndY;

		[RequiredField]
		public FsmFloat sensitivityX;

		[RequiredField]
		public FsmFloat sensitivityY;

		[RequiredField]
		[HasFloatSlider(-360,360)]
		public FsmFloat minimumX;

		[RequiredField]
		[HasFloatSlider(-360, 360)]
		public FsmFloat maximumX;

		[RequiredField]
		[HasFloatSlider(-360, 360)]
		public FsmFloat minimumY;

		[RequiredField]
		[HasFloatSlider(-360, 360)]
		public FsmFloat maximumY;

		[RequiredField]
		[HasFloatSlider(-360, 360)]
		public float Zmultiplier;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		float rotationX;
		float rotationY;
		//float _mouselookFsmFloat;

		public override void Reset()
		{
			gameObject = null;
			axes = RotationAxes.MouseXAndY;
			sensitivityX = 15f;
			sensitivityY = 15f;
			minimumX = -360f;
			maximumX = 360f;
			minimumY = -60f;
			maximumY = 60f;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				Finish();
				return;
			}

            // Make the rigid body not change rotation			
            // TODO: Original Unity script had this. Expose as option?
            if (!UpdateCache(go))
            {
                if (rigidbody)
                {
                    rigidbody.freezeRotation = true;
                }
            }

			DoMouseLook();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoMouseLook();
		}

		void DoMouseLook()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			var transform = go.transform;
			var globalVariables = FsmVariables.GlobalVariables;
			var randomFloatXLast = globalVariables.GetFsmFloat ("randomFloatXLast");
			var randomFloatYLast = globalVariables.GetFsmFloat ("randomFloatYLast");
			var pushAmount = (GetXRotation ()) * 1;
			var pushAmountY = (GetYRotation ()) * 1;
			var rotateAmount = GetXRotation ();

			switch (axes)
			{
				case RotationAxes.MouseXAndY:
					
				transform.localEulerAngles = new Vector3(transform.localPosition.x, GetYRotation (), 0);
				pushAmount = (GetXRotation ()) * 1;
				pushAmountY = (GetYRotation ()) * 1;
					break;
				
			case RotationAxes.MouseX:
				transform.localPosition = new Vector3 (GetXRotation (),  GetYRotation (), 0);
				// rotation of z here 
				// transform.localEulerAngles = new Vector3 (0, 0, (transform.localRotation.z)- (GetXRotation ()*Zmultiplier) );
				pushAmount = (GetXRotation ()) * 1;
				pushAmountY = (GetYRotation ()) * 1;
					break;

			case RotationAxes.MouseY:

				transform.localEulerAngles = new Vector3 (transform.localPosition.x, GetYRotation (), 0);
				//transform.localRotation = new Vector3 (transform.localRotation.x, transform.localRotation.y, GetXRotation ());
				break;
			}
		}



		float GetXRotation()
		{
			rotationX += Input.acceleration.x * sensitivityX.Value;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			return rotationX;
		}

		float GetYRotation()
		{
			
			rotationY += ((Input.acceleration.y -Input.acceleration.z) * sensitivityY.Value);
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			return rotationY;
		}

		// Clamp function that respects IsNone
		static float ClampAngle(float angle, FsmFloat min, FsmFloat max)
		{
			if (!min.IsNone && angle < min.Value)
			{
				angle = min.Value;
			}

			if (!max.IsNone && angle > max.Value)
			{
				angle = max.Value;
			}
			
			return angle;
		}
	}
}