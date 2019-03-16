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
    public class FileRepositoryOptions<InjectT>
    {
        /// <summary>
        /// Callback function to configure the IFileVerifier.
        /// </summary>
        public Action<IFileVerifier> ConfigureVerifier { get; set; }

        /// <summary>
        /// Callback to create the IFileRepository. You can use functions like UseFilesystem to configure this.
        /// </summary>
        public Func<IServiceProvider, IFileRepository<InjectT>> CreateFileRepository;
    }

    public static class FileRepositoryServiceExtensions
    {
        public static FileRepositoryOptions<FileRepository.Handle> UseLocalFiles(this FileRepositoryOptions<FileRepository.Handle> options, Action<FileSystemOptions> config)
        {
            return UseLocalFiles<FileRepository.Handle>(options, config);
        }

        public static FileRepositoryOptions<InjectT> UseLocalFiles<InjectT>(this FileRepositoryOptions<InjectT> options, Action<FileSystemOptions> config)
        {
            var fileSystemOptions = new FileSystemOptions();
            config.Invoke(fileSystemOptions);

            if(options.CreateFileRepository != null)
            {
                throw new InvalidOperationException("Only configure CreateFileRepository one time.");
            }

            if (String.IsNullOrEmpty(fileSystemOptions.RootDir))
            {
                throw new InvalidOperationException("You must include an output directory");
            }

            options.CreateFileRepository = s => new FileRepository<InjectT>(fileSystemOptions.RootDir, s.GetRequiredService<IFileVerifier>());
            return options;
        }

        /// <summary>
        /// Add a file repository. Use ConfigureVerifier in the options to add the file types you want to support.
        /// </summary>
        /// <param name="services">The service colleciton to add the file verifier to.</param>
        /// <param name="config">The configuration callback.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddFileRepository(this IServiceCollection services, Action<FileRepositoryOptions<FileRepository.Handle>> config)
        {
            var options = new FileRepositoryOptions<FileRepository.Handle>();
            config.Invoke(options);

            if(options.ConfigureVerifier == null)
            {
                throw new InvalidOperationException("You must include a function to configure your file validator.");
            }

            if(options.CreateFileRepository == null)
            {
                throw new InvalidOperationException("You must configure the file repository. Call UseFilesystem on your options to do this.");
            }

            services.AddSingleton<IFileVerifier>(s =>
            {
                var verifier = new FileVerifier();
                options.ConfigureVerifier(verifier);
                return verifier;
            });

            services.AddSingleton<IFileRepository>(s => options.CreateFileRepository(s));

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
        public static IServiceCollection AddFileRepository<InjectT>(this IServiceCollection services, Action<FileRepositoryOptions<InjectT>> config)
        {
            var options = new FileRepositoryOptions<InjectT>();
            config.Invoke(options);

            if (options.ConfigureVerifier == null)
            {
                throw new InvalidOperationException("You must include a function to configure your file validator.");
            }

            if (options.CreateFileRepository == null)
            {
                throw new InvalidOperationException("You must configure the file repository. Call UseFilesystem on your options to do this.");
            }

            services.AddSingleton<IFileVerifier<InjectT>>(s =>
            {
                var verifier = new FileVerifier<InjectT>();
                options.ConfigureVerifier(verifier);
                return verifier;
            });

            services.AddSingleton<IFileRepository<InjectT>>(s => options.CreateFileRepository(s));

            return services;
        }
    }
}
