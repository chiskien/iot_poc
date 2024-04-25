using UnityEngine;

namespace State
{
    public class GoingDownState : IState
    {
        [SerializeField] private Animator _animator;

        public void OnEnterState()
        {

        }

        public void Observe()
        {
            throw new System.NotImplementedException();
        }
    }
}