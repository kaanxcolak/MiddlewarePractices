namespace MiddlewarePractices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //Middleware'ler genelde use ile başlayanlardır
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            //app.Run();
            //app.Run(async context => Console.WriteLine("Middleware 1."));
            //app.Run(async context => Console.WriteLine("Middleware 2."));

            //app.User()
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 1 başladı.");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware 1 sonlandırılıyor...");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 2 başladı.");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware 2 sonlandırılıyor...");
            //});

            // app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 3 başladı.");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware 3 sonlandırılıyor...");
            //});


            app.Use(async (context, next) =>
           {
               Console.WriteLine("Use Middleware tetiklendi");
               await next.Invoke();
               //Console.WriteLine("Middleware 3 sonlandırılıyor...");
           });

            app.Map("/example", internalApp => internalApp.Run(async context =>
            {
                Console.WriteLine("/example middleware tetiklendi.");
                await context.Response.WriteAsync("/example middleware tetiklendi.");
            }));



        }
    }
}