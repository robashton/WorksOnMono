using System;
using FubuMVC.Core.Continuations;

namespace Wom.Views.Home
{
    public class Authenticate
    {
        public AuthenticateViewModel Get()
        {
            return new AuthenticateViewModel();
        }

        public FubuContinuation Post(AuthenticateInputModel input)
        {
            throw new NotImplementedException();
        }
    }
}