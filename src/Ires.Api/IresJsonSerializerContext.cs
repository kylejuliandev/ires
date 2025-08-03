using Ires.Api.Endpoints.Person.CreatePerson;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Ires.Api;

[JsonSerializable(typeof(CreatePersonBody))]
[JsonSerializable(typeof(BadRequest<ProblemDetails>))]
internal partial class IresJsonSerializerContext : JsonSerializerContext
{
}
