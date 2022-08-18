namespace PassportsAPI.EfCore
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.EnsureCreated();
            AddPassports(context);
            AddHistory(context);
        }
        private static void AddPassports(DataContext context)
        {
            var passport = context.IanctivePassports.FirstOrDefault();
            if (passport != null) return;

            context.IanctivePassports.Add(new InactivePassports
            {
                Series = 12345,
                Number = 12345,
                IsActive = true,
                ChangeTime = new DateTime(2022, 8, 8,0,0,0, DateTimeKind.Utc)

            });
            context.SaveChanges();
        }
        private static void AddHistory(DataContext context)
        {
            var passport = context.PassportsHistory.FirstOrDefault();
            if (passport != null) return;

            context.PassportsHistory.Add(new PassportsHistory
            {
                Series = 12345,
                Number = 12345,
                IsActive = true,
                ChangeTime = new DateTime(2022, 8, 8, 0, 0, 0, DateTimeKind.Utc)

            }); ;
            context.SaveChanges();
        }
    }
}
