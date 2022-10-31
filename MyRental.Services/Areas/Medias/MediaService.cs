using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Medias.Dto;

namespace MyRental.Services.Areas.Medias;

public class MediaService : IMediaService
{
    private readonly MyRentalContext _context;
    private readonly IMapper _mapper;

    public MediaService(MyRentalContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MediaDto> GetByIdAsync(int id)
    {
        return await _context.Medias
            .ProjectTo<MediaDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(file => file.Id == id)
                ?? throw new Exception($"File with Id:{id} is not found.");
    }

    public async Task<int> CreateAsync(MediaDtoInput mediaInput)
    {
        var media = _mapper.Map<MediaFile>(mediaInput);
        media.Length = mediaInput.Data.Length;
        
        await _context.AddAsync(media);
        await _context.SaveChangesAsync();
        
        return media.Id;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var file = await _context.Medias
            .FirstOrDefaultAsync(file => file.Id == id)
                ?? throw new Exception($"File with Id:{id} is not found.");

        _context.Medias.Remove(file);
        await _context.SaveChangesAsync();
    }
}