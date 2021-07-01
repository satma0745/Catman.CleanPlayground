namespace Catman.CleanPlayground.Presentation.Users
{
    using System;
    using Catman.CleanPlayground.Services.Users;

    internal class ConsoleUsersPresentation
    {
        private readonly UserService _users;

        public ConsoleUsersPresentation(UserService userService)
        {
            _users = userService;
        }

        public void RunInteraction()
        {
            while (true)
            {
                var option = ChooseAction();

                switch (option)
                {
                    case 1:
                        ListUsers();
                        break;
                    case 2:
                        CreateUser();
                        break;
                    case 3:
                        UpdateUser();
                        break;
                    case 4:
                        DeleteUser();
                        break;
                    case 5:
                        return;
                }
            }
        }

        private static byte? ChooseAction()
        {
            Console.WriteLine("Available actions:");
            Console.WriteLine("1. List users");
            Console.WriteLine("2. Create new user");
            Console.WriteLine("3. Update user");
            Console.WriteLine("4. Delete user");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Enter action number: ");
            var option = byte.Parse(Console.ReadLine()!);
            Console.WriteLine();
            
            if (option >= 1 && option <= 5)
            {
                return option;
            }
            else
            {
                Console.WriteLine("Incorrect option number.");
                Console.WriteLine();
                
                return null;
            }
        }

        private void ListUsers()
        {
            Console.WriteLine("|  Id  |                      Username                      |");
            Console.WriteLine("|------|----------------------------------------------------|");
            
            foreach (var user in _users.GetUsers())
            {
                Console.WriteLine($"|  {user.Id:000} | {user.DisplayName,50} |");
            }
            
            Console.WriteLine();
        }

        private void CreateUser()
        {
            Console.Write("Enter user's username: ");
            var username = Console.ReadLine();
            Console.Write("Enter user's password: ");
            var password = Console.ReadLine();
            Console.Write("Enter user's display name: ");
            var displayName = Console.ReadLine();

            var registerUserModel = new RegisterUserModel()
            {
                Username = username,
                Password = password,
                DisplayName = displayName
            };

            try
            {
                _users.RegisterUser(registerUserModel);
                
                Console.WriteLine("User created successfully.");
                Console.WriteLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                Console.WriteLine();
            }
        }

        private void UpdateUser()
        {
            var userId = SelectUser();
            if (userId is null)
            {
                return;
            }
            
            Console.Write("Enter user's username: ");
            var username = Console.ReadLine();
            Console.Write("Enter user's password: ");
            var password = Console.ReadLine();
            Console.Write("Enter user's display name: ");
            var displayName = Console.ReadLine();

            var updateUserModel = new UpdateUserModel()
            {
                Id = userId.Value,
                Username = username,
                Password = password,
                DisplayName = displayName
            };

            try
            {
                _users.UpdateUser(updateUserModel);
                
                Console.WriteLine($"User with id {userId:000} updated successfully.");
                Console.WriteLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                Console.WriteLine();
            }
        }

        private void DeleteUser()
        {
            var userId = SelectUser();
            if (userId is null)
            {
                return;
            }

            try
            {
                _users.DeleteUser(userId.Value);

                Console.WriteLine($"User with id {userId:000} deleted successfully.");
                Console.WriteLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                Console.WriteLine();
            }
        }

        private byte? SelectUser()
        {
            Console.Write("Choose user (enter user's id): ");
            var id = byte.Parse(Console.ReadLine()!);

            if (_users.UserExists(id))
            {
                Console.WriteLine();
                
                return id;
            }
            else
            {
                Console.WriteLine("User with such id does not exist.");
                Console.WriteLine();

                return null;
            }
        }
    }
}
