namespace Catman.CleanPlayground.Application.UseCases.Common.RequestBroker
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Microsoft.Extensions.DependencyInjection;

    internal class RequestBroker : IRequestBroker
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestBroker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task<IResponse<TResource>> SendRequestAsync<TRequest, TResource>(TRequest request)
            where TRequest : IRequest =>
            _serviceProvider
                .GetRequiredService<IRequestHandler<TRequest, TResource>>()
                .HandleRequestAsync(request);
    }
}
