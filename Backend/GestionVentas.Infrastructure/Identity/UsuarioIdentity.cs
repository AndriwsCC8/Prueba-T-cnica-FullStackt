using Microsoft.AspNetCore.Identity;

namespace GestionVentas.Infrastructure.Identity
{
    public class UsuarioIdentity : IdentityUser<int>
    {
        
        public string Nombre { get; set; } = "";
    }
}
