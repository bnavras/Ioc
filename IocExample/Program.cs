using System;
using IocExample.Classes;
using Ninject;

namespace IocExample
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var logger = new ConsoleLogger();
        //    var sqlConnectionFactory = new SqlConnectionFactory("SQL Connection", logger);
        //    var createUserHandler = new CreateUserHandler(new UserService(new QueryExecutor(sqlConnectionFactory), new CommandExecutor(sqlConnectionFactory), new CacheService(logger, new RestClient("API KEY"))), logger);

        //    createUserHandler.Handle();
        //}

        //static void Main(string[] args)
        //{
        //    IKernel idiKernel = new StandardKernel();

        //    idiKernel.Bind<ILogger>().To<ConsoleLogger>();
        //    idiKernel.Bind<IConnectionFactory>()
        //        .To<SqlConnectionFactory>()
        //        .WithConstructorArgument("sqlConnection", "SQL Connection");
        //    idiKernel.Bind<UserService>().ToSelf();
        //    idiKernel.Bind<QueryExecutor>().ToSelf();
        //    idiKernel.Bind<RestClient>().ToSelf().WithConstructorArgument("apiKey", "API KEY");
        //    idiKernel.Bind<CacheService>().ToSelf();
        //    idiKernel.Bind<CommandExecutor>().ToSelf();
        //    idiKernel.Bind<CreateUserHandler>().ToSelf();

        //    var createUserHandler = idiKernel.Get<CreateUserHandler>();
        //    createUserHandler.Handle();
        //    Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            var di = new DependencyResolver();

            di.Bind<ILogger>().To<ConsoleLogger>();
            di.Bind<IConnectionFactory>()
                .To<SqlConnectionFactory>()
                .WithConstructorArgument("sqlConnection", "SQL Connection");
            di.Bind<UserService>().ToSelf();
            di.Bind<QueryExecutor>().ToSelf();
            di.Bind<RestClient>().ToSelf().WithConstructorArgument("apiKey", "API KEY");
            di.Bind<CacheService>().ToSelf();
            di.Bind<CommandExecutor>().ToSelf();
            di.Bind<CreateUserHandler>().ToSelf();

            var createUserHandler = di.Get<CreateUserHandler>();
            createUserHandler.Handle();
            Console.ReadKey();
        }
    }
}
