using ask2.Repositories;
using ask2.Services;
using Microsoft.EntityFrameworkCore;

namespace ask2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services
            builder.Services.AddSingleton<IImapService, ImapService>();
            builder.Services.AddSingleton<ILetterService, LetterService>();
            builder.Services.AddSingleton<ILetterRepository, LetterRepository>();
            builder.Services.AddSingleton<IDbContextFactory<LettersContext>>(provider => new LettersContextFactory(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContext<LettersContext>(options =>
            //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region start point
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var imapService = app.Services.GetRequiredService<IImapService>();
            imapService.Start();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
