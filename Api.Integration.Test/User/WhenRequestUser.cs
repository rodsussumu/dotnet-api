using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
    public class WhenRequestUser : BaseIntegration
    {
        public string _name { get; set; }
        public string _email { get; set; }

        [Fact]
        public async void Is_Possible_To_Realize_Crud_User()
        {
            await AddToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };

            // Post
            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registerPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, registerPost.Name);
            Assert.Equal(_email, registerPost.Email);
            Assert.True(registerPost.Id != default(Guid));

            // Get All
            response = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.NotNull(listFromJson);
            Assert.True(listFromJson.Count() > 0);
            Assert.True(listFromJson.Where(r => r.Id == registerPost.Id).Count() == 1);

            // Put
            var updateUserDto = new UserDtoUpdate()
            {
                Id = registerPost.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registerUpdated = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registerPost.Name, registerUpdated.Name);
            Assert.NotEqual(registerPost.Email, registerUpdated.Email);

            // Get by ID
            response = await client.GetAsync($"{hostApi}users/{registerUpdated.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registerSelected = JsonConvert.DeserializeObject<UserDto>(jsonResult);
            Assert.NotNull(registerSelected);
            Assert.Equal(registerSelected.Name, registerUpdated.Name);
            Assert.Equal(registerSelected.Email, registerUpdated.Email);

            // Delete
            response = await client.DeleteAsync($"{hostApi}users/{registerUpdated.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Get Id after deleted
            response = await client.GetAsync($"{hostApi}users/{registerUpdated.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
