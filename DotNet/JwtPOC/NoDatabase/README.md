# Pacotes

Microsoft.AspNetCore.Authentication.JwtBearer

Autenticaçao: determina a identidade do usário.
Autorização: determina qual recurso o usuário tem acesso.

Autenticação: manipulada pleo o serviço de autenticação ➡ `IAuthenticationService`.
`IAuthenticationService`: é usado pelo o middleware de autenticação.

Schemas: opções de configurações da atenticação.

```c#
builder.Services.AddAuthentication(schemas)
	.AddJwtBearer(options);
```

`AddAuthentication`: define o tipo de schemas utilizado na autenticação.
`AddJwtBearer`: utilizado para configurar o JWT.


# Authetication scheme


