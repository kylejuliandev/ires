using Ires.Api.Endpoints.Address.CreateAddress;
using Ires.Api.Endpoints.People.AddAddress;
using Ires.Api.Endpoints.People.CreatePerson;
using Ires.Api.Endpoints.People.GetPeople;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Ires.Api;

[JsonSerializable(typeof(CreatePersonBody))]
[JsonSerializable(typeof(GetPeopleResponse))]
[JsonSerializable(typeof(GetPeoplePerson))]
[JsonSerializable(typeof(AddAddressBody))]
[JsonSerializable(typeof(CreateAddressBody))]
[JsonSerializable(typeof(BadRequest<ProblemDetails>))]
internal partial class IresJsonSerializerContext : JsonSerializerContext
{
}
