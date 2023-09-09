using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTSCore
{
	public class RTSCamera : MonoBehaviour
	{
#region General
		
		public PlayerInput input;

		[Header("Hierarchy")]
		public Transform vAxis;
		public Transform hAxis;
		public Transform cAxis;
		public Transform eye;


		private InputAction _moveAction;
		private InputAction _holdAction;
		private InputAction _loopAction;
		private InputAction _zoomAction;

		private void Start()
		{
			_moveAction = input.actions["move"];
			_holdAction = input.actions["hold"];
			_loopAction = input.actions["look"];
			_zoomAction = input.actions["zoom"];

			InitMovement();
			InitZoom();
		}

		private void Update()
		{
			UpdateMovement();
			UpdateRotation();
			UpdateHeight();
			UpdateZoom();
			UpdatePushCamera();
		}

#endregion


#region Movement

		[Header("Movement")]
		public float moveDuration;
		public  float   moveSpeedMinZoom;
		public  float   moveSpeedMaxZoom;
		private Vector3 _movementVelocity;
		private float   _reverseDuration;

		private void InitMovement()
		{
			_reverseDuration = 1 / moveDuration;
		}
		
		private void UpdateMovement()
		{
			var offset = _moveAction.ReadValue<Vector2>();

			var moveSpeed = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoomCurrent);
			var nextPoint = (moveSpeed * _reverseDuration) * (vAxis.forward * offset.y + vAxis.right * offset.x);

			var pos = vAxis.position;
			vAxis.position = Vector3.SmoothDamp(pos, pos + nextPoint, ref _movementVelocity, moveDuration);
		}

#endregion


#region Look Rotation

		[Header("Look Rotation")]
		public float lookSpeed;

		private void UpdateRotation()
		{
			if (_holdAction.phase != InputActionPhase.Performed)
				return;

			var offset = lookSpeed * _loopAction.ReadValue<Vector2>();

			vAxis.RotateAround(vAxis.position, vAxis.up, offset.x);

			var angles = hAxis.rotation.eulerAngles;
			angles.x       = Mathf.Clamp(angles.x - offset.y, 0, 90);
			hAxis.rotation = Quaternion.Euler(angles);
		}

#endregion


#region Height

		[Header("Height positioning")]
		public  float     heightMax;
		public  float     heightOffset;
		public  float     heightDuration;
		public  LayerMask terrainLayers;
		private float     _heightTarget;
		private float     _heightVelocity;

		private void UpdateHeight()
		{
			var ray = new Ray(vAxis.position + Vector3.up * heightMax, Vector3.down);
			if (Physics.Raycast(ray, out var hit, heightMax, terrainLayers))
			{
				Debug.DrawRay(hit.point, Vector3.up, Color.blue, 5);
				_heightTarget = hit.point.y + heightOffset;
			}
			
			var pos = hAxis.position;
			pos.y          = Mathf.SmoothDamp(pos.y, _heightTarget, ref _heightVelocity, heightDuration);
			hAxis.position = pos;
		}

#endregion


#region Zoom

		[Header("Zooming")]
		public  float zoomSpeed;
		public  float zoomDuration;
		[Range(0f, 1f)]
		public  float zoomCurrent;
		public  float zoomMinZ = -100;
		public  float zoomMaxZ = -5;
		private float _zoomTarget;
		private float _zoomVelocity;

		private void InitZoom()
		{
			_zoomTarget = zoomCurrent;
		}
		
		private void UpdateZoom()
		{
			var zoom = _zoomAction.ReadValue<Vector2>();
			_zoomTarget = Mathf.Clamp(_zoomTarget + zoom.y * zoomSpeed, 0, 1);
			zoomCurrent = Mathf.SmoothDamp(zoomCurrent, _zoomTarget, ref _zoomVelocity, zoomDuration);
			
			var pos = cAxis.localPosition;
			
			pos.z = Mathf.Lerp(zoomMaxZ, zoomMinZ, zoomCurrent);
			
			cAxis.localPosition = pos;
		}

#endregion


#region Push Camera

		[Header("Push Camera UP")]
		public float pushDuration;
		public float pushMaxHeight;
		public float pushOffset;

		private float _pushTarget;
		private float _pushVelocity;

		private void UpdatePushCamera()
		{
			var up  = cAxis.up;
			var ray = new Ray(cAxis.position + up * pushMaxHeight, -up);
			_pushTarget = 0;
			if (Physics.Raycast(ray, out var hit, pushMaxHeight + pushOffset, terrainLayers))
			{
				Debug.DrawRay(hit.point, Vector3.up, Color.black, 5);
				Debug.DrawLine(hit.point, cAxis.position + cAxis.up * pushMaxHeight, Color.black, 5);
				_pushTarget = pushOffset + Vector3.Dot(hit.point - cAxis.position, up);
				if (_pushTarget < 0)
					_pushTarget = 0;
			}

			var pos = eye.localPosition;
			pos.y             = Mathf.SmoothDamp(pos.y, _pushTarget, ref _pushVelocity, pushDuration);
			eye.localPosition = pos;
		}

#endregion


#region Gizmos
#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			const float thickness = 3;

			var posDown   = vAxis.position;
			var posUp     = posDown + Vector3.up * heightMax;
			var posMiddle = hAxis.position;

			var color = Handles.color;
			Handles.color = Color.black;
			Handles.DrawWireArc(posDown, Vector3.up, Vector3.one, 360, 3, thickness);
			Handles.DrawWireArc(posMiddle, Vector3.up, Vector3.one, 360, 1, thickness);
			Handles.DrawWireArc(posUp, Vector3.up, Vector3.one, 360, 3, thickness);
			Handles.DrawLine(posDown, posUp, 3);
			Handles.color = color;
		}

#endif
#endregion
	}
}