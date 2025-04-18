using MediatR;

namespace Modules.Core.App.Queries.Test;
// Include properties to be used as input for the query
public record TestQuery() : IRequest<string>;
