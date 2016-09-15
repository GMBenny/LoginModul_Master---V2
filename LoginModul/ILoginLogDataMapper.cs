namespace LoginModul
{
    public interface ILoginLogDataMapper
    {
        void Create(LoginLog data);
        long ReadMaxID();
    }
}