using eSlozka.Data;
using eSlozka.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace eSlozka.Core.Queries.Users;

public record EmailExistsQuery(string? Email) : IQuery<bool>;

public class EmailExistsQueryHandler : IQueryHandler<EmailExistsQuery, bool>
{
    private static readonly Func<DataContext, string, bool> Query = EF.CompileQuery((DataContext context, string email) => context.Users
        .AsNoTracking()
        .IgnoreQueryFilters()
        .Any(user => user.Email == email));

    private readonly IDbContextFactory<DataContext> _contextFactory;

    public EmailExistsQueryHandler(IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Task<bool> Handle(EmailExistsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        if (string.IsNullOrWhiteSpace(request.Email)) return Task.FromResult(false);

        using var context = _contextFactory.CreateDbContext();

        return Task.FromResult(Query(context, request.Email));
    }
}