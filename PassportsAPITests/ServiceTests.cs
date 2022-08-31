using NUnit.Framework;
using AutoMapper;
using PassportsAPI.Mapper;
using PassportsAPI.EfCore;
using passports.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Diagnostics;
using passports.Services.PassportService;
using Microsoft.AspNetCore.Mvc;

namespace PassportsAPITests
{
    public class Tests
    {
        private DataContext? _dbContext;
        private PassportService? _provider;
        private IMapper? _mapper;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("Test")
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;

            _dbContext = new DataContext(contextOptions);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _dbContext.AddRange(new InactivePassports()
            { id = 1, Series = 1234, Number = 123456, IsActive = false, ChangeTime = DateTime.Now });

            _dbContext.SaveChanges();


            _provider = new PassportService(_dbContext, _mapper);

        }

        [TearDown]
        public void TearDown()
        {
            _provider = null;
            _dbContext?.Dispose();
            _dbContext = null;
        }


        [Test]
        public void GetInactivePaspport()
        {
            var actrionResult = _provider.GetInactivePassportAsync(1234,123456);
            actrionResult.Wait();
            var viewResult = actrionResult.Result as PassportsInfo;


            Assert.Equals(1234, viewResult.Series);
            Assert.Equals(123456, viewResult.Number);
            Assert.Equals(false, viewResult.IsActive);
            Assert.Equals(DateTime.Now, viewResult.ChangeTime);


        }




    }
}