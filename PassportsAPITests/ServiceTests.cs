using NUnit.Framework;
using AutoMapper;
using PassportsAPI.Mapper;
using PassportsAPI.EfCore;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Diagnostics;
using passports.Services.PassportService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            _mapper = CreateMapper();

            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("Test")
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;

            _dbContext = new DataContext(contextOptions);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

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
        public async Task GetInactivePaspportAsync_WithData()
        {
            _dbContext?.AddRange(new InactivePassports()
            { id = 1, Series = 1234, Number = 123456, IsActive = false, ChangeTime = new DateTime(2022, 8, 8) });

            _dbContext?.SaveChanges();
            var result = await _provider.GetInactivePassportAsync(1234,123456);


            Assert.AreEqual(1234, result.Series);
            Assert.AreEqual(123456, result.Number);
            Assert.AreEqual(false, result.IsActive);
            Assert.AreEqual(new DateTime(2022, 8, 8), result.ChangeTime);


        }
        [Test]
        public async Task GetInactivePaspportAsync_WithNull()
        {
            _dbContext?.AddRange(new InactivePassports()
            { id = 1, Series = 1234, Number = 123456, IsActive = false, ChangeTime = new DateTime(2022, 8, 8) });

            _dbContext?.SaveChanges();
            var result = await _provider.GetInactivePassportAsync(5534, 123456);


            Assert.Null(result);


        } 
        [Test]
        public async Task GetHistoryAsync_BySeriesNumber_WithData()
        {
            _dbContext?.Add(
                new PassportsHistory()
                {
                    id = 1,
                    IsActive = false,
                    ChangeTime = new DateTime(2022, 8, 8),
                    PassportId = 1,
                    Passport = new InactivePassports()
                    {
                        id = 1,
                        Series = 1234,
                        Number = 123456,
                        IsActive = false,
                        ChangeTime = new DateTime(2022, 8, 8)
                    }
                });

            _dbContext?.SaveChanges();
            var result = await _provider.GetHistoryAsync(1234, 123456);


            Assert.AreEqual(false, result[0].IsActive);
            Assert.AreEqual(new DateTime(2022, 8, 8), result[0].ChangeTime);

        }
        [Test]
        public async Task GetHistoryAsync_BySeriesNUmber_Empty()
        {
            _dbContext?.Add(
                new PassportsHistory()
                {
                    id = 1,
                    IsActive = false,
                    ChangeTime = new DateTime(2022, 8, 8),
                    PassportId = 1,
                    Passport = new InactivePassports()
                    {
                        id = 1,
                        Series = 1234,
                        Number = 123456,
                        IsActive = false,
                        ChangeTime = new DateTime(2022, 8, 8)
                    }
                });

            _dbContext?.SaveChanges();
            var result = await _provider.GetHistoryAsync(5555, 123456);


            Assert.IsEmpty(result);


        }
        [Test]
        public async Task GetHistoryAsync_ByDate_WithData()
        {
            _dbContext?.Add(
                new PassportsHistory()
                {
                    id = 1,
                    IsActive = false,
                    ChangeTime = new DateTime(2022, 8, 8).ToUniversalTime(),
                    PassportId = 1,
                    Passport = new InactivePassports()
                    {
                        id = 1,
                        Series = 1234,
                        Number = 123456,
                        IsActive = false,
                        ChangeTime = new DateTime(2022, 8, 8).ToUniversalTime()
                    }
                });

            _dbContext?.SaveChanges();
            var result = await _provider.GetHistoryAsync(new DateTime(2022, 8, 8).ToUniversalTime());


            Assert.AreEqual(false, result[0].IsActive);
            Assert.AreEqual(new DateTime(2022, 8, 8).ToUniversalTime(), result[0].ChangeTime);


        }
        [Test]
        public async Task GetHistoryAsync_ByDate_Empty()
        {
            _dbContext?.Add(
                 new PassportsHistory()
                 {
                     id = 1,
                     IsActive = false,
                     ChangeTime = new DateTime(2022, 8, 8).ToUniversalTime(),
                     PassportId = 1,
                     Passport = new InactivePassports()
                     {
                         id = 1,
                         Series = 1234,
                         Number = 123456,
                         IsActive = false,
                         ChangeTime = new DateTime(2022, 8, 8).ToUniversalTime()
                     }
                 }) ;

            _dbContext?.SaveChanges();
            var result = await _provider.GetHistoryAsync(new DateTime(2022, 8, 9));


            Assert.IsEmpty(result);


        }

        private IMapper CreateMapper()
        {
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new MapperProfile());
            });

            return config.CreateMapper();
        }


    }
}