using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AutoMapper;
using PassportsAPI.Mapper;
using PassportsAPI.EfCore;
using passports.Models;

namespace PassportsAPITests
{
    public class AutoMapperTest
    {
        MapperConfiguration? _mapperConfiguration;
        IMapper? _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            _mapper = _mapperConfiguration.CreateMapper();

        }

        [TearDown]
        public void TearDown()
        {
            _mapper = null;
            _mapperConfiguration = null;
        }

        [Test]
        public void InactivePassports_DAO_PassportsInfo()
        {
            var inactivePaspports = new InactivePassports()
            {
                id = 1,
                Series = 1234,
                Number = 123456,
                IsActive = false,
                ChangeTime = new DateTime(2022, 8, 8)

            };

            var mapped = _mapper.Map<PassportsInfo>(inactivePaspports);

            Assert.That(mapped, Is.Not.Null);
            Assert.That(mapped.Series, Is.EqualTo(1234));
            Assert.That(mapped.Number, Is.EqualTo(123456));
            Assert.That(mapped.IsActive, Is.False);
            Assert.That(mapped.ChangeTime, Is.EqualTo(new DateTime(2022, 8, 8)));

        }
        [Test]
        public void PassportsHistory_DAO_PassportsInfo()
        {
            var paspportsHistory = new PassportsHistory()
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
            };

            var mapped = _mapper.Map<PassportsInfo>(paspportsHistory);

            Assert.That(mapped, Is.Not.Null);
            Assert.That(mapped.Series, Is.EqualTo(1234));
            Assert.That(mapped.Number, Is.EqualTo(123456));
            Assert.That(mapped.IsActive, Is.False);
            Assert.That(mapped.ChangeTime, Is.EqualTo(new DateTime(2022, 8, 9)));

        }
    }
}
