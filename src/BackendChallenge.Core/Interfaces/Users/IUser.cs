namespace BackendChallenge.Core.Interfaces.Users
{
    public interface IUser
    {
        public Entities.User? RequestUser { get; set; }
    }

    public class User : IUser
    {
        public Entities.User? RequestUser { get; set; }
    }
}
