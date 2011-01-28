﻿using System;
using System.Linq;
using System.Configuration;

using MbUnit.Framework;

using Hexa.Core;
using Hexa.Core.Domain;
using Hexa.Core.Database;
using Hexa.Core.Logging;
using Hexa.Core.Validation;

using Hexa.Core.Domain.Tests;
using Hexa.Core.Tests.Domain;
using Hexa.Core.Tests.Data;

namespace Hexa.Core.Tests.Sql
{
    [TestFixture]
    public class SqlTests : BaseDatabaseTest
    {
        protected override NHContextFactory CreateNHContextFactory()
        {
            return new NHContextFactory(DbProvider.MsSqlProvider, ConnectionString(), string.Empty, typeof(Entity).Assembly, ApplicationContext.Container);
        }

        protected override string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Sql.Connection"].ConnectionString;
        }

        [Test]
        [Rollback]
        public Guid Add_Human()
        {
            Human human = new Human();
            human.Name = "Martin";

            var repo = ServiceLocator.GetInstance<IHumanRepository>();
            using (var ctx = repo.UnitOfWork)
            {
                repo.Add(human);
                ctx.Commit();
            }

            Assert.IsNotNull(human);
            Assert.IsNotNull(human.Version);
            Assert.IsFalse(human.UniqueId == Guid.Empty);
            Assert.AreEqual("Martin", human.Name);

            return human.UniqueId;
        }

        [Test]
        [Rollback]
        public void Query_Human()
        {
            var uniqueId = Add_Human();

            var repo = ServiceLocator.GetInstance<IHumanRepository>();
            using (var ctx = repo.UnitOfWork)
            {
                var results = repo.GetFilteredElements(u => u.UniqueId == uniqueId);
                Assert.IsTrue(results.Count() > 0);
            }
        }

        [Test]
        [Rollback]
        public void Update_Human()
        {
            var uniqueId = Add_Human();

            var repo = ServiceLocator.GetInstance<IHumanRepository>();
            using (var ctx = repo.UnitOfWork)
            {
                var results = repo.GetFilteredElements(u => u.UniqueId == uniqueId);
                Assert.IsTrue(results.Count() > 0);

                var human2Update = results.First();
                human2Update.Name = "Maria";
                repo.Modify(human2Update);

                ctx.Commit();
            }

            repo = ServiceLocator.GetInstance<IHumanRepository>();
            using (var ctx = repo.UnitOfWork)
            {
                Assert.AreEqual("Maria", repo.GetFilteredElements(u => u.UniqueId == uniqueId).Single().Name);
            }
        }

        [Test]
        [Rollback]
        public void Delete_Human()
        {
            var uniqueId = Add_Human();

            var repo = ServiceLocator.GetInstance<IHumanRepository>();
            using (var ctx = repo.UnitOfWork)
            {
                var results = repo.GetFilteredElements(u => u.UniqueId == uniqueId);
                Assert.IsTrue(results.Count() > 0);

                var human2Delete = results.First();

                repo.Remove(human2Delete);

                ctx.Commit();
            }

            repo = ServiceLocator.GetInstance<IHumanRepository>();
            using (var ctx = repo.UnitOfWork)
            {
                Assert.AreEqual(0, repo.GetFilteredElements(u => u.UniqueId == uniqueId).Count());
            }
        }
    }
}