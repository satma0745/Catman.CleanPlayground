namespace Catman.CleanPlayground.Application.UseCases.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Users.DeleteUser;
    using Catman.CleanPlayground.Application.UseCases.Users.GetUsers;
    using Catman.CleanPlayground.Application.UseCases.Users.RegisterUser;
    using Catman.CleanPlayground.Application.UseCases.Users.UpdateUser;

    public interface IUserService
    {
        Task<IResponse<ICollection<UserResource>>> GetUsersAsync();

        Task<IResponse<BlankResource>> RegisterUserAsync(RegisterUserRequest registerRequest);

        Task<IResponse<BlankResource>> UpdateUserAsync(UpdateUserRequest updateRequest);

        Task<IResponse<BlankResource>> DeleteUserAsync(DeleteUserRequest deleteRequest);
    }
}
