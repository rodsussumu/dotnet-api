using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    public class TestLogin : BaseIntegration
    {
        [Fact]
        public async Task TestingLogin()
        {
            await AddToken();
        }
    }
}
