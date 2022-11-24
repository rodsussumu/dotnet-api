using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class WhenLoginIsExecuted
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "FindByLogin Method")]
        public async Task Login_Method() {
            var email = Faker.Internet.Email();
            var objectReturn = new {
                authenticated = true,
                created = DateTime.UtcNow,
                expiration = DateTime.UtcNow,
                acessToken = Guid.NewGuid(),
                userName = email,
                message = "Usuário Logado com sucesso"
            };

            var loginDto = new LoginDto {
                Email = email
            };

             _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objectReturn);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);
        }
    }
}
