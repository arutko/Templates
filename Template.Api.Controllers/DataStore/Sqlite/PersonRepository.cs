using Template.Api.Controllers.DatabaseContext;
using Template.Api.Controllers.DataStore.DataStore.Interfaces;
using Template.Api.Controllers.Models;

namespace Template.Api.Controllers.DataStore.DataStore.Sqlite;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext db) : base(db)
    {
    }
}
