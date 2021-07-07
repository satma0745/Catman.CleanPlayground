namespace Catman.CleanPlayground.Application.UseCases.Users.GetUsers
{
    using System.Collections.Generic;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;

    public class GetUsersRequest : RequestBase<ICollection<UserResource>>
    {
    }
}
