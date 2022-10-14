using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Ads.Dto;

namespace MyRental.Services.Areas.Ads.Services;

public class AdService : IAdService
{
    private readonly MyRentalContext _context;
    private readonly IMapper _mapper;

    public AdService(MyRentalContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<AdDto>> GetListAsync()
    {
        return await _context.Ads
            .ProjectTo<AdDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IList<AdDto>> GetFilteredListAsync(FilterDtoInput filter)
    {
        var query = _context.Ads
            .ProjectTo<AdDto>(_mapper.ConfigurationProvider);

        if (filter.Country != null) query = query.Where(ad => ad.Country == filter.Country);
        if (filter.City != null) query = query.Where(ad => ad.City == filter.City);
        if (filter.Area != null) query = query.Where(ad => ad.Area == filter.Area);
        if (filter.Rooms != 0) query = query.Where(ad => ad.Rooms == filter.Rooms);
        if (filter.Square != 0) query = query.Where(ad => ad.Square > filter.Square);
        if (filter.Price != 0.0d) query = query.Where(ad => ad.Price < filter.Price);
        if (filter.CreatedDate.Year != 1) query = query.Where(ad => ad.CreatedDate > filter.CreatedDate);

        return await query.ToListAsync();
    }

    public async Task<AdDto> GetByIdAsync(int id)
    {
        return await _context.Ads
            .ProjectTo<AdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ad => ad.Id == id)
                ?? throw new Exception($"Ad with Id:{id} is not found.");
    }

    public async Task<int> CreateAsync(AdDtoInput adInput)
    {
        await CheckIfTitleIsFreeAsync(adInput.Title);
        
        var ad = _mapper.Map<Ad>(adInput);

        await _context.Ads.AddAsync(ad);
        await _context.SaveChangesAsync();

        return ad.Id;
    }

    public async Task<int> UpdateByIdAsync(int id, AdDtoInput adInput)
    {
        var ad = await GetEntityByIdAsync(id);

        if (ad.Title != adInput.Title) await CheckIfTitleIsFreeAsync(adInput.Title);

        _mapper.Map(adInput, ad);

        _context.Ads.Update(ad);
        await _context.SaveChangesAsync();

        return ad.Id;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var ad = await GetEntityByIdAsync(id);

        _context.Remove(ad);
        await _context.SaveChangesAsync();
    }

    private async Task CheckIfTitleIsFreeAsync(string title)
    {
        var adWithSameTitle = await _context.Ads
            .FirstOrDefaultAsync(ad => ad.Title == title);

        if (adWithSameTitle != null) throw new Exception("Ad with this title is already exists.");
    }

    private async Task<Ad> GetEntityByIdAsync(int id)
    {
        return await _context.Ads
            .FirstOrDefaultAsync(ad => ad.Id == id)
                ?? throw new Exception($"Ad with Id:{id} is not found.");
    }
}