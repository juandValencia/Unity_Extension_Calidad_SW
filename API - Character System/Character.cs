/*
 * Autores
 * Santiago Morales Alvarez
 * Juan Diego Valencia
 * Cristian David Gomez
*/

using UnityEngine;
using System;
using UnityEngine.AI;
using CharacterSystem.Skills;
using CharacterSystem.Composition;

namespace CharacterSystem
{
    public enum CharacterState
    {
        hide,
        inSpawn,
        active,
        inDead
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]

    [RequireComponent(typeof(DefenceCharacter))]
    [RequireComponent(typeof(EnergyCharacter))]
    [RequireComponent(typeof(LifeCharacter))]
    [RequireComponent(typeof(SpeedCharacter))]
    [RequireComponent(typeof(TenacyCharacter))]
    [RequireComponent(typeof(CognitiveControlCharacter))]
    public class Character : MonoBehaviour
    {
        #region attributes
        [SerializeField]
        [InspectorName("Name")]
        private String characterName;

        public delegate void NotifyState(Character m_character, CharacterState state);
        public event NotifyState e_NewState;

        private AttributeComposition m_basicAttributes;
        private int level;
        [SerializeField]
        [InspectorName("List of Skills")]
        protected Skill[] skills;
        private Character_Control m_controlPlayer;
        private CharacterState state;

        private Animator m_Animator;
        private NavMeshAgent m_NavAgent;
        private MovIA m_movIA;
        private MovJoystick m_movJoystick;
        #endregion

        #region properties
        public String CharacterName
        {
            get
            {
                return characterName;
            }

        }
        public Animator Animator
        {
            get
            {
                return m_Animator;
            }

        }
        public NavMeshAgent NavAgent
        {
            get
            {
                return m_NavAgent;
            }

        }
        public MovIA MovIA
        {
            get
            {
                return m_movIA;
            }
        }
        public MovJoystick MovJoystick
        {
            get
            {
                return m_movJoystick;
            }
        }
        public AttributeComposition Attributes
        {
            get
            {
                return m_basicAttributes;
            }
        }
        public Skill[] Skills
        {
            get
            {
                return skills;
            }
        }
        public Character_Control ControlPlayer
        {
            get
            {
                return m_controlPlayer;
            }
            set
            {
                m_controlPlayer = value;
            }
        }
        public CharacterState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        #endregion

        protected void Awake()
        {
            m_Animator = this.GetComponent<Animator>();
            m_NavAgent = this.GetComponent<NavMeshAgent>();
            try
            {
                m_movIA = this.gameObject.GetComponentInChildren<MovIA>();
            }
            catch
            {
            }
            try
            {
                m_movJoystick = this.gameObject.GetComponentInChildren<MovJoystick>();
            }
            catch
            {
            }

            skills = this.GetComponents<Skill>();

            BuildAttributes();
        }

        public void BuildAttributes()
        {
            m_basicAttributes = new AttributeComposition(this.GetComponent<LifeCharacter>(), this.GetComponent<EnergyCharacter>(),
                this.GetComponent<DefenceCharacter>(), this.GetComponent<SpeedCharacter>(), this.GetComponent<TenacyCharacter>(),
                this.GetComponent<CognitiveControlCharacter>());

            m_basicAttributes.LifeCharacter.e_withoutLife -= Dead;
            m_basicAttributes.LifeCharacter.e_withoutLife += Dead;
        }

        public void UpgradeLevel()
        { 
            //a character start in 0 level
            level++;
        }

        #region states

        private void UpdateState(CharacterState t_state)
        {
            state = t_state;
            m_Animator.SetTrigger("changeState");
            try
            {
                e_NewState(this, t_state);
            }
            catch
            {
                Debug.Log("Exception, without state");
            }
        }

        /// <summary>
        /// It's killed by its own life attributte
        /// </summary>
        private void Dead()
        {
            level = 0;
            m_Animator.SetTrigger("die");
            UpdateState(CharacterState.inDead);
        }

        /// <summary>
        /// It's invoke by the GAME MODE
        /// </summary>
        public void FinishDead()
        {
            UpdateState(CharacterState.hide);
        }

        /// <summary>
        /// It's invoke by the GAME MODE
        /// </summary>
        public void Spawn()
        {
            UpdateState(CharacterState.inSpawn);

        }

        /// <summary>
        /// It's invoke by the ANIMATOR
        /// </summary>
        public void FinishSpawn()
        {
            UpdateState(CharacterState.active);
        }

        #endregion

    }


}
