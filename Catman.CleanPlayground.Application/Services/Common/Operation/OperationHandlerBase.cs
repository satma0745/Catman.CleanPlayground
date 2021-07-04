namespace Catman.CleanPlayground.Application.Services.Common.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using FluentValidation;
    using FluentValidation.Results;

    internal abstract class OperationHandlerBase<TRequest, TResource> : IOperation<TRequest, TResource>
        where TRequest : RequestBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _requestValidators;
        private readonly IUserRepository _userRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;
        
        protected virtual bool RequireAuthorizedUser => false;

        protected OperationHandlerBase(
            IEnumerable<IValidator<TRequest>> requestValidators,
            IUserRepository userRepository,
            ITokenManager tokenManager,
            IMapper mapper)
        {
            _requestValidators = requestValidators;
            _userRepository = userRepository;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<TResource>> PerformAsync(TRequest request)
        {
            try
            {
                var validationResult = await ValidateRequestAsync(request);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<TResource>(validationError);
                }

                var authenticationResult = _tokenManager.AuthenticateToken(request.AuthorizationToken);

                if (RequireAuthorizedUser)
                {
                    if (!authenticationResult.IsValid)
                    {
                        var authenticationError = new AuthenticationError(authenticationResult.ErrorMessage);
                        return new OperationResult<TResource>(authenticationError);
                    }
                    else if (!await _userRepository.UserExistsAsync(authenticationResult.UserId!.Value))
                    {
                        var authenticationError = new AuthenticationError("Token owner does not exist.");
                        return new OperationResult<TResource>(authenticationError);
                    }
                }

                var (userExists, currentUser) = await TryGetApplicationUserAsync(authenticationResult.UserId);
                var operationParams = new OperationParameters<TRequest>
                {
                    Authorized = userExists,
                    CurrentUser = currentUser,
                    Request = request
                };

                return await HandleRequestAsync(operationParams);
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult<TResource>(fatalError);
            }
        }

        protected abstract Task<OperationResult<TResource>> HandleRequestAsync(
            OperationParameters<TRequest> operationParameters);

        private async Task<ValidationResult> ValidateRequestAsync(TRequest request)
        {
            foreach (var requestValidator in _requestValidators)
            {
                var validationResult = await requestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
            }

            var validationPassed = new ValidationResult();
            return validationPassed;
        }

        private async Task<(bool UserExists, ApplicationUser User)> TryGetApplicationUserAsync(Guid? userId)
        {
            var userExists = userId is not null && await _userRepository.UserExistsAsync(userId.Value);
            if (!userExists)
            {
                return (false, default);
            }

            var userData = await _userRepository.GetUserAsync(userId.Value);
            var applicationUser = _mapper.Map<ApplicationUser>(userData);
            return (true, applicationUser);
        }
    }
}
