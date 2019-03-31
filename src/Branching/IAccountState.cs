using System;

namespace OO.Branching
{
    public interface IAccountState
    {
        IAccountState Deposit(Action addToBalance);
        IAccountState Withdraw(Action subtractFromBalance);
        IAccountState Freeze();
        IAccountState Close();
        IAccountState HolderVerified();
    }
}
