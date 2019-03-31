using System;

namespace OO.Branching
{
    public sealed class Active : IAccountState
    {
        public Active(Action onUnfreeze)
        {
            _onUnfreeze = onUnfreeze;
        }

        private readonly Action _onUnfreeze;

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Withdraw(Action subtractFromBalance)
        {
            subtractFromBalance();
            return this;
        }

        public IAccountState Freeze() => new Frozen(_onUnfreeze);
        public IAccountState Close() => new Closed();

        public IAccountState HolderVerified() => this;

        public IAccountState Withdraw() => this;

        public IAccountState Deposit() => this;
    }
}
