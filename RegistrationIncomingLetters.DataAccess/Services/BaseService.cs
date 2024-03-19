using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationIncomingLetters.Common.Models;
using RegistrationIncomingLetters.Common.Services;
using RegistrationIncomingLetters.DataAccess.Data;

namespace RegistrationIncomingLetters.DataAccess.Services
{
    public abstract class BaseService<TEntity, TDto>
        where TEntity : class
    {
        protected AppDbContext _context { get; set; }
        protected DbSet<TEntity> _entities { get; set; }
        protected Mapper _mapper;

        protected BaseService(AppDbContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, TDto>()
                    .ReverseMap();
            });
            _mapper = new Mapper(config);
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public virtual async Task<ResponseModel<List<TDto>>> GetAll()
        {
            var result = new ResponseModel<List<TDto>>();
            try
            {
                var entities = await _entities.ToListAsync();
                result.Data = _mapper.Map<List<TDto>>(entities);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                LogService.WriteLog(ex);
            }
            return result;
        }
    }
}
