namespace Catman.CleanPlayground
{
    using Catman.CleanPlayground.Data.Users;
    using Catman.CleanPlayground.Presentation.Users;
    using Catman.CleanPlayground.Services.Users;

    internal static class Program
    {
        private static void Main()
        {
            var userRepository = new InMemoryUserRepository();
            var userService = new UserService(userRepository);
            var userPresentation = new ConsoleUsersPresentation(userService);
            
            userPresentation.RunInteraction();
        }
    }
}
