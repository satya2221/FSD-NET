using AutoMapper;
using Models;

namespace _7._Intro_to_ASP;

public class RegionService (IRegionRepository _regionRepository, ITransactionRepository _transactionRepository, IMapper _mapper) : IRegionService
{
    public async Task<RegionResponseDto> CreateAsync(RegionRequestDto regionRequestDto)
    {
        var mapEntity = _mapper.Map<Region>(regionRequestDto);

        var entity = await _regionRepository.CreateAsync(mapEntity);
        await _transactionRepository.SaveChangesAsync();

        var toDto = _mapper.Map<RegionResponseDto>(entity);
        return toDto;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _regionRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (entity is null)
        {
            return false;
        }
        
        _regionRepository.Delete(entity);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<RegionResponseDto>?> GetAllAsync()
    {
        var data = await _regionRepository.GetAllAsync();

        var toDto = _mapper.Map<IEnumerable<RegionResponseDto>>(data);
        return toDto;
    }

    public async Task<RegionResponseDto?> GetByIdAsync(Guid id)
    {
        var entity = await _regionRepository.GetByIdAsync(id);
        var toDto = _mapper.Map<RegionResponseDto>(entity);
        return toDto;
    }

    public async Task<bool> UpdateAsync(Guid id, RegionRequestDto regionRequestDto)
    {
        var checkId = await _regionRepository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();
        if (checkId == null)
        {
            return false;
        }
        var mapEntity = _mapper.Map<Region>(regionRequestDto);
        // Assign id ke region
        mapEntity.Id = id;
        _regionRepository.Update(mapEntity);
        await _transactionRepository.SaveChangesAsync();
        return true;
    }
}
