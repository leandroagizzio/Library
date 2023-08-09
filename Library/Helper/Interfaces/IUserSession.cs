using CoreLibrary.Models.Interfaces;

namespace Library.Helper.Interfaces
{
    public interface IUserSession
    {
        void CreateUserSession(IUser user);
        void RemoveUserSession();
        IUser GetUserSession();
    }
}
