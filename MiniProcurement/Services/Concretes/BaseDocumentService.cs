using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.Document;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class BaseDocumentService: IBaseDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BaseDocumentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDocumentDto>> GetAllDocuments()
        {
            var documents = _mapper.Map<IEnumerable<GetDocumentDto>>(await _context.Documents.ToListAsync());
            return documents;
        }

        public async Task<GetDocumentDto> GetDocumentById(int id)
        {
            var document = await _context.Documents.FindAsync(id)
                ?? throw new Exception("Document not found. Please provide a valid id");
            return _mapper.Map<GetDocumentDto>(document);
        }

        public async Task CreateDocument(CreateDocumentDto createDocumentDto)
        {
            var document = _mapper.Map<DocumentBase>(createDocumentDto);
            _context.Documents.Add(document);
        }

        public async Task UpdateDocument(int id, UpdateDocumentDto updateDocumentDto)
        {
            var document = await _context.Documents.FindAsync(id)
            ?? throw new Exception("Document not found. Please provide a valid id");

            _mapper.Map(updateDocumentDto, document);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id)
                       ?? throw new Exception("Document not found. Please provide a valid id");

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

        }
    }
}
