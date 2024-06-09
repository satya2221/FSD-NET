using Models;

namespace _7._Intro_to_ASP;

public interface IRegionService
{
    Task<IEnumerable<RegionResponseDto>?> GetAllAsync();
    Task<RegionResponseDto?> GetByIdAsync (Guid id);
    Task<RegionResponseDto> CreateAsync (RegionRequestDto regionRequestDto);
    Task<bool> UpdateAsync(Guid id, RegionRequestDto region);
    Task<bool> DeleteAsync (Guid id);
}
