using Authentication.Entities;
using Authentication.Entities.Helpers.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Authentication.Application
{
    public class AppAuthentication
    {
        private AuthResult authResult = new AuthResult();

        public AuthResult AuthUser(UserDb userDb, string inputPassword, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            try
            {
                bool credenciaisValidas = ValidateCredentials(userDb, inputPassword);

                if (credenciaisValidas)
                {
                    ClaimsIdentity identity = CreateClaims(userDb);

                    if (identity != null)
                    {
                        TokenDates tkDate = GetTokenDates(tokenConfigurations);

                        if (tkDate != null)
                        {
                            string token = CreateToken(tkDate, identity, signingConfigurations, tokenConfigurations);

                            if (!string.IsNullOrWhiteSpace(token))
                            {
                                authResult.authenticated = true;
                                authResult.created = tkDate.NotBefore;
                                authResult.expiration = tkDate.Expires;
                                authResult.accessToken = $"Bearer {token}";
                                authResult.message = "OK";

                                return authResult;
                            }
                            else
                            {
                                authResult.authenticated = false;
                                authResult.message = "Falha ao gerar token.";

                                return authResult;
                            }
                        }
                        else
                        {
                            authResult.authenticated = false;
                            authResult.message = "Falha ao gerar validade do token.";

                            return authResult;
                        }
                    }
                    else
                    {
                        authResult.authenticated = false;
                        authResult.message = "Impossível gerar token.";

                        return authResult;
                    }
                }
                else
                {
                    authResult.authenticated = false;
                    authResult.message = "Credenciais inválidas!";

                    return authResult;
                }

            }
            catch (Exception ex)
            {
                authResult.authenticated = false;
                authResult.message = "Ocorreu um erro durante a autenticação.";

                return authResult;
            }
        }

        private TokenDates GetTokenDates(TokenConfigurations tokenConfigurations)
        {
            try
            {
                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao.AddSeconds(tokenConfigurations.Seconds);
                TokenDates tkDt = new TokenDates(dataCriacao, dataExpiracao);

                return tkDt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string CreateToken(TokenDates tkDate, ClaimsIdentity identity, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = tkDate.NotBefore,
                    Expires = tkDate.Expires
                });
                string token = handler.WriteToken(securityToken);

                return token;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private ClaimsIdentity CreateClaims(UserDb userDb)
        {
            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(userDb.UserName, userDb.Service),
                    new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, userDb.UserName)
                    }
                );

                return identity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool ValidateCredentials(UserDb usrDb, string inputPassword)
        {
            //Verificar se senha está correta
            bool credenciaisValidas = SecurutyPbkdf2.VerifyHashedPassword(usrDb.PasswordHash, inputPassword);

            return credenciaisValidas;
        }
    }
}
