using LightsOutPuzzle.Application.Interfaces;
using LightsOutPuzzle.Application.Service;
using LightsOutPuzzle.Domain.Interfaces.Repositories;
using LightsOutPuzzle.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LightsOutPuzzle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            // We add as singleton since we want to keep state of the game between http request.
            service.AddSingleton<ICurrentGameRepository, CurrentGameRepository>();
            service.AddScoped<ILightsPuzzleGameService, LightsPuzzleGameService>();

            return service;
        }
    }
}