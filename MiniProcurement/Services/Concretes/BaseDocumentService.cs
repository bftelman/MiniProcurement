using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Concretes
{
    public class BaseDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BaseDocumentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DocumentBase>> GetAllDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<DocumentBase> GetDocumentById(int id)
        {
            var document = await _context.Documents.FindAsync(id) ?? throw new Exception("Document not found");
            return document;
        }

        public async Task CreateDocument(CreateDocumentDto createDocumentDto)
        {
            var document = _mapper.Map<DocumentBase>(createDocumentDto);
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocument(DocumentBase document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
