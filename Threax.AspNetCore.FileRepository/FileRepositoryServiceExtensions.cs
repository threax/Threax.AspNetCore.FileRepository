using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.FileRepository;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Options for the file repository.
    /// </summary>
    public class FileRepositoryOptions
    {
        /// <summary>
        /// Callback function to 
        /// </summary>
        public Action<IFileVerifier> ConfigureVerifier { get; set; }

        public String OutputDir { get; set; }
    }

    public static class FileRepositoryServiceExtensions
    {
        /// <summary>
        /// Add a file repository. Use ConfigureVerifier in the options to add the file types you want to support.
        /// </summary>
        /// <param name="services">The service colleciton to add the file verifier to.</param>
        /// <param name="options">The options.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFileRepository(this IServiceCollection services, FileRepositoryOptions options)
        {
            if (String.IsNullOrEmpty(options.OutputDir))
            {
                throw new InvalidOperationException("You must include an output directory");
            }

            if(options.ConfigureVerifier == null)
            {
                throw new InvalidOperationException("You must include a function to configure your file validator.");
            }

            services.AddSingleton<IFileVerifier>(s =>
            {
                var verifier = new FileVerifier();
                options.ConfigureVerifier(verifier);
                return verifier;
            });
            services.AddSingleton<IFileRepository>(s =>
            {
                return new FileRepository<IFileRepository>(options.OutputDir, s.GetRequiredService<IFileVerifier>());
            });

            return services;
        }

        /// <summary>
        /// Add a file repository. Use ConfigureVerifier in the options to add the file types you want to support.
        /// This version allows you to use a type for injection so you can have multiple file repositories with different
        /// configurations.
        /// </summary>
        /// <typeparam name="InjectT">The type to uniquly identify this repository with. This type is not used anywhere in the actual repository.</typeparam>
        /// <param name="services">The service colleciton to add the file verifier to.</param>
        /// <param name="options">The options.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFileRepository<InjectT>(this IServiceCollection services, FileRepositoryOptions options)
        {
            if (String.IsNullOrEmpty(options.OutputDir))
            {
                throw new Exception("You must include an output directory");
            }

            if (options.ConfigureVerifier == null)
            {
                throw new InvalidOperationException("You must include a function to configure your file validator.");
            }

            services.AddSingleton<IFileVerifier<InjectT>>(s =>
            {
                var verifier = new FileVerifier<InjectT>();
                options.ConfigureVerifier(verifier);
                return verifier;
            });

            services.AddSingleton<IFileRepository<InjectT>>(s =>
            {
                return new FileRepository<InjectT>(options.OutputDir, s.GetRequiredService<IFileVerifier<InjectT>>());
            });

            return services;
        }
    }
}
