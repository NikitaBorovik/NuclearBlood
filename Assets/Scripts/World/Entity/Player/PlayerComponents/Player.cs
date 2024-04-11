using App.UI.Events;
using App.Upgrades;
using App.World.Entity.Player.Events;
using App.World.Entity.Player.Weapons;
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
    public class Player : MonoBehaviour, IUpgradable, IKillable
    {
        #region Components
        private Transform playerTransform;
        private Animator pAnimator;
        private Health health;
        [SerializeField]
        private PlayerDataSO playerData;
        #endregion

        #region Weapon
        [SerializeField]
        private Transform shootPosition;
        [SerializeField]
        private Transform weaponAnchor;
        [SerializeField]
        private GameObject curWeaponObj;
        private Weapon weapon;
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
        [SerializeField]
        private CountUpdatedEvent countUpdatedEvent;
        [SerializeField]
        private DieEvent dieEvent;
        #endregion

        #region Parameters
        private float movementSpeed;
        private int money;
        private bool isDead;
        private float dodgeChance = 0f;
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
        public GameObject CurWeaponObj { get => curWeaponObj; set => curWeaponObj = value; }

        public Weapon Weapon {get => weapon; set => weapon = value;}
        public int Money { get => money; set { money = value; countUpdatedEvent?.CallCountUpdatedEvent(value); } }
        public Health Health { get => health; set => health = value; }

        public float DodgeChance
        {
            get => dodgeChance;
            set => dodgeChance = Mathf.Clamp01(value); // for dexterity upgarde
        }
        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            playerTransform = GetComponent<Transform>();
            pAnimator = GetComponent<Animator>();
            weapon = CurWeaponObj.GetComponent<Weapon>();
            health = GetComponent<Health>();
            health.MaxHealth = playerData.maxHealth;
            movementSpeed = playerData.speed;
        }

        public void Die()
        {
            Weapon.enabled = false;
            GetComponent<Movement>().enabled = false;
            GetComponent<Aim>().enabled = false;
            GetComponent<PlayerAnimationsController>().enabled = false;
            if (isDead) return;
            dieEvent.CallDieEvent();
        }

        public void EnableUpgrade(IUpgradeAbstractVisitor upgrade)
        {
            IUpgradable.EnableUpgradeViaVisitorOf(this, upgrade);
        }
    }
}

