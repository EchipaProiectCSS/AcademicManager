using Database.Interfaces;

namespace ProcessManagement.Interfaces
{
    public interface IDatabaseModel
    {
        void SetInstance();
        IDatabase GetInstance();
    }
}
