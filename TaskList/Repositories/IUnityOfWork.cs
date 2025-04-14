namespace TaskList.Repositories
{
    public interface IUnityOfWork
    {
        ITaskRepository TaskRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
