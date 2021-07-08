namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandling
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using FluentValidation;
    using MediatR;

    internal class RequestValidationPipelineBehavior<TAnyRequest, TResponse>
        : PipelineBehaviorBase<TAnyRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TAnyRequest>> _requestValidators;
        
        public RequestValidationPipelineBehavior(IEnumerable<IValidator<TAnyRequest>> requestValidators)
        {
            _requestValidators = requestValidators;
        }
        
        protected override async Task<IResponse<TResource>> HandleAsync<TRequest, TResource>(
            TRequest request,
            RequestHandlerDelegate<IResponse<TResource>> next)
        {
            foreach (var requestValidator in _requestValidators)
            {
                var validationResult = await requestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new Response<TResource>(validationError);
                }
            }
            
            return await next();
        }
    }
}
