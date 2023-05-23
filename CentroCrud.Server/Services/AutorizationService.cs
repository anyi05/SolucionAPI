using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentroCrud.Server.Models;
using CentroCrud.Server.Models.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CentroCrud.Server.Services
{
  public class AutorizationService : IAutorizationService
  {
    private readonly CentroFormacionContext _dbContext;
    private readonly IConfiguration _configuration;

    public AutorizationService(CentroFormacionContext context, IConfiguration configuration)
    {
      _dbContext = context;
      _configuration = configuration;
    }

    //Metodo para generar el token
    private string GenerarToken(string idUsuario)
    {
      var key = _configuration.GetValue<string>("JwtSettings:key");
      var keyBytes = Encoding.ASCII.GetBytes(key);

      var claims = new ClaimsIdentity();
      claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

      var credencialesToken = new SigningCredentials(
        new SymmetricSecurityKey(keyBytes),
        SecurityAlgorithms.HmacSha256Signature
        );

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = claims,
        Expires = DateTime.UtcNow.AddMinutes(10),
        SigningCredentials = credencialesToken
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

      string tokenCreado = tokenHandler.WriteToken(tokenConfig);
      return tokenCreado;
    }

    public async Task<AutorizationResponse> DevolverToken(AutorizationRequest autorization)
    {
      var usuario_encontrado = _dbContext.Usuarios.FirstOrDefault(x =>
      x.Usuario1 == autorization.usuario1 &&
      x.Password == autorization.password);

      if (usuario_encontrado == null)
      {
        return await Task.FromResult<AutorizationResponse>(null);
      }

      string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString());

      return new AutorizationResponse()
      {
        Token = tokenCreado,
        Result = true,
        Message = "Ok"
      };
    }
  }
}
