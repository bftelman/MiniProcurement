using AutoMapper;
using MiniProcurement.Data.Contracts.Document;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentBase, CreateDocumentDto>();
            CreateMap<PurchaseRequestDocumentItem, CreatePurchaseRequestDocumentItemDto>();
        }
    }
}
