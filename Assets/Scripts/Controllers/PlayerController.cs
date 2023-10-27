using System;
using System.Collections;
using ColliderBase;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private const string Running = "isRunning";
        private const string Jumping = "isJumping";
    
        private const float GravityValue = -9.81f;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ColliderChecker _colliderChecker;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private TorchController _torchController;
        [SerializeField] private float _moveForwardSpeed;
        [SerializeField] private float _moveSideSpeed;
        [SerializeField] private float _jumpHeight = 5.0f;

        private Camera _camera;
        private Vector3 _lastPosition;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private bool _isJumping;
        private bool _isMoving;
        private bool _isFinish;
        private bool _isStop;
        private bool _startRun;
        private Coroutine _coroutine;

        public Action OnEndGame;

        private Vector2 Axis { get; set; }

        private void Start()
        {
            _camera = Camera.main;
            _lastPosition = transform.position;
            _coroutine = null;

            _torchController.OnLightOff += SetFinish;
        }

        private void Update()
        {
            if (!_startRun)
                return;
            
            if (_isFinish)
            {
                CheckTorchLight();

                return;
            }

            Axis = SimpleInputAxis();

            Vector3 movementVector = Vector3.zero;
            Vector3 forwardVector = new Vector3(-_moveForwardSpeed, 0, 0);

            if (Axis.sqrMagnitude > 0.001f)
            {
                movementVector = _camera.transform.TransformDirection(Axis);
                movementVector.y = 0;
                movementVector.x = 0;
                movementVector.Normalize();
            }

            if (Axis.y > 0.3f)
            {
                OnJump();
            }

            UpdatePlayerRotation();

            var distanceTravelled = (transform.position - _lastPosition).magnitude;

            if (distanceTravelled > 0.01f && !_isMoving)
            {
                _coroutine = StartCoroutine(StartScoreAccrual());
            }

            _lastPosition = transform.position;

            _characterAnimator.SetBool(Running, true);
            _characterController.Move(_moveSideSpeed * movementVector * Time.deltaTime);
            _characterController.Move(forwardVector * Time.deltaTime);
            
            MovementJump();
        }

        private void OnDestroy()
        {
            _torchController.OnLightOff -= SetFinish;
        }

        public void SetTorchLight(int torchBurningValue, float torchLightValue)
        {
            _torchController.SetTorchValues(torchBurningValue, torchLightValue);

            TorchGasController.ChangeTorchGas(torchBurningValue);
        }

        public void StartRun()
        {
            _startRun = true;
        }

        public void SetFinish()
        {
            _isFinish = true;
            _isStop = true;
        }

        private IEnumerator StartScoreAccrual()
        {
            _isMoving = true;

            while (enabled)
            {
                yield return new WaitForSeconds(1);

                ScoreCounterController.SetValue(1);
            }
        }

        private void CheckTorchLight()
        {
            if (_isStop)
            {
                _characterAnimator.SetBool(Running, false);
                
                StopCoroutine(_coroutine);
                OnEndGame?.Invoke();

                _isStop = false;
            }
            
            _characterAnimator.SetFloat(Jumping, _characterController.velocity.y);
            _playerVelocity.y += GravityValue * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }

        private void UpdatePlayerRotation()
        {
            if (Axis.x > 0.01f && _groundedPlayer)
            {
                transform.forward = Vector3.Lerp(transform.forward, (transform.position - _lastPosition).normalized,
                    5f * Time.deltaTime);
            }
            else if (Axis.x < -0.01f && _groundedPlayer)
            {
                transform.forward = Vector3.Lerp(transform.forward, (transform.position + -_lastPosition).normalized,
                    5f * Time.deltaTime);
            }
            else if (_groundedPlayer)
            {
                transform.forward = Vector3.Lerp(transform.forward, Vector3.left, 5f * Time.deltaTime);
            }
        }

        private void MovementJump()
        {
            _groundedPlayer = _colliderChecker.IsInLayer;

            if (_groundedPlayer)
            {
                _playerVelocity.y = 0.0f;
            }

            _playerVelocity.y += GravityValue * Time.deltaTime;
            
            if (_isJumping && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2f * GravityValue);
                _isJumping = false;
            }
            
            _characterController.Move(_playerVelocity * Time.deltaTime);
            _characterAnimator.SetFloat(Jumping, _characterController.velocity.y);
        }

        private void OnJump()
        {
            if (_characterController.velocity.y == 0)
            {
                _isJumping = true;
            }
        }

        private Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        }
    }
}