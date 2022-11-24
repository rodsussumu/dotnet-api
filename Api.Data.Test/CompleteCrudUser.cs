using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CompleteCrudUser : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvide;

        public CompleteCrudUser(DbTest dbTest)
        {
            _serviceProvide = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD User")]
        [Trait("CRUD", "UserEntity")]
        public async Task Is_Possible_Using_CRUD_User()
        {
            using (var context = _serviceProvide.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };
                var _createdUser = await _repository.InsertAsync(_entity);
                Assert.NotNull(_createdUser);
                Assert.Equal(_entity.Email, _createdUser.Email);
                Assert.Equal(_entity.Name, _createdUser.Name);
                Assert.False(_createdUser.Id == Guid.Empty);

                _entity.Name = Faker.Name.First();
                var _updatedUser = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_updatedUser);
                Assert.Equal(_entity.Email, _updatedUser.Email);
                Assert.Equal(_entity.Name, _updatedUser.Name);


                var _userExists = await _repository.ExistAsync(_updatedUser.Id);
                Assert.True(_userExists);

                var _selectedUser = await _repository.SelectAsync(_updatedUser.Id);
                Assert.NotNull(_userExists);
                Assert.Equal(_updatedUser.Email, _selectedUser.Email);
                Assert.Equal(_updatedUser.Email, _selectedUser.Email);

                var _allUsers = await _repository.SelectAsync();
                Assert.NotNull(_allUsers);
                Assert.True(_allUsers.Count() > 1);

                var _removedUser = await _repository.DeleteAsync(_selectedUser.Id);
                Assert.True(_removedUser);

                var _defaultUser = await _repository.FindByLogin("user@example.com");
                Assert.NotNull(_defaultUser);
                Assert.Equal("user@example.com", _defaultUser.Email);
            }
        }

    }
}
