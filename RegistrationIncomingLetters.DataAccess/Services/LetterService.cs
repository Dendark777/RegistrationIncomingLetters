using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationIncomingLetters.Common.Models;
using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.Common.Services;
using RegistrationIncomingLetters.DataAccess.Data;
using RegistrationIncomingLetters.DataAccess.Models;

namespace RegistrationIncomingLetters.DataAccess.Services
{
    public class LetterService : BaseService<Letter, LetterDto>
    {
        public LetterService(AppDbContext context) : base(context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Letter, LetterDto>()
                    .ReverseMap();
                cfg.CreateMap<Letter, LetterCutDto>()
                    .ReverseMap();
                cfg.CreateMap<Employee, EmployeeDto>()
                    .ReverseMap();
            });
            _mapper = new Mapper(config);

        }
        public override async Task<ResponseModel<List<LetterDto>>> GetAll()
        {
            var result = new ResponseModel<List<LetterDto>>();
            try
            {
                var entities = from letter in _context.Letters
                               join sender in _context.Employees on letter.SenderId equals sender.Id
                               join addressee in _context.Employees on letter.AddresseeId equals addressee.Id
                               select new LetterDto
                               {
                                   Id = letter.Id,
                                   Name = letter.Name,
                                   Content = letter.Content,
                                   Date = letter.Date,
                                   AddresseeId = letter.AddresseeId,
                                   Addressee = _mapper.Map<EmployeeDto>(letter.Addressee),
                                   SenderId = letter.SenderId,
                                   Sender = _mapper.Map<EmployeeDto>(letter.Sender)
                               };
                result.Data = _mapper.Map<List<LetterDto>>(await entities.ToListAsync());

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                LogService.WriteLog(ex);
            }
            return result;
        }
        public async Task<ResponseModel<LetterDto>> Create(LetterCutDto dto)
        {
            var result = new ResponseModel<LetterDto>();
            var newEntity = _mapper.Map<Letter>(dto);
            _entities.Add(newEntity);
            try
            {
                _context.SaveChanges();
                result.Data = _mapper.Map<LetterDto>(newEntity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                LogService.WriteLog(ex);
            }
            return result;
        }

        public async Task<ResponseModel<LetterDto>> Update(LetterCutDto dto)
        {
            var result = new ResponseModel<LetterDto>();
            var existingEntity = GetExistingLetter(dto.Id);
            _mapper.Map(dto, existingEntity);
            try
            {
                _context.SaveChanges();
                result.Data = _mapper.Map<LetterDto>(existingEntity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                LogService.WriteLog(ex);
            }
            return result;

        }

        public async Task<ResponseModel<LetterCutDto>> Delete(ulong id)
        {
            var result = new ResponseModel<LetterCutDto>();
            var existingEntity = GetExistingLetter(id);
            _entities.Remove(existingEntity);
            try
            {
                _context.SaveChanges();
                result.Data = _mapper.Map<LetterCutDto>(existingEntity);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                LogService.WriteLog(ex);
            }
            return result;
        }

        private Letter GetExistingLetter(ulong? id)
        {
            var result = _entities.FirstOrDefault(x => x.Id == id);
            return result ?? throw new Exception($"Ненайдено мисьмо с ИД {id}");
        }
    }
}
