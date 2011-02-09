using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Runtime;

namespace Wom.Behaviours
{
    public class ServiceValidationBehaviour<T> : IActionBehavior where T : class
    {
        private readonly IFubuRequest fubuRequest;
        private readonly IValidator<T> validator;

        public IActionBehavior InsideBehavior { get; set; }

        public ServiceValidationBehaviour(IFubuRequest fubuRequest, IValidator<T> validator)
        {
            this.fubuRequest = fubuRequest;
            this.validator = validator;
        }

        public void Invoke()
        {
            var results = validator.Validate(fubuRequest.Get<T>());
            var output = new ServiceCommandOutput()
                             {
                                 Success = results.IsValid,
                                  Messages = results.Errors
                                  .Select(error=> new ServiceCommandOutputMessage()
                                                      {
                                                           Key = error.PropertyName,
                                                           Text = error.ErrorMessage
                                                      }).ToArray()
                             };
            fubuRequest.Set(output);
            
            if (InsideBehavior != null)
            {
                this.InsideBehavior.Invoke();
            }
        }

        public void InvokePartial()
        {
            this.Invoke();
        }
    }
}