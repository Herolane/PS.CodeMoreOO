using System;

namespace OO.Branching
{
    public class AccountV2
    {
        public decimal Balance { get; private set; }
        private IAccountState State { get; set; }
        
        
        public AccountV2(Action onUnfreeze)
        {
            State = new NotVerified(onUnfreeze);
        } 

        public void Deposit(decimal amount)
        {
            State = State.Deposit(() => Balance += amount);
        }


        public void Withdraw(decimal amount)
        {
            State = State.Withdraw(() => Balance -= amount);
        }

        public void HolderVerified()
        {
            State = State.HolderVerified();
        }
        
        public void Close()
        {
            State = State.Close();
        }

        public void Freeze()
        {
            State = State.Freeze();
        }
    }

    public class AccountV1
    {
        private bool IsClosed;
        private bool IsFrozen;
        private bool IsVerified;
        private readonly Action OnUnfreeze;

        public AccountV1(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }

        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            if (IsClosed) return;

            if (IsFrozen) OnUnfreeze();

            Balance += amount;
        }


        public void Withdraw(decimal amount)
        {
            if (!IsVerified) return;

            if (IsClosed) return;

            if (IsFrozen) OnUnfreeze();

            Balance -= amount;
        }
    }
}
