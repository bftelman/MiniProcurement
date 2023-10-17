using AutoMapper;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserResponseDto, User>();
            CreateMap<Department, CreateUserDepartmentDto>();
            CreateMap<DocumentBase, CreateDocumentDto>();
            CreateMap<PurchaseRequestDocumentItem, CreatePurchaseRequestDocumentItemDto>();
            CreateMap<User, GetUserDto>();
        }
    }
}
