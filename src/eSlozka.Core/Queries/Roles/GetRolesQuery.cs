using eSlozka.Data;
using eSlozka.Domain.Models;
using eSlozka.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace eSlozka.Core.Queries.Roles;

public record GetRolesQuery : IQuery<List<Role>>;

public class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<Role>>
{
    private static readonly Func<DataContext, List<Role>> Query = EF.CompileQuery((DataContext context) => context.Roles.AsNoTracking().ToList());

    private readonly IDbContextFactory<DataContext> _contextFactory;

    public GetRolesQueryHandler(IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Task<List<Role>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        using var context = _contextFactory.CreateDbContext();

        var roles = Query(context);

        return Task.FromResult(roles);
    }
}
