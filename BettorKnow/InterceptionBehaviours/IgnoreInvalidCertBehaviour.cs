using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace BettorKnow.InterceptionBehaviours
{
    public class IgnoreInvalidCertBehaviour : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;
            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

    }
}