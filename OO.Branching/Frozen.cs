using System;

namespace OO.Branching
{
    public sealed class Frozen : IAccountState
    {
        public Frozen(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }

        private readonly Action OnUnfreeze;

        public IAccountState Freeze() => this;
        public IAccountState Close() => new Closed();

        public IAccountState HolderVerified() => this;

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            OnUnfreeze();
            subtractFromBalance();
            return new Active(OnUnfreeze);
        }

        public IAccountState Deposit(Action addToBalance)
        {
            OnUnfreeze();
            addToBalance();
            return new Active(OnUnfreeze);
        }
    }
}