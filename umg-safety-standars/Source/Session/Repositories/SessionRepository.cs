using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using umg_database.Common.Interfaces;
using umg_database.Common.Resource;
using umg_database.Sources.Session.Entities;
using umg_database.Sources.Supplier.Entities;
using umg_safety_standars.Context;

namespace umg_safety_standars.Source.Session.Repositories
{
    [ExcludeFromCodeCoverage]
    public class SessionRepository : 
        IPostRepository<SessionEntity, SessionEntity>, 
        IPutRepository<SessionEntity>
    {
        private readonly SafetyStandarContext _context;
        public SessionRepository(SafetyStandarContext context)
        {
            _context = context;
        }
        public async Task<SessionEntity> PostAsync(SessionEntity data, CancellationToken cancellationToken = default)
        {

            string hash = Guid.NewGuid().ToString();
            try
            {
                if (data == null)
                {
                    SessionEntity session = new()
                    {
                        SessionId = Guid.Parse(hash),
                        Name = hash
                    };
                    await _context.Session.AddAsync(session);
                    await _context.SaveChangesAsync();
                    return session;

                }
                else
                {
                    SessionEntity session = new()
                    {
                        SessionId = Guid.Parse(hash),
                        Name = hash,
                        JwtExpiration = data.JwtExpiration,
                        JwtToken = data.JwtToken,
                        UpdatedAt = DateTime.UtcNow,
                        Id = data.Id
                    };
                    await _context.Session.AddAsync(session);
                    await _context.SaveChangesAsync();
                    return session;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(DatabaseException.SupplierInfoIncorrect + " | Error: " + ex);
            }
            finally { Console.WriteLine($"Session {hash} created!"); }
        }

        
        public async Task PutAsync(SessionEntity data)
        {
            try
            {
                SessionEntity session = await _context.Session.FirstAsync(session => session.Id == data.Id);

                if (data == null) throw new Exception(DatabaseException.SupplierInfoIncorrect);

                session.JwtExpiration = data.JwtExpiration;
                session.JwtToken = data.JwtToken;
                session.UpdatedAt = DateTime.UtcNow;
                session.Id = data.Id;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(DatabaseException.SupplierInfoIncorrect + " | Error: " + ex);
            }

        }
    }
}
