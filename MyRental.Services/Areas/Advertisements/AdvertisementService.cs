using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Advertisements.Dto;

namespace MyRental.Services.Areas.Advertisements;

public class AdvertisementService : IAdvertisementService
{
    private readonly MyRentalContext _context;
    private readonly IMapper _mapper;

    public AdvertisementService(MyRentalContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<AdvertisementDto>> GetListAsync(AdvertisementFilterDto advertisementFilter)
    {
        var query = _context.Advertisements
            .ProjectTo<AdvertisementDto>(_mapper.ConfigurationProvider);

        if (advertisementFilter.UserId.HasValue) query = query.Where(ad => ad.UserId == advertisementFilter.UserId);
        if (!string.IsNullOrEmpty(advertisementFilter.Country)) query = query.Where(ad => ad.Country == advertisementFilter.Country);
        if (!string.IsNullOrEmpty(advertisementFilter.City)) query = query.Where(ad => ad.City == advertisementFilter.City);
        if (!string.IsNullOrEmpty(advertisementFilter.Area)) query = query.Where(ad => ad.Area == advertisementFilter.Area);
        if (advertisementFilter.Rooms.HasValue) query = query.Where(ad => ad.Rooms == advertisementFilter.Rooms);
        if (advertisementFilter.Square.HasValue) query = query.Where(ad => ad.Square > advertisementFilter.Square);
        if (advertisementFilter.Price.HasValue) query = query.Where(ad => ad.Price < advertisementFilter.Price);
        if (advertisementFilter.CreatedDate.HasValue) query = query.Where(ad => ad.CreatedDate > advertisementFilter.CreatedDate);

        return await query.ToListAsync();
    }

    public async Task<AdvertisementDto> GetByIdAsync(int id)
    {
        return await _context.Advertisements
            .ProjectTo<AdvertisementDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ad => ad.Id == id)
                ?? throw new Exception($"Ad with Id:{id} is not found.");
    }

    public async Task<int> CreateAsync(AdvertisementDtoInput advertisementInput)
    {
        await CheckIfTitleIsFreeAsync(advertisementInput.Title);
        
        var ad = _mapper.Map<Advertisement>(advertisementInput);

        for (var i = 0; i < advertisementInput.Medias.Count; i++)
        {
            ad.Medias.ElementAt(i).Length = advertisementInput.Medias.ElementAt(i).Data.Length;
        }
        
        await _context.Advertisements.AddAsync(ad);
        await _context.SaveChangesAsync();

        return ad.Id;
    }

    public async Task<int> UpdateByIdAsync(int id, AdvertisementDtoInput advertisementInput)
    {
        var advertisement = await _context.Advertisements
            .Include(ad => ad.Medias)
            .FirstOrDefaultAsync(ad => ad.Id == id)
                ?? throw new Exception($"Ad with Id:{id} is not found.");

        if (advertisement.Title != advertisementInput.Title) await CheckIfTitleIsFreeAsync(advertisementInput.Title);

        _mapper.Map(advertisementInput, advertisement);

        for (var i = 0; i < advertisementInput.Medias.Count; i++)
        {
            advertisement.Medias.ElementAt(i).Length = advertisementInput.Medias.ElementAt(i).Data.Length;
        }
        
        _context.Advertisements.Update(advertisement);
        await _context.SaveChangesAsync();

        return advertisement.Id;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var advertisement = await _context.Advertisements
            .FirstOrDefaultAsync(ad => ad.Id == id)
                ?? throw new Exception($"Ad with Id:{id} is not found.");

        _context.Remove(advertisement);
        await _context.SaveChangesAsync();
    }

    private async Task CheckIfTitleIsFreeAsync(string title)
    {
        var adWithSameTitle = await _context.Advertisements
            .FirstOrDefaultAsync(ad => ad.Title == title);

        if (adWithSameTitle != null) throw new Exception("Ad with this title is already exists.");
    }
}