using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Principal;
using System.Security;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Permissions;

namespace RakeAuthorizationManager
{
    public class RakeAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {

            string user = "Requester";
            IIdentity identity = new GenericIdentity(user);
            RakePrincipal rakePrincipal = new RakePrincipal(identity);
            OperationContext.Current.IncomingMessageProperties["Principal"] = rakePrincipal;
            return true;
        }
    }

    public class RakePrincipal : IPrincipal
    {
        readonly IIdentity identity;

        public RakePrincipal(IIdentity identity)
        {
            this.identity = identity;
        }

        public IIdentity Identity => this.identity;

        public bool IsInRole(string role)
        {
            return true;
        }
    }

    public class RakeAuthorizationPolicy : IAuthorizationPolicy
    {
        string id = Guid.NewGuid().ToString();
        public ClaimSet Issuer => ClaimSet.System;

        public string Id => this.id;

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            return true;
        }
    }

    public class RakePermission : IPermission
    {
        public RakePermission(string accessLevel)
        {
            _accessLevel = accessLevel;
        }

        public string _accessLevel { get; set; }

        public IPermission Copy()
        {
            throw new NotImplementedException();
        }

        public void Demand()
        {
            if (OperationContext.Current.IncomingMessageProperties["Principal"] is RakePrincipal)
            {
                RakePrincipal user = OperationContext.Current.IncomingMessageProperties["Principal"] as RakePrincipal;
               if( user.Identity.Name != _accessLevel)
                {
                    throw new FaultException("doesn't have permission");
                }
            }
           
        }

        public void FromXml(SecurityElement e)
        {
            throw new NotImplementedException();
        }

        public IPermission Intersect(IPermission target)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IPermission target)
        {
            return false;
        }

        public SecurityElement ToXml()
        {
            throw new NotImplementedException();
        }

        public IPermission Union(IPermission target)
        {
            throw new NotImplementedException();
        }
    }

    public class RakeCodePermissionAttribute : CodeAccessSecurityAttribute
    {
        public RakeCodePermissionAttribute(SecurityAction action) : base(action)
        {
        }

        public string _accessLevel { get; set; }

        public override IPermission CreatePermission()
        {
            return new RakePermission(_accessLevel);
        }
    }
}
