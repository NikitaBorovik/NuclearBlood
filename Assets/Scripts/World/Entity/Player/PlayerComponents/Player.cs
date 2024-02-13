using App.World.Entity.Player.Events;
using UnityEngine;

namespace App.World.Entity.Player.PlayerComponents
{
    #region Required
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(PlayerAnimationsController))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Aim))]
    [RequireComponent(typeof(Stand))]
    #endregion
    public class Player : MonoBehaviour
    {
        #region Components
        private Transform playerTransform;
        private Animator pAnimator;
        [SerializeField]
        private PlayerDataSO playerData;
        #endregion

        #region Weapon
        [SerializeField]
        private Transform shootPosition;
        [SerializeField]
        private Transform weaponAnchor;
        [SerializeField]
        private Transform weaponPoint;
        #endregion

        #region Events
        [SerializeField]
        private AimEvent aimEvent;
        [SerializeField]
        private StandEvent standEvent;
        [SerializeField]
        private MovementEvent movementEvent;
        #endregion

        #region Parameters
        private float movementSpeed;
        #endregion

        #region Properties
        public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }
        public Animator PAnimator { get => pAnimator; }
        public Transform PlayerTransform { get => playerTransform; }
        public Transform WeaponAnchor { get => weaponAnchor; }
        public AimEvent AimEvent { get => aimEvent; }
        public StandEvent StandEvent { get => standEvent; }
        public MovementEvent MovementEvent { get => movementEvent; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public Transform WeaponPoint { get => weaponPoint; set => weaponPoint = value; }
        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            playerTransform = GetComponent<Transform>();
            pAnimator = GetComponent<Animator>();
            
            movementSpeed = playerData.speed;
        }
    }
}

