using System;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using KBriones.Models;
namespace KBriones
{
    public class AuthorizationServer : OAuthAuthorizationServerProvider
    {
        KEntities _context = new KEntities();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var user = from oc in _context.Usuario
                       join od in _context.TipoRol on oc.IdTipo equals od.IdTipo
                       where oc.Usuario1 == context.UserName && oc.Pass == context.Password
                       select new
                       {
                           Id = od.IdTipo,
                           Rol = od.Descripcion,
                           Usuario = oc.Usuario1,
                           Pass = oc.Pass
                       };

            Usuario usuarioDb = new Usuario();
            usuarioDb.Usuario1 = user.FirstOrDefault().Usuario;
            usuarioDb.Pass = user.FirstOrDefault().Pass;
            string rol = user.FirstOrDefault().Rol;
            
            if (usuarioDb != null && rol == "admin")
            {

                identity.AddClaim(new Claim(ClaimTypes.Role, rol));
                identity.AddClaim(new Claim("username", rol));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuarioDb.Usuario1));
                context.Validated(identity);
            }
            else if (usuarioDb != null && rol == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, rol));
                identity.AddClaim(new Claim("username", rol));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuarioDb.Usuario1));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
    }
}