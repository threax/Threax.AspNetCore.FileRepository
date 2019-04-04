# Threax.AspNetCore.FileRepository
This library provides an abstract file repository with validation. It also allows the root directory where files are saved to be easily configurable without needing to change your app.

## How to Use
The most basic usage will look something like the following in ConfigureServices in Startup.cs:
```
services.AddFileRepository(o =>
{
    o.ConfigureVerifier = v =>
    {
        v.AddGif()
         .AddJpeg()
         .AddPng();
    };
    o.UseLocalFiles(lfo => lfo.RootDir = appConfig.UploadDir);
});
```
This first defines that we want to use the FileRepository by calling `AddFileRepository`
which will enable us to inject IFileRepository objects into our consuming classes. You can 
optionally use a type when calling `AddFileRepository<T>` which will allow you to inject
`IFileRepository<T>` objects, which allows for multiple file repositories to be defined.

Next we setup a method for `ConfigureVerifier`. This is where we define the file types that
our repository is allowed to use. By default only files that you define will be allowed to be uploaded.
If you set `AllowUnknownFiles` to true then all file types will be allowed, but any types you define
will still be validated and rejected if they do not meet the criteria.

Finally the call to `UseLocalFiles` tells this repository to use the local file system with the 
`RootDir` set to the `UploadDir` of the appConfig object.