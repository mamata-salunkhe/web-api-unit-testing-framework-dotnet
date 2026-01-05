using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(string email);
        Task<Product> GetById(int id);
        User CreateUser(User user);

    }
    public class Product
    {
        public int ID { get; set; }
        public string Names { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

