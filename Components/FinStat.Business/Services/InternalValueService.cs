using System;
using System.Globalization;
using System.Threading.Tasks;
using FinStat.Domain.Configuration;
using FinStat.Domain.Interfaces.Repositories;
using FinStat.Domain.Interfaces.Services;

namespace FinStat.Business.Services
{
    public class InternalValueService : IInternalValueService
    {
        private readonly IInternalValueRepository _internalValueRepository;

        public InternalValueService(IInternalValueRepository internalValueRepository)
        {
            _internalValueRepository = internalValueRepository;
        }

        public async Task<T> GetValueAsync<T>(InternalValue<T> internalValue)
        {
            var key = internalValue.Key;
            var result = await _internalValueRepository.GetValue(key);
            if (result == null)
                return internalValue.DefaultValue;

            return ParseResult(result, internalValue.DefaultValue);
        }

        private T ParseResult<T>(string value, T defaultValue)
        {
            try
            {
                var type = typeof(T);
                if (type.IsEnum)
                    return (T)Enum.Parse(type, value);

                var result = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                return result;
            }
            catch (NotSupportedException)
            {
                return defaultValue;
            }
        }

        public Task UpdateValueAsync<T>(InternalValue<T> internalValue, T value)
        {
            var key = internalValue.Key;
            var entityValue = Convert.ToString(value, CultureInfo.InvariantCulture);

            return _internalValueRepository.UpdateValue(key, entityValue);
        }
    }
}
