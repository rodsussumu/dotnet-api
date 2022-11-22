using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<Object> FindByLogin(LoginDto user);
    }
}
